using Microsoft.AspNetCore.Components;

namespace Panel16.Pages.Components;

public class VolumeComponent:RangedComponent
{
    private bool IsMouseDownOnButtons { get; set; }
    
    public void IncreaseValue()
    {
        
        Value += ((To - From) / 100);
        Value = Math.Round(Value, 2);
        if (Value > To)
            Value = To;
    }
    public void DecreaseValue()
    {
        Value -=((To - From) / 100);
        Value = Math.Round(Value, 2);
        if (Value < From)
            Value = From;
    }

    public enum VolumeButtons
    {
        PlusButton,
        MinusButton
    }

    public void MouseUpOnButtons()
    {
        IsMouseDownOnButtons = false;
    }

    public void MouseDownOnButtons(VolumeButtons mb)
    {
        IsMouseDownOnButtons = true;
        HandleMouseDownButton(mb);
    }

    public async Task HandleMouseDownButton(VolumeButtons mb)
    {
       
    }
}