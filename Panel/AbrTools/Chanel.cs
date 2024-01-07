using Models.Config;

namespace AbrTools;

public static class ChannelNames
{
    public const string DEFAULT_CHANNEL = "default";
    public const string SIMULATION_CHANNEL = "simulation";
    public const string UPDATE_OUTPUTS = "Update-Outputs";
    public const string UPDATE_INPUTS = "update-model";
    public const string BROADCAST_UPDATE_INPUTS = "broadcast-update-model";
    public const string TOGGLE_SLIPS = "Toggle-SilipsStand";
    public const string TOGGLE_TONG = "Toggle-Tong";
}

public class Chanel<T> : IChannel<T>
{

    public virtual void SendMessage(string chanel, T message)
    {
        OnNewMessage?.Invoke(chanel , message);
    }

    public virtual void SendMessage(T message)
    {
        OnNewMessage?.Invoke(ChannelNames.DEFAULT_CHANNEL , message);
    }

    public event Action<string ,T>? OnNewMessage;
}

public class SimulationChanel : Chanel<SimulationModel>
{
    public override void SendMessage(SimulationModel message)
    {
        base.SendMessage(ChannelNames.SIMULATION_CHANNEL,message);
    }
}

public interface IChannel<T>
{

    /// <summary>
    /// send message to specific chanel
    /// </summary>
    /// <param name="chanel">chanel name</param>
    /// <param name="message">message OBJ</param>
    public void SendMessage(string chanel, T message);
    
    /// <summary>
    /// send message to default channel
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(T message);
    
    /// <summary>
    /// new message from channel
    /// </summary>
    public event  Action<string,T> OnNewMessage;
}
