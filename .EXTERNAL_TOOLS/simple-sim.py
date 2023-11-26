# simulation-simulator
import time
import json
import redis
import os
import random;

random_charts = True
random_pit_percent = True
random_hook_load = True

redsi_addresss = "85.198.9.229"
redis_port = 6379
redis_pass = "1qazxsw2$$"

out_sample_json = "{\"step\":0,\"Warnings\":{\"PumpWithKellyDisconnected\":false,\"PumpWithTopdriveDisconnected\":false,\"Pump1PopOffValveBlown\":false,\"Pump1Failure\":false,\"Pump2PopOffValveBlown\":false,\"Pump2Failure\":false,\"Pump3PopOffValveBlown\":false,\"Pump3Failure\":false,\"DrawworksGearsAbuse\":false,\"RotaryGearsAbuse\":false,\"HoistLineBreak\":false,\"PartedDrillString\":false,\"ActiveTankOverflow\":false,\"ActiveTankUnderVolume\":false,\"TripTankOverflow\":false,\"DrillPipeTwistOff\":false,\"DrillPipeParted\":false,\"TripWithSlipsSet\":false,\"Blowout\":false,\"UndergroundBlowout\":false,\"MaximumWellDepthExceeded\":true,\"CrownCollision\":false,\"FloorCollision\":false,\"TopdriveRotaryTableConfilict\":false},\"Equipments\":{\"BopControl\":{\"ManifoldPressureGauge\":1500,\"AirSupplyPressureGauge\":120,\"AccumulatorPressureGauge\":3000,\"AnnularPressureGauge\":0,\"AnnularOpenLED\":1,\"AnnularCloseLED\":0,\"UpperRamsOpenLED\":1,\"UpperRamsCloseLED\":0,\"MiddleRamsOpenLED\":1,\"MiddleRamsCloseLED\":0,\"KillLineOpenLED\":0,\"KillLineCloseLED\":1,\"ChokeLineOpenLED\":0,\"ChokeLineCloseLED\":1,\"LowerRamsOpenLED\":1,\"LowerRamsCloseLED\":0,\"AnnularStatus\":13.625,\"UpperRamsStatus\":13.625,\"MiddleRamsStatus\":13.625,\"LowerRamsStatus\":13.625},\"ChokeControl\":{\"StandPipePressure\":0,\"CasingPressure\":0,\"ChokePosition\":0,\"ChokePanelSPMCounter\":0,\"ChokePanelTotalStrokeCounter\":0,\"Choke1LED\":0,\"Choke2LED\":1},\"ChokeManifold\":{\"HydraulicChock1\":0,\"HydraulicChock2\":0,\"HyChock1OnProblem\":false,\"HyChock2OnProblem\":false,\"LeftManChokeOnProblem\":false,\"RightManChokeOnProblem\":false},\"DataDisplay\":{\"WOBPointer\":0,\"HookLoadPointer\":0,\"TripTankGauge\":0,\"TripTankAlarmLED\":0,\"TripTankPumpLED\":0,\"StandPipePressureGauge\":0,\"CasingPressureGauge\":0,\"MP1SPMGauge\":0,\"MP2SPMGauge\":0,\"ReturnLineTempGauge\":0,\"RotaryTorqueGauge\":-5.570499647696085e-7,\"RotaryRPMGauge\":1.0362694263458252,\"AcidGasDetectionLED\":0,\"TotalStrokeCounter\":0,\"PitGainLossGauge\":0,\"MudTanksVolumeGauge\":0,\"MVTAlarmLED\":0,\"ReturnMudFlowGauge\":0,\"FillStrokeCounter\":0,\"MFFITotalStrokeCounter\":0,\"MFFIAlarmLED\":0,\"MFFIPumpLED\":0,\"TotalWellDepth\":2000,\"BitDepth\":0,\"HookLoad\":0,\"StandPipePressure\":0,\"CasingPressure\":0,\"MP1SPM\":0,\"MP2SPM\":0,\"RTTorque\":-5.570499647696085e-7,\"RTRPM\":1.0362694263458252,\"WOP\":0,\"ROP\":0,\"MudWeightIn\":0,\"MudWeightOut\":0,\"Buzzer1\":false,\"Buzzer2\":false,\"Buzzer3\":false,\"Buzzer4\":false,\"MVTAlarmHigh\":0,\"MVTAlarmLow\":0,\"MFFIAlarmHigh\":0,\"MFFIAlarmLow\":0},\"Drilling\":{\"ParkingBrakeLed\":false,\"GEN1LED\":0,\"GEN2LED\":0,\"GEN3LED\":0,\"GEN4LED\":0,\"SCR1LED\":1,\"SCR2LED\":1,\"SCR3LED\":1,\"SCR4LED\":1,\"MP1BLWR\":1,\"MP2BLWR\":1,\"DWBLWR\":1,\"RTBLWR\":1,\"PWRLIM\":0,\"PWRLIMMTR\":0,\"RTTorqueLimitGauge\":0,\"AutoDWLED\":0,\"GEN1BTNLED\":0,\"GEN2BTNLED\":0,\"GEN3BTNLED\":0,\"GEN4BTNLED\":0,\"OpenKellyCockLed\":0,\"CloseKellyCockLed\":0,\"OpenSafetyValveLed\":1,\"CloseSafetyValveLed\":0,\"IRSafetyValveLed\":1,\"IRIBopLed\":0,\"LatchPipeLED\":0,\"UnlatchPipeLED\":0,\"SwingLed\":0,\"FillMouseHoleLed\":0,\"MP1CPSwitch\":-1,\"MP2CPSwitch\":-1,\"DWSwitch\":-1,\"RTSwitch\":-1,\"ResetWob\":-1,\"TotalStrokeCounterResetSwitch\":-1},\"Hook\":{\"HookHeight_S\":0,\"HookHeight\":75},\"StandPipeManifold\":{\"StandPipeGauge1\":0,\"StandPipeGauge2\":0},\"DrillingWatch\":{\"Depth\":2000,\"BitPosition\":1197.52,\"HookLoad\":0,\"WeightOnBit\":0,\"RPM\":1.0362694263458252,\"ROP\":0,\"Torque\":-5.570499647696085e-7,\"PumpPressure\":0,\"SPM1\":0,\"SPM2\":0,\"CasingPressure\":0,\"PercentFlow\":0,\"PitGainLose\":0,\"PitVolume\":68.68,\"KillMudVolume\":0,\"TripTankVolume\":0,\"MudWeightIn\":0,\"FillVolume\":0,\"MudWeightOut\":0}}}"

# os.system('mode con: cols=30 lines=20')

# sim_id = sim_last_out
dict_temp = {}


def get_json_val(json_data, nested_key, default_value):
  """Checks if a JSON nested key doesn't exist and returns a default value.

  Args:
    json_data: A JSON object.
    nested_key: A nested key.
    default_value: The default value to return if the nested key doesn't exist.

  Returns:
    The value of the nested key, or the default value if the nested key doesn't exist.
  """

  nested_key_parts = nested_key.split(".")

  current_json_data = json_data
  for key_part in nested_key_parts:
    if key_part not in current_json_data:
      return default_value

    current_json_data = current_json_data[key_part]

  return current_json_data

def replace_suffixes(keys):
  """Replaces `.in`, `.out`, and `.other` from the end of keys with `""` and makes the list distinct.

  Args:
    keys: A list of keys.

  Returns:
    A list of keys with the suffixes removed and the list made distinct.
  """

  distinct_keys = set()
  for key in keys:
    if key.endswith('.in'):
      key = key[:-3]
    elif key.endswith('.out'):
      key = key[:-4]
    elif key.endswith('.other'):
      key = key[:-6]
    else:
        continue

    distinct_keys.add(key)

  return list(distinct_keys)



num_loops = 5000
RESET_AFTER_N_LOOPS = 100 # *INTERVAL s
INTERVAL = 0.5 #s

# always runnig service ....
while True:

    if num_loops >= RESET_AFTER_N_LOOPS:
        r = redis.Redis(host=redsi_addresss, port=redis_port, password=redis_pass, decode_responses=True)
        num_loops = 0
    
    # Get all keys in Redis
    all_keys = r.scan(0)[1]
    

    # Filter the keys to only include those that end with ".in"
    all_keys = replace_suffixes(all_keys)
    
    print('found keys: ' , all_keys)
    
    for simulation_id in all_keys:
        json_in = r.get(f'{simulation_id}.in')
        
        if json_in is None:
            continue
        
        last_str = dict_temp.get(simulation_id ,out_sample_json)
        
        last_out_as_json = json.loads(last_str)
        currnt_out_as_json = json.loads(last_str)
        inputs_as_json = json.loads(json_in)
        
        # simulation is paused
        if inputs_as_json['status'] == 2:
            continue
        
        currnt_out_as_json['Equipments']['Drilling']['MP1CPSwitch'] = get_json_val(inputs_as_json ,'Equipments.Drilling.MP1CPSwitch' , 0 ) # inputs_as_json['Equipments']['Drilling']['MP1CPSwitch']
        currnt_out_as_json['Equipments']['Drilling']['MP2Switch'] = get_json_val(inputs_as_json ,'Equipments.Drilling.MP2CPSwitch' , 0 ) #inputs_as_json['Equipments']['Drilling']['MP2CPSwitch']
        currnt_out_as_json['Equipments']['Drilling']['DWSwitch']    =  get_json_val(inputs_as_json ,'Equipments.Drilling.DWSwitch' , 0 )#inputs_as_json['Equipments']['Drilling']['DWSwitch']
        currnt_out_as_json['Equipments']['Drilling']['RTSwitch']    = get_json_val(inputs_as_json ,'Equipments.Drilling.RTSwitch' , 0 ) #inputs_as_json['Equipments']['Drilling']['RTSwitch']
        
        #alarm switch
        currnt_out_as_json['Equipments']['DataDisplay']['MVTSetAlarmSwitch']    = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MVTSetAlarmSwitch' , 0 ) #inputs_as_json['Equipments']['DataDisplay']['MVTSetAlarmSwitch']
        currnt_out_as_json['Equipments']['DataDisplay']['MFFISetAlarmSwitch']   = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MFFISetAlarmSwitch' , 0 ) #inputs_as_json['Equipments']['DataDisplay']['MFFISetAlarmSwitch'] 
        # currnt_out_as_json['Equipments']['DataDisplay']['MVTAlarmLED']          = currnt_out_as_json['Equipments']['DataDisplay']['MVTSetAlarmSwitch']
        # currnt_out_as_json['Equipments']['DataDisplay']['MFFIAlarmLED']         = currnt_out_as_json['Equipments']['DataDisplay']['MFFISetAlarmSwitch'] 
        
        currnt_out_as_json['Equipments']['DataDisplay']['MVTAlarmHigh']    = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MVTAlarmHigh' , 0 ) #inputs_as_json['Equipments']['DataDisplay']['MVTAlarmHigh']    
        currnt_out_as_json['Equipments']['DataDisplay']['MVTAlarmLow']     = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MVTAlarmLow' , 0 )#inputs_as_json['Equipments']['DataDisplay']['MVTAlarmLow']    
        currnt_out_as_json['Equipments']['DataDisplay']['MFFIAlarmHigh']    = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MFFIAlarmHigh' , 0 )#inputs_as_json['Equipments']['DataDisplay']['MFFIAlarmHigh']    
        currnt_out_as_json['Equipments']['DataDisplay']['MFFIAlarmLow']     = get_json_val(inputs_as_json ,'Equipments.DataDisplay.MFFIAlarmLow' , 0 )#inputs_as_json['Equipments']['DataDisplay']['MFFIAlarmLow'] 
        
        
        if last_out_as_json['Equipments']['Drilling']['MP1CPSwitch'] == -1:
            currnt_out_as_json['Equipments']['DataDisplay']['MP1SPM'] = inputs_as_json['Equipments']['DataDisplay']['MP1SPM']
        else:
            currnt_out_as_json['Equipments']['DataDisplay']['MP1SPM'] = 0
        
        if last_out_as_json['Equipments']['Drilling']['MP2CPSwitch'] == -1:
            currnt_out_as_json['Equipments']['DataDisplay']['MP2SPM'] = inputs_as_json['Equipments']['DataDisplay']['MP2SPM']
        else:
            currnt_out_as_json['Equipments']['DataDisplay']['MP2SPM'] = 0
        
        if last_out_as_json['Equipments']['Drilling']['DWSwitch'] == -1:
            currnt_out_as_json['Equipments']['Drilling']['DWThrottle'] = inputs_as_json['Equipments']['Drilling']['DWThrottle']
        else:
            currnt_out_as_json['Equipments']['Drilling']['DWThrottle'] = 0
            
        if last_out_as_json['Equipments']['Drilling']['RTSwitch'] == -1:
            currnt_out_as_json['Equipments']['DataDisplay']['RTRPM'] = inputs_as_json['Equipments']['DataDisplay']['RTRPM']
        else:
            currnt_out_as_json['Equipments']['DataDisplay']['RTRPM'] = 0
            
        # calculate hook height
        if currnt_out_as_json['Equipments']['Drilling']['DWSwitch'] == -1:
            hook_height_changes = currnt_out_as_json['Equipments']['Drilling']['DWThrottle'] * 1  / 900 
            currnt_out_as_json['Equipments']['Hook']['HookHeight'] = currnt_out_as_json['Equipments']['Hook']['HookHeight'] + hook_height_changes
            # limit to real work limits
            currnt_out_as_json['Equipments']['Hook']['HookHeight'] = min(currnt_out_as_json['Equipments']['Hook']['HookHeight'] , 140)
            currnt_out_as_json['Equipments']['Hook']['HookHeight'] = max(currnt_out_as_json['Equipments']['Hook']['HookHeight'] , 0)
        
        if get_json_val(inputs_as_json ,'Equipments.Drilling.TotalStrokeCounterResetSwitch' , 0 ) == -1:
            print('total strock signaled to 0')
            currnt_out_as_json['Equipments']['DataDisplay']['TotalStrokeCounter'] = 0
        
        if get_json_val(inputs_as_json ,'Equipments.Drilling.ResetWob' , 0 ) == -1:
            print('WeightOnBit signaled to 0')
            currnt_out_as_json['Equipments']['DrillingWatch']['WeightOnBit'] = 0
        
        
        if currnt_out_as_json['Equipments']['Hook']['HookHeight'] < 10:
        
            if random_pit_percent:
                # set pit percet / percent flow randomly
                currnt_out_as_json['Equipments']['DrillingWatch']['PercentFlow'] = random.randint( 85 , 100)
                currnt_out_as_json['Equipments']['DrillingWatch']['PitGainLose'] = random.randint( -10 , 10)
            
            if currnt_out_as_json['Equipments']['DataDisplay']['MVTSetAlarmSwitch'] == -1 and (currnt_out_as_json['Equipments']['DrillingWatch']['PercentFlow'] > get_json_val(inputs_as_json ,'Equipments.DataDisplay.MVTAlarmHigh' , 0 ) or currnt_out_as_json['Equipments']['DrillingWatch']['PercentFlow'] < get_json_val(inputs_as_json ,'Equipments.DataDisplay.MVTAlarmLow' , 0 )):
                currnt_out_as_json['Equipments']['DataDisplay']['MVTAlarmLED'] = -1
            else:
                currnt_out_as_json['Equipments']['DataDisplay']['MVTAlarmLED'] = 0
                    
            
            if currnt_out_as_json['Equipments']['DataDisplay']['MFFISetAlarmSwitch'] == -1 and \
                (currnt_out_as_json['Equipments']['DrillingWatch']['PitGainLose'] > get_json_val(inputs_as_json ,'Equipments.DataDisplay.MFFIAlarmHigh' , 0 ) or \
                currnt_out_as_json['Equipments']['DrillingWatch']['PitGainLose'] < get_json_val(inputs_as_json ,'Equipments.DataDisplay.MFFIAlarmLow' , 0 )):
                currnt_out_as_json['Equipments']['DataDisplay']['MFFIAlarmLED'] = -1
            else:
                currnt_out_as_json['Equipments']['DataDisplay']['MFFIAlarmLED'] = 0
            
            
            if random_charts:
                # set rand data for ROP,WOB,PRESS,TORQ
                currnt_out_as_json['Equipments']['DrillingWatch']['ROP'] = random.randint(0 , 24)
                currnt_out_as_json['Equipments']['DrillingWatch']['Torque'] = random.randint(0 , 24)
                currnt_out_as_json['Equipments']['DrillingWatch']['PumpPressure'] = random.randint(0 , 13)
                
            
            currnt_out_as_json['Equipments']['DrillingWatch']['WeightOnBit'] = currnt_out_as_json['Equipments']['DrillingWatch']['WeightOnBit'] + random.randint(4 , 23)
            currnt_out_as_json['Equipments']['DataDisplay']['TotalStrokeCounter'] = last_out_as_json['Equipments']['DataDisplay']['TotalStrokeCounter'] + random.randint(0,35)
        currnt_out_as_json['step'] = currnt_out_as_json['step'] + 1
        
        # Convert the JSON data to a string
        str_json_out = json.dumps(currnt_out_as_json)
        # print('config = ' , currnt_out_as_json)
        
        # Store in storage
        dict_temp[simulation_id] = str_json_out
        
        # Set the key in Redis to the JSON string
        r.set(f'{simulation_id}.out', str_json_out)   
    
    print(f'loop {num_loops} --finished')
    num_loops += 1

    # Wait for 500ms
    time.sleep(INTERVAL)
