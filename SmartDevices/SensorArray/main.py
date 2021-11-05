digital_0 = 'false'
digital_1 = 'false'
digital_2 = 'false'
digital_3 = 'false'
digital_4 = 'false'
digital_5 = 'false'
digital_6 = 'false'
digital_7 = 'false'
digital_8 = 'false'
timeout = 10000
last_ack = time.ticks_ms()

def get_Values():
    global digital_0
    global digital_1
    global digital_2
    global digital_3
    global digital_4
    global digital_5
    global digital_6
    global digital_7 
    global digital_8
    
    new_digital_0 = 'true' if d0.value() == 1 else 'false'
    new_digital_1 = 'true' if d1.value() == 1 else 'false'
    new_digital_2 = 'true' if d2.value() == 1 else 'false'
    new_digital_3 = 'true' if d3.value() == 1 else 'false'
    new_digital_4 = 'true' if d4.value() == 1 else 'false'
    new_digital_5 = 'true' if d5.value() == 1 else 'false'
    new_digital_6 = 'true' if d6.value() == 1 else 'false'
    new_digital_7 = 'true' if d7.value() == 1 else 'false'
    new_digital_8 = 'true' if d8.value() == 1 else 'false'
    
    result = (digital_0 != new_digital_0 or
              digital_1 != new_digital_1 or
              digital_2 != new_digital_2 or
              digital_3 != new_digital_3 or
              digital_4 != new_digital_4 or
              digital_5 != new_digital_5 or
              digital_6 != new_digital_6 or
              digital_7 != new_digital_7 or
              digital_8 != new_digital_8)
    
    digital_0 = new_digital_0
    digital_1 = new_digital_1
    digital_2 = new_digital_2
    digital_3 = new_digital_3
    digital_4 = new_digital_4
    digital_5 = new_digital_5
    digital_6 = new_digital_6
    digital_7 = new_digital_7
    digital_8 = new_digital_8
    
    return result

def get_message():  
    analog_0 = str(adc.read()*(330/1024))
    t = rtc.datetime()
    time = '{:02d}-{:02d}-{:02d}T{:02d}:{:02d}:{:02d}Z'.format(t[0], t[1], t[2], t[4], t[5], t[6])
    return '''{
    "device_id": "''' + device_id + '''",
    "device_name": "''' + device_name + '''",
    "sensors": {
        "d0": { "value": ''' + digital_0 + ''', "description": "''' + descriptions['d0'] + '''" },
        "d1": { "value": ''' + digital_1 + ''', "description": "''' + descriptions['d1'] + '''" },
        "d2": { "value": ''' + digital_2 + ''', "description": "''' + descriptions['d2'] + '''" },
        "d3": { "value": ''' + digital_3 + ''', "description": "''' + descriptions['d3'] + '''" },
        "d4": { "value": ''' + digital_4 + ''', "description": "''' + descriptions['d4'] + '''" },
        "d5": { "value": ''' + digital_5 + ''', "description": "''' + descriptions['d5'] + '''" },
        "d6": { "value": ''' + digital_6 + ''', "description": "''' + descriptions['d6'] + '''" },
        "d7": { "value": ''' + digital_7 + ''', "description": "''' + descriptions['d7'] + '''" },
        "d8": { "value": ''' + digital_8 + ''', "description": "''' + descriptions['d8'] + '''" },
        "a0": { "value": ''' + analog_0 + ''', "description": "''' + descriptions['a0'] + '''" }
    },
    "datetime": "''' + time + '''"
}'''

def publish(message):
    print('publish: '+ message +' to ' + mqtt_topic_state)
    client.publish(mqtt_topic_state, message, retain=False)
    
def clear_topic():
    print('Clear: ' + mqtt_topic_state)
    client.publish(mqtt_topic_state, "", retain=True)

def connect():
    global client_id, mqtt_server, mqtt_topic_set
    client.connect()
    print('Connected to %s MQTT broker' % (mqtt_server))
    logger.info('Connected to MQTT broker', 'Connected to %s MQTT broker' % (mqtt_server))

def restart_and_reconnect():
    print('Failed to connect to MQTT broker. Reconnecting...')
    logger.warning('Failed to connect to MQTT broker. Reconnecting...', device_id)
    time.sleep(10)
    machine.reset()
 
try:
    connect()
except OSError as e:
    restart_and_reconnect()
    
while True:    
    try:
        if get_Values():
            message = get_message()
            publish(message)
        elif time.ticks_diff(time.ticks_ms(), last_ack) > timeout:
            message = get_message()
            clear_topic()
            publish(message)
            last_ack = time.ticks_ms()
    except Exception as e:
        print('Error: '+ str(e))
        logger.error('Error: during loop', str(e))
        restart_and_reconnect()
                
    gc.collect()