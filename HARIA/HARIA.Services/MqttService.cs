using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;
using HARIA.Domain.Models;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using Serilog;

namespace HARIA.Services
{
    public class MqttService : IMqttService
    {
        private readonly MQTTConfig mqttConfig;
        private readonly MqttClientOptions mqttOptions;
        private readonly IManagedMqttClient managedMqttClientSubscriber;
        private readonly IManagedMqttClient managedMqttClientPublisher;
        private Action<DeviceData> receiveMessageHandler;

        public MqttService(
            MQTTConfig mqttConfig)
        {
            this.mqttConfig = mqttConfig;
            mqttOptions = ConfigureMQTT(mqttConfig);
            var factory = new MqttFactory();
            managedMqttClientSubscriber = factory
                .CreateManagedMqttClient();
            managedMqttClientPublisher = factory
                .CreateManagedMqttClient();
        }

        public async Task StartPublisher()
        {
            managedMqttClientPublisher.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnPublisherConnected);
            managedMqttClientPublisher.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnPublisherDisconnected);
            if (!managedMqttClientPublisher.IsStarted)
            {
                await managedMqttClientPublisher.StartAsync(
                    new ManagedMqttClientOptions
                    {
                        ClientOptions = mqttOptions
                    });
            }
        }

        public async Task Subscribe(Action<DeviceData> receiveMessage)
        {
            receiveMessageHandler = receiveMessage;
            managedMqttClientSubscriber.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnSubscriberConnected);
            managedMqttClientSubscriber.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnSubscriberDisconnected);
            managedMqttClientSubscriber.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnSubscriberMessageReceived);
            if (!managedMqttClientSubscriber.IsStarted)
            {
                await managedMqttClientSubscriber.StartAsync(
                    new ManagedMqttClientOptions
                    {
                        ClientOptions = mqttOptions
                    });
            }
            var topicFilter = new MqttTopicFilter { Topic = mqttConfig.TopicsPattern };
            await managedMqttClientSubscriber.SubscribeAsync(topicFilter);
        }

        public async Task Unsubscribe()
        {
            receiveMessageHandler = null;
            await managedMqttClientSubscriber.UnsubscribeAsync(mqttConfig.TopicsPattern);
            await managedMqttClientSubscriber.StopAsync();
        }

        public async Task SendMessage(string payload, string topic)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag()
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
                .Build();
            if (managedMqttClientPublisher != null)
            {
                await managedMqttClientPublisher.PublishAsync(message);
            }
        }

        private void OnSubscriberConnected(MqttClientConnectedEventArgs args)
        {
            Log.Logger.Debug(Templates.LOG_TEMPLATE, mqttConfig.DeviceId, "MQTT subscriber connected", "");
        }

        private void OnSubscriberDisconnected(MqttClientDisconnectedEventArgs args)
        {
            if (args.Exception != null)
                Log.Logger.Error(args.Exception, Templates.LOG_TEMPLATE, mqttConfig.DeviceId, "MQTT subscriber disconnected", args.Exception.ToString());
        }

        private void OnPublisherConnected(MqttClientConnectedEventArgs args)
        {
            Log.Logger.Debug(Templates.LOG_TEMPLATE, mqttConfig.DeviceId, "MQTT publisher connected", "");
        }

        private void OnPublisherDisconnected(MqttClientDisconnectedEventArgs args)
        {                        
            if (args.Exception != null)
                Log.Logger.Error(args.Exception, Templates.LOG_TEMPLATE, mqttConfig.DeviceId, "MQTT publisher disconnected", args.Exception.ToString());
        }

        private void OnSubscriberMessageReceived(MqttApplicationMessageReceivedEventArgs args)
        {
            Log.Logger.Debug(Templates.LOG_TEMPLATE, mqttConfig.DeviceId, $"MQTT message received {DateTime.Now}", "");
            if (args.ApplicationMessage != null && args.ApplicationMessage.Payload != null)
            {
                var message = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
                var result = JsonSerializer.Deserialize<DeviceData>(message);
                receiveMessageHandler.Invoke(result);
            }
        }

        private MqttClientOptions ConfigureMQTT(MQTTConfig mqttConfig)
        {
            var options = new MqttClientOptions
            {
                ClientId = $"{mqttConfig.DeviceId}-{Domain.Helpers.SessionIdHelper.CreateSessionId()}",
                ProtocolVersion = MqttProtocolVersion.V500,
                ChannelOptions = new MqttClientTcpOptions
                {
                    Server = mqttConfig.Server,
                    Port = mqttConfig.Port,
                    TlsOptions = new MqttClientTlsOptions
                    {
                        UseTls = false,
                        IgnoreCertificateChainErrors = true,
                        IgnoreCertificateRevocationErrors = true,
                        AllowUntrustedCertificates = true
                    }
                }
            };
            options.Credentials = new MqttClientCredentials
            {
                Username = mqttConfig.User,
                Password = Encoding.UTF8.GetBytes(mqttConfig.Password)
            };
            options.CleanSession = false;
            return options;
        }
    }
}