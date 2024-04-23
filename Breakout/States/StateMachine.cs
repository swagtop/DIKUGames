using System;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;
using Breakout.States;

namespace Breakout.States;
public class StateMachine : IGameEventProcessor {
    private static StateMachine instance = null;
    private GameEventBus eventBus = BreakoutBus.GetBus();
    public IGameState ActiveState { get; private set; }
    public StateMachine() { 
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        
        ActiveState = MainMenu.GetInstance();
        
        eventBus.Subscribe(GameEventType.PlayerEvent, GameRunning.GetInstance());
        eventBus.Subscribe(GameEventType.GameStateEvent, GameRunning.GetInstance());
        
        GamePaused.GetInstance();
        ChooseLevel.GetInstance();
    }

    public static StateMachine GetInstance() {
        if (instance == null) {
            instance = new StateMachine();
        }
        return instance;
    }

    public void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.ChooseLevel:
                ActiveState = ChooseLevel.GetInstance();
                break;
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {stateType}");
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
            if (gameEvent.Message == "CHANGE_STATE") {
                if (gameEvent.StringArg2 == "RESET_GAME") {
                    Console.WriteLine("Resetting game");
                    GameRunning.GetInstance().ResetState();
                }
                SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
            }
        }
    }
}