from umqtt.simple import MQTTClient
import ubinascii
import network
import machine
import ntptime
import time

import esp
esp.osdebug(None)

import gc
gc.enable()
gc.threshold(0)
gc.collect()

device_id = 'SP001'
device_name = 'intercom module'
ssid = '<SSID>'
password = '<Wifi_Password>'
mqtt_user = b'<mqtt_username>'
mqtt_password = b'<mqtt_password>'
mqtt_server = '<mqtt_server>'
mqtt_port = 1883
mqtt_topic = 'devices/' + device_id + '/publish'
client_id = ubinascii.hexlify(machine.unique_id())

station = network.WLAN(network.STA_IF)

station.active(True)
station.connect(ssid, password)

while station.isconnected() == False:
  pass

print('Connection successful')
print(station.ifconfig())

client = MQTTClient(client_id, mqtt_server, port=mqtt_port, user=mqtt_user, password=mqtt_password)

try:
    client.connect()
    client.disconnect()
    print('MQTT Connection successful')
except Exception as error:
    print('MQTT Connection fail: ' + str(error))

ntptime.settime()
local_time = time.localtime(ntptime.time())
rtc = machine.RTC()
rtc.datetime((
    local_time[0],
    local_time[1],
    local_time[2],
    local_time[6],
    local_time[3],
    local_time[4],
    local_time[5],
    local_time[7]))

t = rtc.datetime()
print('current time: {:02d}-{:02d}-{:02d}T{:02d}:{:02d}:{:02d}:{:02d}'.format(t[0], t[1], t[2], t[4], t[5], t[6], t[7]))

adc = machine.ADC(0)
intercom = machine.Pin(16, machine.Pin.IN, None)
sound = machine.Pin(5, machine.Pin.IN, None)
d2 = machine.Pin(4, machine.Pin.IN, machine.Pin.PULL_UP)
d3 = machine.Pin(0, machine.Pin.IN, machine.Pin.PULL_UP)
d4 = machine.Pin(2, machine.Pin.IN, machine.Pin.PULL_UP)
d5 = machine.Pin(14, machine.Pin.IN, machine.Pin.PULL_UP)
d6 = machine.Pin(12, machine.Pin.IN, machine.Pin.PULL_UP)
d7 = machine.Pin(13, machine.Pin.IN, machine.Pin.PULL_UP)
d8 = machine.Pin(15, machine.Pin.IN, None)

