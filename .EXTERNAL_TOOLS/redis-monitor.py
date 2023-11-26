# simulation-simulator
import time
import json
import redis
import os
import random;

os.system('mode con: cols=30 lines=20')

# Connect to Redis
r = redis.Redis(host='85.198.9.229', port=6379, password='1qazxsw2$$', decode_responses=True)

# simulation_id = 'c7e41563-d08b-43e2-dd36-08dbe12513b0'
#simulation_id = '72a84eb3-eee5-4104-8363-08dba3c9a113'
simulation_id = 'b6996849-fb69-4076-ca17-08dbea60e4cb'

keyin = f'{simulation_id}.in'
keyout = f'{simulation_id}.out'

num_loops = 0
RESET_AFTER_N_LOOPS = 100 # *INTERVAL s
INTERVAL = 1 #s

while True:

    if num_loops >= RESET_AFTER_N_LOOPS:
        r = redis.Redis(host='85.198.9.229', port=6379, password='1qazxsw2$$', decode_responses=True)
        num_loops = 0

    # Read the first row of the key from the index as JSON
    json_data = r.get(keyin)

    inputs = 'could not get inputs.'
    outputs = 'could not get outputs.'

    if json_data is not None:
        try:
            data = json.loads(json_data)
            speed = data['speed']
            hook_height = data['Equipments']['Hook']['HookHeight']
            rop = data['Equipments']['DrillingWatch']['ROP']
            dw = data['Equipments']['Drilling']['DWThrottle']
            dwbl = data['Equipments']['Drilling']['DWBLWR']
            mp1smp = data['Equipments']['DataDisplay']['MP1SPM']
            mp2smp = data['Equipments']['DataDisplay']['MP2SPM']
            mp1sw = data['Equipments']['Drilling']['MP1CPSwitch']
            # mp2sw = data['Equipments']['Drilling']['MP2Switch']
            mp2sw = '?'
            rtsw = data['Equipments']['Drilling']['RTSwitch']
            dwsw = data['Equipments']['Drilling']['DWSwitch']
            rt = data['Equipments']['DataDisplay']['RTRPM']
            pit_vol = data['Equipments']['DrillingWatch']['PitVolume']
            pf = data['Equipments']['DrillingWatch']['PercentFlow']

            inputs = f's: {speed}\nHH: {hook_height}\ndw: {dw} - {"UNLOCK" if dwbl==1 else "LOCK"}\nRT: {rt}\n \
        MP1SW: {mp1sw} , MP2SW: {mp2smp} , RTSW: {rtsw} , DWSW: {dwsw} \
        MP1SPM: {mp1smp} , MP2SPM : {mp2smp}\nPitVol: {pit_vol}\nPerFlow: {pf}'

        except json.JSONDecodeError:
            print('Invalid JSON data:', json_data)
    else:
        print('No data at index', keyin)

    json_data = r.get(keyout)
    if json_data is not None:
        try:
            data = json.loads(json_data)
            steps = data['step']
            hook_height = data['Equipments']['Hook']['HookHeight']
            rop = data['Equipments']['DrillingWatch']['ROP']
            dwbl = data['Equipments']['Drilling']['DWBLWR']
            mp1smp = data['Equipments']['DataDisplay']['MP1SPM']
            mp2smp = data['Equipments']['DataDisplay']['MP2SPM']
            rt = data['Equipments']['DataDisplay']['RTRPM']
            pit_vol = data['Equipments']['DrillingWatch']['PitVolume']
            pf = data['Equipments']['DrillingWatch']['PercentFlow']

            outputs = f's: {steps}\nHH: {hook_height}\ndw: {"UNLOCK" if dwbl==1 else "LOCK"}\nRT: {rt}\nMP1SPM: {mp1smp} , MP2SPM : {mp2smp}\nPitVol: {pit_vol}\nPerFlow: {pf}'

        except json.JSONDecodeError:
            print('Invalid JSON data:', json_data)
    else:
        print('No data at index', keyout)

    os.system('cls')
    print("inputs ------------- \n", inputs, '\n\n')
    print("outputs ------------- \n", outputs)

    num_loops += 1

    # Wait for 500ms
    time.sleep(INTERVAL)
