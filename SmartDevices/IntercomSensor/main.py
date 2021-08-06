intercom_state = 'false'
sound_state = 'false'
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
    global intercom_state
    global sound_state
    global digital_2
    global digital_3
    global digital_4
    global digital_5
    global digital_6
    global digital_7 
    global digital_8
    
    new_intercom_state = 'true' if intercom.value() == 1 else 'false'
    new_sound_state = 'true' if sound.value() == 0 else 'false'
    new_digital_2 = 'true' if d2.value() == 0 else 'false'
    new_digital_3 = 'true' if d3.value() == 0 else 'false'
    new_digital_4 = 'true' if d4.value() == 0 else 'false'
    new_digital_5 = 'true' if d5.value() == 0 else 'false'
    new_digital_6 = 'true' if d6.value() == 0 else 'false'
    new_digital_7 = 'true' if d7.value() == 0 else 'false'
    new_digital_8 = 'true' if d8.value() == 1 else 'false'
    
    result = (intercom_state != new_intercom_state or
              sound_state != new_sound_state or
              digital_2 != new_digital_2 or
              digital_3 != new_digital_3 or
              digital_4 != new_digital_4 or
              digital_5 != new_digital_5 or
              digital_6 != new_digital_6 or
              digital_7 != new_digital_7 or
              digital_8 != new_digital_8)
    
    intercom_state = new_intercom_state
    sound_state = new_sound_state
    digital_2 = new_digital_2
    digital_3 = new_digital_3
    digital_4 = new_digital_4
    digital_5 = new_digital_5
    digital_6 = new_digital_6
    digital_7 = new_digital_7
    digital_8 = new_digital_8
    
    return result

def get_message():  
    temperature = str(adc.read()*(330/1024))
    t = rtc.datetime()
    time = '{:02d}-{:02d}-{:02d}T{:02d}:{:02d}:{:02d}Z'.format(t[0], t[1], t[2], t[4], t[5], t[6])
    return '''{
    "device_id": "''' + device_id + '''",
    "device_name": "''' + device_name + '''",
    "intercom": ''' + intercom_state + ''',
    "sound": ''' + sound_state + ''',
    "digital_in": {        
        "d2": ''' + digital_2 + ''',
        "d3": ''' + digital_3 + ''',
        "d4": ''' + digital_4 + ''',
        "d5": ''' + digital_5 + ''',
        "d6": ''' + digital_6 + ''',
        "d7": ''' + digital_7 + ''',
        "d8": ''' + digital_8 + '''    
    },
    "temperature": ''' + temperature + ''',
    "datetime": "''' + time + '''"
}'''

def publish(message):
    print('publish: '+ message +' to ' + mqtt_topic)
    client.connect()
    client.publish(mqtt_topic, message, retain=False)
    client.disconnect()
    
def clear_topic():
    print('Clear: ' + mqtt_topic)
    client.connect()
    client.publish(mqtt_topic, "", retain=True)
    client.disconnect()

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
                
    gc.collect()