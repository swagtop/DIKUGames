using DIKUArcade;
using DIKUArcade.Events;

namespace Breakout;
public static class BreakoutBus {
    private static GameEventBus eventBus = new GameEventBus();

    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus ?? (eventBus = new GameEventBus());
    }
}