using SimulationInputValues;
using SimulationOutPutValues;
using System.Collections.Generic;
using static System.Enum;

public class SimulationFeilds
{
    public OutputRoot OutputVals { set; get; } = new();
    public InputRoot InputValues { set; get; } = new();
    public TempValues TempValues { set; get; } = new();
    
    
}


public class UnitySignalsType
{
    public int MudBucket { get; set; }
    public int Elevator { get; set; }
    public int FillupHead { get; set; }
    public int Ibop { get; set; }
    public int Kelly { get; set; }
    public int MouseHole { get; set; }
    public int OperationCondition { get; set; }
    public int SafetyValve { get; set; }
    public int operation { get; set; }
    public int Slips { get; set; }
    public int Slips_S { get; set; }
    public int Swing { get; set; }
    public int Swing_S { get; set; }
    public int TdsBackupClamp { get; set; }
    public int TdsSpine { get; set; }
    public int TdsSwing { get; set; }
    public int TdsTong { get; set; }
    public int Tong { get; set; }
    public int Tong_S { get; set; }
    public int TdsConnectionModes { get; set; }
    public int TdsElevatorModes { get; set; }

    public UnitySignalsType()
    {
        MudBucket = 0;
        Elevator = 0;
        FillupHead = 0;
        Ibop = 0;
        Kelly = 0;
        MouseHole = 0;
        OperationCondition = 0;
        SafetyValve = 0;
        operation = 0;
        Slips = 0;
        Slips_S = 0;
        Swing = 0;
        Swing_S = 0;
        TdsBackupClamp = 0;
        TdsSpine = 0;
        TdsSwing = 0;
        TdsTong = 0;
        Tong = 0;
        Tong_S = 0;
        TdsConnectionModes = 0;
        TdsElevatorModes = 0;
    }
}

 public enum TONG
    {
        TONG_NEUTRAL,
        TONG_BREAKOUT_BEGIN,
        TONG_BREAKOUT_END,
        TONG_MAKEUP_BEGIN,
        TONG_MAKEUP_END
    }

    public enum TDS_SWING
    {
        TDS_SWING_NEUTRAL,
        TDS_SWING_OFF_BEGIN,
        TDS_SWING_OFF_END,
        TDS_SWING_DRILL_BEGIN,
        TDS_SWING_DRILL_END,
        TDS_SWING_TILT_BEGIN,
        TDS_SWING_TILT_END
    }

    public enum TDS_SPINE
    {
        TDS_SPINE_NEUTRAL,
        TDS_SPINE_CONNECT_BEGIN,
        TDS_SPINE_CONNECT_END,
        TDS_SPINE_DISCONNECT_BEGIN,
        TDS_SPINE_DISCONNECT_END
    }

    public enum BACKUP_CLAMP
    {
        BACKUP_CLAMP_OFF_END,
        BACKUP_CLAMP_OFF_BEGIN,
        BACKUP_CLAMP_FW_BEGIN,
        BACKUP_CLAMP_FW_END
    }

    public enum SWING
    {
        SWING_NEUTRAL,
        SWING_MOUSE_HOLE_BEGIN,
        SWING_MOUSE_HOLE_END,
        SWING_RAT_HOLE_BEGIN,
        SWING_RAT_HOLE_END,
        SWING_WELL_BEGIN,
        SWING_WELL_END
    }

    public enum TDS_TONG
    {
        TDS_TONG_BREAKOUT_END,
        TDS_TONG_BREAKOUT_BEGIN,
        TDS_TONG_MAKEUP_BEGIN,
        TDS_TONG_MAKEUP_END
    }

    public enum SAFETY_VALVE
    {
        SAFETY_VALVE_NEUTRAL,
        SAFETY_VALVE_REMOVE,
        SAFETY_VALVE_INSTALL
    }

    public enum OPERATION
    {
        OPERATION_DRILL,
        OPERATION_TRIP
    }

    public enum SLIPS
    {
        SLIPS_NEUTRAL,
        SLIPS_SET_BEGIN,
        SLIPS_SET_END,
        SLIPS_UNSET_BEGIN,
        SLIPS_UNSET_END,

        MOUSE_HOLE_NEUTRAL,
        MOUSE_HOLE_FILL,
        MOUSE_HOLE_EMPTY,

        KELLY_NEUTRAL,
        KELLY_INSTALL,
        KELLY_REMOVE,

        FILLUP_HEAD_REMOVE,
        FILLUP_HEAD_INSTALL,

        ELEVATOR_NEUTRAL,

        ELEVATOR_LATCH_STRING_BEGIN,
        ELEVATOR_LATCH_STRING_END,

        ELEVATOR_UNLATCH_STRING_BEGIN,
        ELEVATOR_UNLATCH_STRING_END,

        ELEVATOR_LATCH_STAND_BEGIN,
        ELEVATOR_LATCH_STAND_END,

        ELEVATOR_UNLATCH_STAND_BEGIN,
        ELEVATOR_UNLATCH_STAND_END,

        ELEVATOR_LATCH_SINGLE_BEGIN,
        ELEVATOR_LATCH_SINGLE_END,

        ELEVATOR_UNLATCH_SINGLE_BEGIN,
        ELEVATOR_UNLATCH_SINGLE_END,

        MUD_BUCKET_REMOVE,
        MUD_BUCKET_INSTALL,

        IBOP_REMOVE,
        IBOP_INSTALL,

        TDS_CONNECTION_NOTHING,
        TDS_CONNECTION_STRING,
        TDS_CONNECTION_SPINE,

        TDS_ELEVATOR_CONNECTION_NOTHING,
        TDS_ELEVATOR_CONNECTION_STRING,
        TDS_ELEVATOR_CONNECTION_SINGLE,
        TDS_ELEVATOR_CONNECTION_STAND,
        TDS_ELEVATOR_LATCH_STRING,
        TDS_ELEVATOR_LATCH_SINGLE,
        TDS_ELEVATOR_LATCH_STAND
    }

// public class OperationScenarioEvent
// {
//     public string KellyConnection { get; set; } = KellyConnectionEnum.ConnectionNothing.ToString();
//     public int KellyConnectionCode { get; set; } = (int) KellyConnectionEnum.ConnectionNothing;
// }


public enum KellyConnectionEnum
{
    Unknown = 0 ,
    Kelly_ConnectionNothing = 1 ,
    Kelly_ConnectionString = 2
}

// public static class OperationScenarioEventExtension
public static class SimulationFieldsExtension
{
    public static void SetOperationEvent( this OutputRoot @event,  KellyConnectionEnum @enum)
    {
        @event.OperationScenarioEvent = @enum.ToString();
    }

    public static KellyConnectionEnum GetKellyEnum(this OutputRoot @event)
    {
        var parsed = TryParse<KellyConnectionEnum>(@event.OperationScenarioEvent, out var res);
        return parsed ? res : KellyConnectionEnum.Unknown;
    }
    
    
    public static void SetOperationEvent( this InputRoot @event,  KellyConnectionEnum @enum)
    {
        @event.OperationScenarioEvent = @enum.ToString();
    }

    public static KellyConnectionEnum GetKellyEnum(this InputRoot @event)
    {
        var parsed = TryParse<KellyConnectionEnum>(@event.OperationScenarioEvent, out var res);
        return parsed ? res : KellyConnectionEnum.Unknown;
    }
    
} 



public class TempValues
{
    public bool MaxValumeHornActived { set; get; }
    public bool MaxValumeAlarmState { set; get; }
    public bool TripTankAlarmState { set; get; }
    public bool TripTankHornActived { set; get; }
    public bool FlowFillIndicatorAlarmState { set; get; }
    public bool FlowFillIndicatorHornActived{set;get;}
    public double MaxVolumeLot { set; get; }
    public bool TongClose { set; get; }
    public bool TongOpen { set; get; }
    public bool RightChokOpen { set; get; }
    public bool LeftChokOpen { set; get; }
    public bool RigAir { set; get; }
    public bool ChockRate { set; get; }
    public bool ChockManValve1 { set; get; }
    public bool ChockManValve2 { set; get; }
    public bool ChockManValve3 { set; get; }
    public bool ChockManValve4 { set; get; }
    public bool ChockManValve5 { set; get; }
    public bool ChockManValve6 { set; get; }
    public bool ChockManValve7 { set; get; }
    public bool ChockManValve8 { set; get; }
    public bool ChockManValve9 { set; get; }
    public bool ChockManValve10 { set; get; }
    public bool ChockManValve11 { set; get; }
    //stand pipe valves;
    public bool StandPipeValve1 { set; get; }
    public bool StandPipeValve2 { set; get; }
    public bool StandPipeValve3 { set; get; }
    public bool StandPipeValve4 { set; get; }
    public bool StandPipeValve5 { set; get; }
    public bool StandPipeValve6 { set; get; }
    public bool StandPipeValve7 { set; get; }
    public bool StandPipeValve8 { set; get; }
    public bool StandPipeValve9 { set; get; }
    public bool StandPipeValve10 { set; get; }
    public bool StandPipeValve11 { set; get; }
    public bool StandPipeValve12 { set; get; }
    public bool StandPipeValve13 { set; get; }
    public bool StandPipeValve14 { set; get; }
    public bool StandPipeValve15 { set; get; }
    public bool StandPipeValve16 { set; get; }
    public bool StandPipeValve17 { set; get; }
    public bool StandPipeValve18 { set; get; }
    public bool StandPipeValve19 { set; get; }
    public bool StandPipeValve20 { set; get; }
    public bool StandPipeValve21 { set; get; }
    public bool StandPipeValve22 { set; get; }
    public bool StandPipeValve23 { set; get; }
    public bool StandPipeValve24 { set; get; }
    public bool StandPipeValve25 { set; get; }
    public bool StandPipeValve26 { set; get; }
    public bool StandPipeValve27 { set; get; }
    public bool StandPipeValve28 { set; get; }
    public bool StandPipeValve29 { set; get; }
    public bool StandPipeValve30 { set; get; }
    public bool StandPipeValve31 { set; get; }
    public bool StandPipeValve32 { set; get; }
    public bool StandPipeValve33 { set; get; }
    public bool StandPipeValve34 { set; get; }
    public bool StandPipeValve35 { set; get; }
    public bool StandPipeValve36 { set; get; }
    public bool StandPipeValve37 { set; get; }
    public bool StandPipeValve38 { set; get; }
    public bool StandPipeValve39 { set; get; }
    public bool StandPipeValve40 { set; get; }
    public bool StandPipeValve41 { set; get; }
    public bool StandPipeValve42 { set; get; }
    public bool StandPipeValve43 { set; get; }
    public bool StandPipeValve44 { set; get; }
    public bool StandPipeValve45 { set; get; }
    public bool StandPipeValve46 { set; get; }
    public bool StandPipeValve47 { set; get; }
    public bool StandPipeValve48 { set; get; }
    public bool StandPipeValve49 { set; get; }
    public bool StandPipeValve50 { set; get; }
    public bool StandPipeValve51 { set; get; }
    public bool StandPipeValve52 { set; get; }
    public bool StandPipeValve53 { set; get; }
    public bool StandPipeValve54 { set; get; }
    public bool StandPipeValve55 { set; get; }
    public bool StandPipeValve56 { set; get; }
    public bool StandPipeValve57 { set; get; }
    public bool StandPipeValve58 { set; get; }
    public bool StandPipeValve59 { set; get; }
    //Tanks lineup:
    public bool TanksLineValve1 { set; get; }
    public bool TanksLineValve2 { set; get; }
    public bool TanksLineValve3 { set; get; }
    public bool TanksLineValve4 { set; get; }
    public bool TanksLineValve5 { set; get; }
    public bool TanksLineValve6 { set; get; }
    public bool TanksLineValve7 { set; get; }
    public bool TanksLineValve8 { set; get; }
    public bool TanksLineValve9 { set; get; }
    public bool TanksLineValve10 { set; get; }
    public bool TanksLineValve11 { set; get; }
    public bool TanksLineValve12 { set; get; }
    public bool TanksLineValve13 { set; get; }
    public bool TanksLineValve14 { set; get; }
    public bool TanksLineValve15 { set; get; }
    public bool TanksLineValve16 { set; get; }
    public bool TanksLineValve17 { set; get; }

    public bool Pump1 { set; get; }
    public bool Pump2 { set; get; }

    public bool Pump3 { set; get; }







}
namespace SimulationInputValues
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    /// <summary>
    /// Outpuy
    /// </summary>
    public class BopControl
    {
        public double ManifoldPressureGauge { get; set; } = new();
        public double AirSupplyPressureGauge { get; set; } = new();
        public double AccumulatorPressureGauge { get; set; } = new();
        public double AnnularPressureGauge { get; set; } = new();
        public int AnnularOpenLED { get; set; } = new();
        public int AnnularCloseLED { get; set; } = new();
        public int UpperRamsOpenLED { get; set; } = new();
        public int UpperRamsCloseLED { get; set; } = new();
        public int MiddleRamsOpenLED { get; set; } = new();
        public int MiddleRamsCloseLED { get; set; } = new();
        public int KillLineOpenLED { get; set; } = new();
        public int KillLineCloseLED { get; set; } = new();
        public int ChokeLineOpenLED { get; set; } = new();
        public int ChokeLineCloseLED { get; set; } = new();
        public int LowerRamsOpenLED { get; set; } = new();
        public int LowerRamsCloseLED { get; set; } = new();
        public double AnnularStatus { get; set; } = new();
        public double UpperRamsStatus { get; set; } = new();
        public double MiddleRamsStatus { get; set; } = new();
        public double LowerRamsStatus { get; set; } = new();
        
        public double AirMasterValve { set; get; } = new();
        public double ByePassValve { get; set; } = new();
    }

    public class ChokeControl
    {
        public double StandPipePressure { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double ChokePosition { get; set; } = new();
        public double ChokePanelSPMCounter { get; set; } = new();
        public double ChokePanelTotalStrokeCounter { get; set; } = new();
        public double ChokeControlLever { set; get; }
        public int Choke1LED { get; set; } = new();
        public int Choke2LED { get; set; } = new();
        public double ChokePanelPumpSelectorSwitch { set; get; }
        public double ChokeRateControlKnob { set; get; }
        public bool ChokePanelStrokeResetSwitch { set; get; }   
        public bool ChokePanelRigAirSwitch { set; get; }
        public bool ChokeSelectorSwitch { set; get; }
    }

    public class ChokeManifold
    {
        public int HydraulicChock1 { get; set; } = new();
        public int HydraulicChock2 { get; set; } = new();
        public bool HyChock1OnProblem { get; set; } = new();
        public bool HyChock2OnProblem { get; set; } = new();
        public bool LeftManChokeOnProblem { get; set; } = new();
        public bool RightManChokeOnProblem { get; set; } = new();
    }

    /// <summary>
    /// outouts
    /// </summary>
    public class DataDisplay
    {
        public double WOBPointer { get; set; } = new();
        public double HookLoadPointer { get; set; } = new();
        public double TripTankGauge { get; set; } = new();
        public int TripTankAlarmLED { get; set; } = new();
        public int TripTankPumpLED { get; set; } = new();
        public double StandPipePressureGauge { get; set; } = new();
        public double CasingPressureGauge { get; set; } = new();
        public double MP1SPMGauge { get; set; } = new();
        public double MP2SPMGauge { get; set; } = new();
        public double ReturnLineTempGauge { get; set; } = new();
        public double RotaryTorqueGauge { get; set; } = new();
        public double RotaryRPMGauge { get; set; } = new();
        public int AcidGasDetectionLED { get; set; } = new();
        public double TotalStrokeCounter { get; set; } = new();
        public double PitGainLossGauge { get; set; } = new();
        public double MudTanksVolumeGauge { get; set; } = new();
        public int MVTAlarmLED { get; set; } = new();
        public int MVTSetAlarmSwitch { get; set; } = new();
        public double ReturnMudFlowGauge { get; set; } = new();
        public double FillStrokeCounter { get; set; } = new();
        public double MFFITotalStrokeCounter { get; set; } = new();
        public int MFFIAlarmLED { get; set; } = new();
        public int MFFISetAlarmSwitch { get; set; } = new();
        public int MFFIPumpLED { get; set; } = new();
        public double TotalWellDepth { get; set; } = new();
        public double BitDepth { get; set; } = new();
        public double HookLoad { get; set; } = new();
        public double StandPipePressure { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double MP1SPM { get; set; } = new();
        public double MP2SPM { get; set; } = new();
        public double RTTorque { get; set; } = new();
        public double RTRPM { get; set; } = new();
        public double WOP { get; set; } = new();
        public double ROP { get; set; } = new();
        public double MudWeightIn { get; set; } = new();
        public double MudWeightOut { get; set; } = new();
        public bool Buzzer1 { get; set; } = new();
        public bool Buzzer2 { get; set; } = new();
        public bool Buzzer3 { get; set; } = new();
        public bool Buzzer4 { get; set; } = new();
        
        public double MVTAlarmHigh { get; set; } = new();
        public double MVTAlarmLow { get; set; } = new();
        public double MFFIAlarmHigh { get; set; } = new();
        public double MFFIAlarmLow { get; set; } = new();
    }

    public class Drilling
    {
        public bool ParkingBrakeLed { get; set; } = new();
        public int GEN1LED { get; set; } = new();
        public int GEN2LED { get; set; } = new();
        public int GEN3LED { get; set; } = new();
        public int GEN4LED { get; set; } = new();
        public int SCR1LED { get; set; } = new();
        public int SCR2LED { get; set; } = new();
        public int SCR3LED { get; set; } = new();
        public int SCR4LED { get; set; } = new();
        public int MP1BLWR { get; set; } = new();
        public int MP2BLWR { get; set; } = new();
        public int DWBLWR { get; set; } = new();
        public int RTBLWR { get; set; } = new();
        public int PWRLIM { get; set; } = new();
        public double PWRLIMMTR { get; set; } = new();
        public double RTTorqueLimitGauge { get; set; } = new();
        public int AutoDWLED { get; set; } = new();
        public int GEN1BTNLED { get; set; } = new();
        public int GEN2BTNLED { get; set; } = new();
        public int GEN3BTNLED { get; set; } = new();
        public int GEN4BTNLED { get; set; } = new();
        public int OpenKellyCockLed { get; set; } = new();
        public int CloseKellyCockLed { get; set; } = new();
        public int OpenSafetyValveLed { get; set; } = new();
        public int CloseSafetyValveLed { get; set; } = new();
        public int IRSafetyValveLed { get; set; } = new();
        public int IRIBopLed { get; set; } = new();
        public int LatchPipeLED { get; set; } = new();
        public int UnlatchPipeLED { get; set; } = new();
        public int SwingLed { get; set; } = new();
        public int FillMouseHoleLed { get; set; } = new();
        
        
        //  -1 is ON , 0 is OFF
        public int MP1CPSwitch { get; set; }
        public int MP2Switch { get; set; }
        public int DWSwitch { get; set; }
        public int RTSwitch { get; set; }
        public int ResetWob { get; set; }
        public int TotalStrokeCounterResetSwitch { get; set; }
        
        // drilling throttle 
        public double MP1Throttle { set; get; } = 0;
        public double MP2Throttle { set; get; } = 0;
        public double RTThrottle { set; get; } = 0;
        public double DWThrottle { set; get; } = 0;
    }

    public class DrillingWatch
    {
        public double Depth { get; set; } = new();
        public double BitPosition { get; set; } = new();
        public double HookLoad { get; set; } = new();
        public double WeightOnBit { get; set; } = new();
        public double RPM { get; set; } = new();
        public double ROP { get; set; } = new();
        public double Torque { get; set; } = new();
        public double PumpPressure { get; set; } = new();
        public double SPM1 { get; set; } = new();
        public double SPM2 { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double PercentFlow { get; set; } = new();
        public double PitGainLose { get; set; } = new();
        public double PitVolume { get; set; } = new();
        public double KillMudVolume { get; set; } = new();
        public double TripTankVolume { get; set; } = new();
        public double MudWeightIn { get; set; } = new();
        public string FillVolume { get; set; }
        public double MudWeightOut { get; set; } = new();
    }

    public class Equipments
    {
        public BopControl BopControl { get; set; } = new();
        public ChokeControl ChokeControl { get; set; } = new();
        public ChokeManifold ChokeManifold { get; set; } = new();
        public DataDisplay DataDisplay { get; set; } = new();
        public Drilling Drilling { get; set; } = new();
        public Hook Hook { get; set; } = new();
        public StandPipeManifold StandPipeManifold { get; set; } = new();
        public DrillingWatch DrillingWatch { get; set; } = new();
    }

    public class Hook
    {
        public double HookHeight_S { get; set; } = new();
        public double HookHeight { get; set; } = new();
    }

    public class OutputRoot
    {
        public UnitySignalsType UnitySignals = new();
        public string OperationScenarioEvent { get; set; } = string.Empty;
        public int Step { get; set; } = new();
        public Warnings Warnings { get; set; } = new();
        public Equipments Equipments { get; set; } = new();
    }

    public class StandPipeManifold
    {
        public double StandPipeGauge1 { get; set; } = new();
        public double StandPipeGauge2 { get; set; } = new();
    }

    public class Warnings
    {
        public bool PumpWithKellyDisconnected { get; set; } = new();
        public bool PumpWithTopdriveDisconnected { get; set; } = new();
        public bool Pump1PopOffValveBlown { get; set; } = new();
        public bool Pump1Failure { get; set; } = new();
        public bool Pump2PopOffValveBlown { get; set; } = new();
        public bool Pump2Failure { get; set; } = new();
        public bool Pump3PopOffValveBlown { get; set; } = new();
        public bool Pump3Failure { get; set; } = new();
        public bool DrawworksGearsAbuse { get; set; } = new();
        public bool RotaryGearsAbuse { get; set; } = new();
        public bool HoistLineBreak { get; set; } = new();
        public bool PartedDrillString { get; set; } = new();
        public bool ActiveTankOverflow { get; set; } = new();
        public bool ActiveTankUnderVolume { get; set; } = new();
        public bool TripTankOverflow { get; set; } = new();
        public bool DrillPipeTwistOff { get; set; } = new();
        public bool DrillPipeParted { get; set; } = new();
        public bool TripWithSlipsSet { get; set; } = new();
        public bool Blowout { get; set; } = new();
        public bool UndergroundBlowout { get; set; } = new();
        public bool MaximumWellDepthExceeded { get; set; } = new();
        public bool CrownCollision { get; set; } = new();
        public bool FloorCollision { get; set; } = new();
        public bool TopdriveRotaryTableConfilict { get; set; } = new();
    }


}
namespace SimulationOutPutValues
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Accumulator
    {
        public double AccumulatorMinimumOperatingPressure { get; set; } = new();
        public double AccumulatorSystemSize { get; set; } = new();
        public double AirPlungerPumpOutput { get; set; } = new();
        public double ElectricPumpOutput { get; set; } = new();
        public int NumberOfBottels { get; set; } = new();
        public double OilTankVolume { get; set; } = new();
        public double PrechargePressure { get; set; } = new();
        public double StartPressure { get; set; } = new();
        public double StartPressure2 { get; set; } = new();
        public double StopPressure { get; set; } = new();
        public double StopPressure2 { get; set; } = new();
    }

    public class AccumulatorPressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AccumulatorPumpFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AccumulatorPumpLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AccumulatorSystemFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AccumulatorSystemLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AnnularFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AnnularLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AnnularPressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class AnnularWash
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class BitDefenition
    {
        public int BitCodeHundreds { get; set; } = new();
        public int BitCodeOnes { get; set; } = new();
        public int BitCodeTens { get; set; } = new();
        public double BitLength { get; set; } = new();
        public int BitNozzleNo { get; set; } = new();
        public double BitNozzleSize { get; set; } = new();
        public double BitSize { get; set; } = new();
        public int BitType { get; set; } = new();
        public double BitWeightPerLength { get; set; } = new();
        public bool FloatValve { get; set; } = new();
    }

    public class BitProblems
    {
        public JetWashout JetWashout { get; set; } = new();
        public PlugJets PlugJets { get; set; } = new();
        public int JetWashoutCount { get; set; } = new();
        public int PlugJetsCount { get; set; } = new();
    }

    /// <summary>
    /// inputs
    /// </summary>
    public class BopControl
    {
        public double ManifoldPressureGauge { get; set; } = new();
        public double AirSupplyPressureGauge { get; set; } = new();
        public double AccumulatorPressureGauge { get; set; } = new();
        public double AnnularPressureGauge { get; set; } = new();
        public int AnnularOpenLED { get; set; } = new();
        public int AnnularCloseLED { get; set; } = new();
        public int UpperRamsOpenLED { get; set; } = new();
        public int UpperRamsCloseLED { get; set; } = new();
        public int MiddleRamsOpenLED { get; set; } = new();
        public int MiddleRamsCloseLED { get; set; } = new();
        public int KillLineOpenLED { get; set; } = new();
        public int KillLineCloseLED { get; set; } = new();
        public int ChokeLineOpenLED { get; set; } = new();
        public int ChokeLineCloseLED { get; set; } = new();
        public int LowerRamsOpenLED { get; set; } = new();
        public int LowerRamsCloseLED { get; set; } = new();
        public double AnnularStatus { get; set; } = new();
        public double UpperRamsStatus { get; set; } = new();
        public double MiddleRamsStatus { get; set; } = new();
        public double LowerRamsStatus { get; set; } = new();
        public double AirMasterValve { set; get; } = new();
        public double KillLineValve { set; get; } = 0;
        public double ChokeLineValve { set; get; } = 0;
        public double UpperRamsValve { set; get; } = 0;
        public double LowerRamsValve { set; get; } = 0;
        public double AnnularValve { set; get; } = 0;

        public double ByePassValve { get; set; } = new();
        public double MiddleRamsValve { set; get; } = 0;
    }

    public class BopProblems
    {
        public AnnularWash AnnularWash { get; set; } = new();
        public AnnularFail AnnularFail { get; set; } = new();
        public AnnularLeak AnnularLeak { get; set; } = new();
        public UpperRamWash UpperRamWash { get; set; } = new();
        public UpperRamFail UpperRamFail { get; set; } = new();
        public UpperRamLeak UpperRamLeak { get; set; } = new();
        public MiddleRamWash MiddleRamWash { get; set; } = new();
        public MiddleRamFail MiddleRamFail { get; set; } = new();
        public MiddleRamLeak MiddleRamLeak { get; set; } = new();
        public LowerRamWash LowerRamWash { get; set; } = new();
        public LowerRamFail LowerRamFail { get; set; } = new();
        public LowerRamLeak LowerRamLeak { get; set; } = new();
        public AccumulatorPumpFail AccumulatorPumpFail { get; set; } = new();
        public AccumulatorPumpLeak AccumulatorPumpLeak { get; set; } = new();
        public AccumulatorSystemFail AccumulatorSystemFail { get; set; } = new();
        public AccumulatorSystemLeak AccumulatorSystemLeak { get; set; } = new();
    }

    public class BopStack
    {
        public double AboveAnnularHeight { get; set; } = new();
        public double AnnularPreventerClose { get; set; } = new();
        public double AnnularPreventerHeight { get; set; } = new();
        public double AnnularPreventerOpen { get; set; } = new();
        public double AnnularStringDrag { get; set; } = new();
        public double BlindRamClose { get; set; } = new();
        public double BlindRamHeight { get; set; } = new();
        public double BlindRamOpen { get; set; } = new();
        public double ChokeClose { get; set; } = new();
        public double ChokeLineId { get; set; } = new();
        public double ChokeLineLength { get; set; } = new();
        public double ChokeOpen { get; set; } = new();
        public double GroundLevel { get; set; } = new();
        public double KillClose { get; set; } = new();
        public double KillHeight { get; set; } = new();
        public double KillOpen { get; set; } = new();
        public double LowerRamClose { get; set; } = new();
        public double LowerRamHeight { get; set; } = new();
        public double LowerRamOpen { get; set; } = new();
        public double RamStringDrag { get; set; } = new();
        public double UpperRamClose { get; set; } = new();
        public double UpperRamHeight { get; set; } = new();
        public double UpperRamOpen { get; set; } = new();
    }

    public class CasingLinerChoke
    {
        public double CasingDepth { get; set; } = new();
        public double CasingId { get; set; } = new();
        public double CasingOd { get; set; } = new();
        public double CasingWeight { get; set; } = new();
        public double CasingCollapsePressure { get; set; } = new();
        public double CasingTensileStrength { get; set; } = new();
        public double LinerTopDepth { get; set; } = new();
        public double LinerLength { get; set; } = new();
        public double LinerId { get; set; } = new();
        public double LinerOd { get; set; } = new();
        public double LinerWeight { get; set; } = new();
        public double LinerCollapsePressure { get; set; } = new();
        public double LinerTensileStrength { get; set; } = new();
        public double OpenHoleId { get; set; } = new();
        public double OpenHoleLength { get; set; } = new();
    }

    public class CasingPressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class CasingPressure2
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class CementPumpBlowPopOffValve
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class CementPumpPowerFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ChokeControl
    {
        public double StandPipePressure { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double ChokePosition { get; set; } = new();
        public double ChokePanelSPMCounter { get; set; } = new();
        public double ChokePanelTotalStrokeCounter { get; set; } = new();
        public int Choke1LED { get; set; } = new();
        public int Choke2LED { get; set; } = new();
        public bool ChokePanelRigAirSwitch { set; get; }
        public bool ChokePanelStrokeResetSwitch { set; get; }
        public double ChokeControlLever { set; get; }    
        public double ChokePanelPumpSelectorSwitch { set; get; }
        public bool ChokeSelectorSwitch { set; get; }
    }

    public class ChokeManifold
    {
        public int HydraulicChock1 { get; set; } = new();
        public int HydraulicChock2 { get; set; } = new();
        public bool HyChock1OnProblem { get; set; } = new();
        public bool HyChock2OnProblem { get; set; } = new();
        public bool LeftManChokeOnProblem { get; set; } = new();
        public bool RightManChokeOnProblem { get; set; } = new();
        public bool ChokeManifoldValve1 { set; get; }
        public bool ChokeManifoldValve2 { set; get; }
        public bool ChokeManifoldValve3 { set; get; }
        public bool ChokeManifoldValve4 { set; get; }
        public bool ChokeManifoldValve5 { set; get; }
        public bool ChokeManifoldValve6 { set; get; }
        public bool ChokeManifoldValve7 { set; get; }
        public bool ChokeManifoldValve8 { set; get; }
        public bool ChokeManifoldValve9 { set; get; }
        public bool ChokeManifoldValve10 { set; get; }
        public bool ChokeManifoldValve11 { set; get; }
        public bool ChokeManifoldValve12 { set; get; }
        public bool ChokeManifoldValve13 { set; get; }
        public double LeftManualChoke { set; get; }
        public double RightManualChoke { set; get; }

    }

    public class ChokePanelAirFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ChokePosition
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ChokeProblems
    {
        public HydraulicChoke1Plugged HydraulicChoke1Plugged { get; set; } = new();
        public HydraulicChoke1Fail HydraulicChoke1Fail { get; set; } = new();
        public HydraulicChoke1Washout HydraulicChoke1Washout { get; set; } = new();
        public HydraulicChoke2Plugged HydraulicChoke2Plugged { get; set; } = new();
        public HydraulicChoke2Fail HydraulicChoke2Fail { get; set; } = new();
        public HydraulicChoke2Washout HydraulicChoke2Washout { get; set; } = new();
        public ManualChoke1Plugged ManualChoke1Plugged { get; set; } = new();
        public ManualChoke1Fail ManualChoke1Fail { get; set; } = new();
        public ManualChoke1Washout ManualChoke1Washout { get; set; } = new();
        public ManualChoke2Plugged ManualChoke2Plugged { get; set; } = new();
        public ManualChoke2Fail ManualChoke2Fail { get; set; } = new();
        public ManualChoke2Washout ManualChoke2Washout { get; set; } = new();
        public ChokePanelAirFail ChokePanelAirFail { get; set; } = new();
        public int ManualChoke1PluggedPercent { get; set; } = new();
        public int HydraulicChoke2PluggedPercent { get; set; } = new();
        public int HydraulicChoke1PluggedPercent { get; set; } = new();
        public int ManualChoke2PluggedPercent { get; set; } = new();
    }

    public class ClutchDisengage
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ClutchEngage
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Configuration
    {
        public StringConfiguration StringConfiguration { get; set; } = new();
        public List<Formation> Formations { get; set; } = new();
        public Reservoir Reservoir { get; set; } = new();
        public Shoe Shoe { get; set; } = new();
        public Accumulator Accumulator { get; set; } = new();
        public BopStack BopStack { get; set; } = new();
        public Hoisting Hoisting { get; set; } = new();
        public Power Power { get; set; } = new();
        public Pumps Pumps { get; set; } = new();
        public RigSize RigSize { get; set; } = new();
        public CasingLinerChoke CasingLinerChoke { get; set; } = new();
        public Path Path { get; set; } = new();
        public Mud Mud { get; set; } = new();
    }

    /// <summary>
    /// inputs 
    /// </summary>
    public class DataDisplay
    {
        public double WOBPointer { get; set; } = new();
        public double HookLoadPointer { get; set; } = new();
        public double TripTankGauge { get; set; } = new();
        public int TripTankAlarmLED { get; set; } = new();
        public int TripTankPumpLED { get; set; } = new();
        public double StandPipePressureGauge { get; set; } = new();
        public double CasingPressureGauge { get; set; } = new();
        public double MP1SPMGauge { get; set; } = new();
        public double MP2SPMGauge { get; set; } = new();
        public double ReturnLineTempGauge { get; set; } = new();
        public double RotaryTorqueGauge { get; set; } = new();
        public double RotaryRPMGauge { get; set; } = new();
        public int AcidGasDetectionLED { get; set; } = new();
        public double TotalStrokeCounter { get; set; } = new();
        public double PitGainLossGauge { get; set; } = new();
        public double MudTanksVolumeGauge { get; set; } = new();
        public int MVTAlarmLED { get; set; } = new();
        public int MVTSetAlarmSwitch { get; set; } = new();
        public double ReturnMudFlowGauge { get; set; } = new();
        public double FillStrokeCounter { get; set; } = new();
        public double MFFITotalStrokeCounter { get; set; } = new();
        public int MFFIAlarmLED { get; set; } = new();
        public int MFFISetAlarmSwitch { get; set; } = new();
        public int MFFIPumpLED { get; set; } = new();
        public double TotalWellDepth { get; set; } = new();
        public double BitDepth { get; set; } = new();
        public double HookLoad { get; set; } = new();
        public double StandPipePressure { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double MP1SPM { get; set; } = new();
        public double MP2SPM { get; set; } = new();
        public double RTTorque { get; set; } = new();
        public double RTRPM { get; set; } = new();
        public double WOP { get; set; } = new();
        public double ROP { get; set; } = new();
        public double MudWeightIn { get; set; } = new();
        public double MudWeightOut { get; set; } = new();
        public bool Buzzer1 { get; set; } = new();
        public bool Buzzer2 { get; set; } = new();
        public bool Buzzer3 { get; set; } = new();
        public bool Buzzer4 { get; set; } = new();
        
        public double MVTAlarmHigh { get; set; } = new();
        public double MVTAlarmLow { get; set; } = new();
        public double MFFIAlarmHigh { get; set; } = new();
        public double MFFIAlarmLow { get; set; } = new();
        
        public int TotalStrokeCounterResetSwitch { get; set; }
        
        public int ResetWob { get; set; }
    }

    public class Degasser
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Desander
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Desilter
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Drilling
    {
        public double MP1Throttle { set; get; } = 0;
        public double MP2Throttle { set; get; } = 0;
        public double RTThrottle { set; get; } = 0;
        public double DWThrottle { set; get; } = 0;
        public bool ParkingBrakeLed { get; set; } = new();
        public int GEN1LED { get; set; } = new();
        public int GEN2LED { get; set; } = new();
        public int GEN3LED { get; set; } = new();
        public int GEN4LED { get; set; } = new();
        public int SCR1LED { get; set; } = new();
        public int SCR2LED { get; set; } = new();
        public int SCR3LED { get; set; } = new();
        public int SCR4LED { get; set; } = new();
        public int MP1BLWR { get; set; } = new();
        public int MP2BLWR { get; set; } = new();
        public int DWBLWR { get; set; } = new();
        public int RTBLWR { get; set; } = new();
        public int PWRLIM { get; set; } = new();
        public double PWRLIMMTR { get; set; } = new();
        public double RTTorqueLimitGauge { get; set; } = new();
        public int AutoDWLED { get; set; } = new();
        public int GEN1BTNLED { get; set; } = new();
        public int GEN2BTNLED { get; set; } = new();
        public int GEN3BTNLED { get; set; } = new();
        public int GEN4BTNLED { get; set; } = new();
        public int OpenKellyCockLed { get; set; } = new();
        public int CloseKellyCockLed { get; set; } = new();
        public int OpenSafetyValveLed { get; set; } = new();
        public int CloseSafetyValveLed { get; set; } = new();
        public int IRSafetyValveLed { get; set; } = new();
        public int IRIBopLed { get; set; } = new();
        public int LatchPipeLED { get; set; } = new();
        public int UnlatchPipeLED { get; set; } = new();
        public int SwingLed { get; set; } = new();
        public int FillMouseHoleLed { get; set; } = new();

        //  -1 is ON , 0 is OFF
        public int MP1CPSwitch { get; set; }
        public int MP2Switch { get; set; }
        public int DWSwitch { get; set; }
        public int RTSwitch { get; set; }

    }

    public class DrillingWatch
    {
        public double Depth { get; set; } = new();
        public double BitPosition { get; set; } = new();
        public double HookLoad { get; set; } = new();
        public double WeightOnBit { get; set; } = new();
        public double RPM { get; set; } = new();
        public double ROP { get; set; } = new();
        public double Torque { get; set; } = new();
        public double PumpPressure { get; set; } = new();
        public double SPM1 { get; set; } = new();
        public double SPM2 { get; set; } = new();
        public double CasingPressure { get; set; } = new();
        public double PercentFlow { get; set; } = new();
        public double PitGainLose { get; set; } = new();
        public double PitVolume { get; set; } = new();
        public double KillMudVolume { get; set; } = new();
        public double TripTankVolume { get; set; } = new();
        public double MudWeightIn { get; set; } = new();
        public double FillVolume { get; set; } = new();
        public double MudWeightOut { get; set; } = new();
    }

    public class DrillPipePressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class DrillStemsProblems
    {
        public StringDragIncrease StringDragIncrease { get; set; } = new();
        public StringTorqueIncrease StringTorqueIncrease { get; set; } = new();
        public StringTorqueFluctuation StringTorqueFluctuation { get; set; } = new();
        public double StringDragIncreaseTime { get; set; } = new();
        public double StringTorqueIncreaseTime { get; set; } = new();
    }

    public class Equipments
    {
        public BopControl BopControl { get; set; } = new();
        public ChokeControl ChokeControl { get; set; } = new();
        public ChokeManifold ChokeManifold { get; set; } = new();
        public DataDisplay DataDisplay { get; set; } = new();
        public Drilling Drilling { get; set; } = new();
        public Hook Hook { get; set; } = new();
        public StandPipeManifold StandPipeManifold { get; set; } = new();
        public DrillingWatch DrillingWatch { get; set; } = new();        
    }
   
    public class GaugesProblems
    {
        public WeightIndicator WeightIndicator { get; set; } = new();
        public RotaryRpm RotaryRpm { get; set; } = new();
        public RotaryTorque RotaryTorque { get; set; } = new();
        public StandPipePressure StandPipePressure { get; set; } = new();
        public CasingPressure CasingPressure { get; set; } = new();
        public Pump1Strokes Pump1Strokes { get; set; } = new();
        public Pump2Strokes Pump2Strokes { get; set; } = new();
        public ReturnLineTemperature ReturnLineTemperature { get; set; } = new();
        public TripTank TripTank { get; set; } = new();
        public PitGainLoss PitGainLoss { get; set; } = new();
        public MudTankVolume MudTankVolume { get; set; } = new();
        public ReturnMudFlow ReturnMudFlow { get; set; } = new();
        public TorqueLimit TorqueLimit { get; set; } = new();
        public PowerLimit PowerLimit { get; set; } = new();
        public AccumulatorPressure AccumulatorPressure { get; set; } = new();
        public ManifoldPressure ManifoldPressure { get; set; } = new();
        public AnnularPressure AnnularPressure { get; set; } = new();
        public RigAirPressure RigAirPressure { get; set; } = new();
        public StandPipe1 StandPipe1 { get; set; } = new();
        public StandPipe2 StandPipe2 { get; set; } = new();
        public DrillPipePressure DrillPipePressure { get; set; } = new();
        public ChokePosition ChokePosition { get; set; } = new();
        public CasingPressure2 CasingPressure2 { get; set; } = new();
    }

    public class Gen1
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Gen2
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Gen3
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Gen4
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Hoisting
    {
        public double DrillingLineBreakingLoad { get; set; } = new();
        public int DriveType { get; set; } = new();
        public double KellyWeight { get; set; } = new();
        public int NumberOfLine { get; set; } = new();
        public double TopDriveWeight { get; set; } = new();
        public double TravelingBlockWeight { get; set; } = new();
    }

    public class HoistingProblems
    {
        public MotorFail MotorFail { get; set; } = new();
        public ClutchEngage ClutchEngage { get; set; } = new();
        public ClutchDisengage ClutchDisengage { get; set; } = new();
    }

    public class Hook
    {
        public double HookHeight_S { get; set; } = new();
        public double HookHeight { get; set; } = new();
    }

    public class HydraulicChoke1Fail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class HydraulicChoke1Plugged
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class HydraulicChoke1Washout
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class HydraulicChoke2Fail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class HydraulicChoke2Plugged
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class HydraulicChoke2Washout
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class JetWashout
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Kick
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class KickProblems
    {
        public Kick Kick { get; set; } = new();
        public int FluidType { get; set; } = new();
        public int FlowRate { get; set; } = new();
        public int OverBalancePressure { get; set; } = new();
        public bool IsAutoMigrationRateSelected { get; set; } = new();
        public double AutoMigrationRate { get; set; } = new();
    }

    public class LostCirculation
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class LostProblems
    {
        public LostCirculation LostCirculation { get; set; } = new();
        public double FlowRate { get; set; } = new();
    }

    public class LowerRamFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class LowerRamLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class LowerRamWash
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManifoldPressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke1Fail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke1Plugged
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke1Washout
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke2Fail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke2Plugged
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ManualChoke2Washout
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class MiddleRamFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class MiddleRamLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class MiddleRamWash
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class MotorFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Mud
    {
        public int ActiveMudType { get; set; } = new();
        public int ActiveRheologyModel { get; set; } = new();
        public double ActiveMudVolume { get; set; } = new();
        public double ActiveMudVolumeGal { get; set; } = new();
        public double ActiveDensity { get; set; } = new();
        public double ActivePlasticViscosity { get; set; } = new();
        public double ActiveYieldPoint { get; set; } = new();
        public double ActiveThetaThreeHundred { get; set; } = new();
        public double ActiveThetaSixHundred { get; set; } = new();
        public int ReserveMudType { get; set; } = new();
        public double ReserveMudVolume { get; set; } = new();
        public double ReserveMudVolumeGal { get; set; } = new();
        public double ReserveDensity { get; set; } = new();
        public double ReservePlasticViscosity { get; set; } = new();
        public double ReserveYieldPoint { get; set; } = new();
        public double ReserveThetaThreeHundred { get; set; } = new();
        public double ReserveThetaSixHundred { get; set; } = new();
        public double ActiveTotalTankCapacity { get; set; } = new();
        public double ActiveTotalTankCapacityGal { get; set; } = new();
        public double ActiveSettledContents { get; set; } = new();
        public double ActiveSettledContentsGal { get; set; } = new();
        public double ActiveTotalContents { get; set; } = new();
        public double ActiveTotalContentsGal { get; set; } = new();
        public bool ActiveAutoDensity { get; set; } = new();
        public double InitialTripTankMudVolume { get; set; } = new();
        public double InitialTripTankMudVolumeGal { get; set; } = new();
        public double PedalFlowMeter { get; set; } = new();
    }

    public class MudTankVolume
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class MudTreatmentProblems
    {
        public Degasser Degasser { get; set; } = new();
        public ShaleShaker ShaleShaker { get; set; } = new();
        public Desander Desander { get; set; } = new();
        public Desilter Desilter { get; set; } = new();
    }

    public class OtherProblems
    {
        public RigAlarm RigAlarm { get; set; } = new();
        public RigWaterSupply RigWaterSupply { get; set; } = new();
        public RigAir RigAir { get; set; } = new();
        public Gen1 Gen1 { get; set; } = new();
        public Gen2 Gen2 { get; set; } = new();
        public Gen3 Gen3 { get; set; } = new();
        public Gen4 Gen4 { get; set; } = new();
        public Scr1 Scr1 { get; set; } = new();
        public Scr2 Scr2 { get; set; } = new();
        public Scr3 Scr3 { get; set; } = new();
        public Scr4 Scr4 { get; set; } = new();
    }

    public class OverideTorqueLimit
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Path
    {
        public List<PathItem> Items { get; set; } = new();
    }
    public class PathItem
    {
        public int HoleType { get; set; }
        public double Angle { get; set; }
        public double Length { get; set; }
        public double FinalAngle { get; set; }
        public double TotalLength { get; set; }
        public double MeasuredDepth { get; set; }
        public double TotalVerticalDepth { get; set; }
    }
    public class PitGainLoss
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class PlugJets
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Power
    {
        public double CementPump { get; set; } = new();
        public double Drawworks { get; set; } = new();
        public double GeneratorPowerRating { get; set; } = new();
        public double MudPump1 { get; set; } = new();
        public double MudPump2 { get; set; } = new();
        public int NumberOfgenerators { get; set; } = new();
        public double RotaryTable { get; set; } = new();
        public double TopDrive { get; set; } = new();
    }

    public class PowerLimit
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Problems
    {
        public BitProblems BitProblems { get; set; } = new();
        public BopProblems BopProblems { get; set; } = new();
        public ChokeProblems ChokeProblems { get; set; } = new();
        public DrillStemsProblems DrillStemsProblems { get; set; } = new();
        public GaugesProblems GaugesProblems { get; set; } = new();
        public HoistingProblems HoistingProblems { get; set; } = new();
        public KickProblems KickProblems { get; set; } = new();
        public LostProblems LostProblems { get; set; } = new();
        public MudTreatmentProblems MudTreatmentProblems { get; set; } = new();
        public OtherProblems OtherProblems { get; set; } = new();
        public PumpProblems PumpProblems { get; set; } = new();
        public RotaryProblems RotaryProblems { get; set; } = new();
    }

    public class Pump1BlowPopOffValve
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Pump1PowerFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Pump1Strokes
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Pump2BlowPopOffValve
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Pump2PowerFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Pump2Strokes
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class PumpProblems
    {
        public Pump1PowerFail Pump1PowerFail { get; set; } = new();
        public Pump1BlowPopOffValve Pump1BlowPopOffValve { get; set; } = new();
        public Pump2PowerFail Pump2PowerFail { get; set; } = new();
        public Pump2BlowPopOffValve Pump2BlowPopOffValve { get; set; } = new();
        public CementPumpPowerFail CementPumpPowerFail { get; set; } = new();
        public CementPumpBlowPopOffValve CementPumpBlowPopOffValve { get; set; } = new();
    }

    public class Pumps
    {
        public double MudPump1LinerDiameter { get; set; } = new();
        public double MudPump1Stroke { get; set; } = new();
        public double MudPump1MechanicalEfficiency { get; set; } = new();
        public double MudPump1VolumetricEfficiency { get; set; } = new();
        public double MudPump1Output { get; set; } = new();
        public double MudPump1OutputBblStroke { get; set; } = new();
        public double MudPump1Maximum { get; set; } = new();
        public double MudPump1ReliefValvePressure { get; set; } = new();
        public double MudPump2LinerDiameter { get; set; } = new();
        public double MudPump2Stroke { get; set; } = new();
        public double MudPump2MechanicalEfficiency { get; set; } = new();
        public double MudPump2VolumetricEfficiency { get; set; } = new();
        public double MudPump2Output { get; set; } = new();
        public double MudPump2OutputBblStroke { get; set; } = new();
        public double MudPump2Maximum { get; set; } = new();
        public double MudPump2ReliefValvePressure { get; set; } = new();
        public double CementPumpLinerDiameter { get; set; } = new();
        public double CementPumpStroke { get; set; } = new();
        public double CementPumpMechanicalEfficiency { get; set; } = new();
        public double CementPumpVolumetricEfficiency { get; set; } = new();
        public double CementPumpOutput { get; set; } = new();
        public double CementPumpOutputBblStroke { get; set; } = new();
        public double CementPumpMaximum { get; set; } = new();
        public double CementPumpReliefValvePressure { get; set; } = new();
        public bool MudPump1ReliefValveIsSet { get; set; } = new();
        public bool MudPump2ReliefValveIsSet { get; set; } = new();
        public bool CementPumpReliefValveIsSet { get; set; } = new();
        public bool ManualPumpPower { get; set; } = new();
        public bool Valve1 { get; set; } = new();
        public bool Valve2 { get; set; } = new();
        public bool Valve3 { get; set; } = new();
        public bool Valve4 { get; set; } = new();
        public bool Valve5 { get; set; } = new();
        public double MudPump1MaximumPressure { set; get; } = new();
        public double MudPump1PumpRateChange { set; get; } = new();
        public double MudPump1SurfaceLineLength { set; get; } = new();
        public double MudPump1DelayToShutdown { set; get; }=new();
        public double MudPump2MaximumPressure { set; get; } = new();
        public double MudPump2PumpRateChange { set; get; } = new();
        public double MudPump2SurfaceLineLength { set; get; } = new();
        public double MudPump2DelayToShutdown { set; get; } = new();
        public double CementPumpMaximumPressure { set; get; } = new();
        public double CementPumpPumpRateChange { set; get; } = new();
        public double CementPumpSurfaceLineLength { set; get; } = new();
        public double CementPumpDelayToShutdown { set; get; } = new();
        public double MudPump1VolumetricOutput { set; get; } = new();
        public double MudPump2VolumetricOutput { set; get; } = new();
        public double CementPumpVolumetricOutput { set; get; } = new();

    }
    public class Formation
    {
        public double Abrasiveness { get; set; }
        public double Drillablity { get; set; }
        public double PorePressureGradient { get; set; }
        public double Thickness { get; set; }
        public double ThresholdWeight { get; set; }
        public double Top { get; set; }
    }
    public class Reservoir
    {
        public double AutoMigrationRate { get; set; } = new();
        public double FluidGradient { get; set; } = new();
        public int FluidType { get; set; } = new();
        public double FluidViscosity { get; set; } = new();
        public int FormationNo { get; set; } = new();
        public double FormationPermeability { get; set; } = new();
        public double FormationTop { get; set; } = new();
        public double GeothermalGradient { get; set; } = new();
        public bool InactiveInflux { get; set; } = new();
        public bool IsAutoMigrationRateSelected { get; set; } = new();
        public bool MakeKickSinglePacket { get; set; } = new();
        public double PressureGradient { get; set; } = new();
    }

    public class ReturnLineTemperature
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ReturnMudFlow
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class RigAir
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class RigAirPressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class RigAlarm
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class RigSize
    {
        public int RigType { get; set; } = new();
        public double CrownHeight { get; set; } = new();
        public double MonkeyBoandHeight { get; set; } = new();
        public double RigFloorHeight { get; set; } = new();
    }

    public class RigWaterSupply
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class InputRoot
    {
        public UnitySignalsType UnitySignals = new();
        public string OperationScenarioEvent { get; set; } = string.Empty;
        public int status { get; set; } = new();
        public int speed { get; set; } = new();
        public int endstep { get; set; } = new();
        public Configuration Configuration { get; set; } = new();
        public Problems Problems { get; set; } = new();
        public Equipments Equipments { get; set; } = new();
    }

    public class RotaryProblems
    {
        public MotorFail MotorFail { get; set; } = new();
        public OverideTorqueLimit OverideTorqueLimit { get; set; } = new();
    }

    public class RotaryRpm
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class RotaryTorque
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Scr1
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Scr2
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Scr3
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Scr4
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class ShaleShaker
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class Shoe
    {
        public double Breakdown { get; set; } = new();
        public int FormationNo { get; set; } = new();
        public double FracturePropagation { get; set; } = new();
        public bool InactiveFracture { get; set; } = new();
        public double LeakOff { get; set; } = new();
        public double ShoeDepth { get; set; } = new();
    }

    public class StandPipe1
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class StandPipe2
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class StandPipeManifold
    {
        public double StandPipeGauge1 { get; set; } = new();
        public double StandPipeGauge2 { get; set; } = new();
        public bool StandPipeManifoldValve1 { set; get; }
        public bool StandPipeManifoldValve2 { set; get; }
        public bool StandPipeManifoldValve3 { set; get; }
        public bool StandPipeManifoldValve4 { set; get; }
        public bool StandPipeManifoldValve5 { set; get; }
        public bool StandPipeManifoldValve6 { set; get; }
        public bool StandPipeManifoldValve7 { set; get; }
        public bool StandPipeManifoldValve8 { set; get; }

        public bool StandPipeManifoldValve9 { set; get; }

        public bool StandPipeManifoldValve10 { set; get; }
        public bool StandPipeManifoldValve11 { set; get; }
        public bool StandPipeManifoldValve12 { set; get; }
        public bool StandPipeManifoldValve13 { set; get; }
        public bool StandPipeManifoldValve14 { set; get; }
        public bool StandPipeManifoldValve15 { set; get; }




    }

    public class StandPipePressure
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class StringConfiguration
    {
        public List<StringConfigItem> StringConfigurationItems { get; set; } = new();
        public BitDefenition BitDefenition { get; set; } = new();
    }
    public class StringConfigItem
    {
        public double ComponentLength { get; set; }
        public int ComponentType { get; set; }
        public string Grade { get; set; }
        public double LengthPerJoint { get; set; }
        public double NominalId { get; set; }
        public double NominalOd { get; set; }
        public double NominalToolJointOd { get; set; }
        public double NumberOfJoint { get; set; }
        public double WeightPerLength { get; set; }
    
    }

    public class StringDragIncrease
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class StringTorqueFluctuation
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class StringTorqueIncrease
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class TorqueLimit
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class TripTank
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class UpperRamFail
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class UpperRamLeak
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class UpperRamWash
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

    public class WeightIndicator
    {
        public int ProblemType { get; set; } = new();
        public int StatusType { get; set; } = new();
        public double Value { get; set; } = new();
        public double DueValue { get; set; } = new();
    }

}
