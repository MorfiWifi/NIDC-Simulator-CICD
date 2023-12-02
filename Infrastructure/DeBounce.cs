using System;
using System.Collections.Generic;
using System.Timers;


namespace Infrastructure;

/// <summary>
/// Debounced event (function) call to its last value and prevent 
/// flooding in short period 
/// </summary>
public static class DeBounce
{

    private static readonly Dictionary<string, Action> dictActions = new();
    private static readonly Dictionary<string, Timer> dictTimers = new();


    /// <summary>
    /// Invoke only the last Action within given time period
    /// </summary>
    /// <param name="Identifier">actions Id (override last one in this group)</param>
    /// <param name="ac">action body</param>
    /// <param name="delay">default 100ms (rec for client side)</param>
    public static void CallAction(string Identifier, Action ac, int delay = 100)
    {
        dictActions[Identifier] = ac;
        if (dictTimers.ContainsKey(Identifier)) return;
        var timer = new Timer(delay);

        timer.Elapsed += (_, _) =>
        {
            Action action = null;
            if (dictActions.TryGetValue(Identifier, out var dictAction))
                action = dictAction;

            action?.Invoke();
            // destroy timer
            timer.Dispose();
            timer = null;

            if (dictTimers.ContainsKey(Identifier))
                dictTimers.Remove(Identifier);

            //race condition! cautious
            if (dictActions.ContainsKey(Identifier))
                dictActions.Remove(Identifier);
        };

        timer.Start();
        dictTimers[Identifier] = timer;
    }

}