namespace Breakout;

using DIKUArcade;
using DIKUArcade.Events;

/// <summary>
/// BreakoutBus is the singleton wrapper for the GameEventBus class. Makes sure that there is only
/// one event bus. This event bus serves as the mediator between many classes, and should be the 
/// only instance of an event bus ever in use in this game.
/// </summary>
public static class BreakoutBus {
    private static GameEventBus eventBus = new GameEventBus();

    /// <summary> GetEventBus method for Singleton purposes. </summary>
    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus;
    }
}
