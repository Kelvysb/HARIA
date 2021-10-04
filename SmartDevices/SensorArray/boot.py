from umqtt.simple import MQTTClient
import ubinascii
import network
import machine
import ntptime
import time
import json

import esp
esp.osdebug(None)

import gc
gc.enable()
gc.threshold(0)
gc.collect()

f = open('config.json',)
config_file = json.load(f)
config = config_file['config']
descriptions = config_file['descriptions']

device_id = config['device_id']
device_name = config['device_name']
ssid = config['wifi_ssid']
password = config['wifi_password']
mqtt_user = config['mqtt_user'].encode('utf-8')
mqtt_password = config['mqtt_password'].encode('utf-8')
mqtt_server = config['mqtt_server'].encode('utf-8')
mqtt_port = config['mqtt_port']
mqtt_topic_state = 'devices/' + device_id + '/state'
client_id = ubinascii.hexlify(machine.unique_id())

station = network.WLAN(network.STA_IF)

station.active(True)
station.connect(ssid, password)

while station.isconnected() == False:
  pass

print('Connection successful')
print(station.ifconfig())

client = MQTTClient(client_id, mqtt_server, port=mqtt_port, user=mqtt_user, password=mqtt_password, keepalive=60)

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

d0 = machine.Pin(16, machine.Pin.IN, None)
d1 = machine.Pin(5, machine.Pin.IN, machine.Pin.PULL_UP)
d2 = machine.Pin(4, machine.Pin.IN, machine.Pin.PULL_UP)
d3 = machine.Pin(0, machine.Pin.IN, machine.Pin.PULL_UP)
d4 = machine.Pin(2, machine.Pin.IN, machine.Pin.PULL_UP)
d5 = machine.Pin(14, machine.Pin.IN, machine.Pin.PULL_UP)
d6 = machine.Pin(12, machine.Pin.IN, machine.Pin.PULL_UP)
d7 = machine.Pin(13, machine.Pin.IN, machine.Pin.PULL_UP)
d8 = machine.Pin(15, machine.Pin.IN, None)

