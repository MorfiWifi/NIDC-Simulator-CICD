using System;
using System.Collections.Generic;
using System.Timers;


namespace Infrastructure;

/// <summary>
/// Deboumd event (function) call to its last value and prevent 
/// fludding in short period 
/// </summary>
public static class DeBounce
{

    private static readonly Dictionary<string, Action> dictActions = new();
    private static readonly Dictionary<string, Timer> dictTimers = new();


    /// <summary>
    /// Invoke only the last Action witin given time period
    /// </summary>
    /// <param name="Identifier">actions Id (override last one in this group)</param>
    /// <param name="ac">action body</param>
    /// <param name="delay">default 100ms (rec for client sied)</param>
    public static void CallAction(string Identifier, Action ac, int delay = 100)
    {
        dictActions[Identifier] = ac;
        if (!dictTimers.ContainsKey(Identifier))
        {
            var timer = new Timer(delay);

            timer.Elapsed += (_, _) =>
            {
                Action action = null;
                if (dictActions.ContainsKey(Identifier))
                    action = dictActions[Identifier];

                action?.Invoke();
                // detroy timer
                timer.Dispose();
                timer = null;

                if (dictTimers.ContainsKey(Identifier))
                    dictTimers.Remove(Identifier);

                //rece condition! cousion
                if (dictActions.ContainsKey(Identifier))
                    dictActions.Remove(Identifier);
            };

            timer.Start();
            dictTimers[Identifier] = timer;
        }
    }

}