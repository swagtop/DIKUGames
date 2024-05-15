namespace Breakout;

using DIKUArcade;
using DIKUArcade.Events;

public static class BreakoutBus {
    private static GameEventBus eventBus = new GameEventBus();

    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus;
    }
}
