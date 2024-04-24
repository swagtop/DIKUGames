using System;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;
using Breakout.States;

namespace Breakout.States;
public class StateMachine : IGameEventProcessor {
    private static StateMachine instance = new StateMachine();
    public IGameState ActiveState { get; private set; }
    public StateMachine() { 
        ActiveState = MainMenu.GetInstance();
        
        ChooseLevel.GetInstance();
        GameRunning.GetInstance();
        GamePaused.GetInstance();
        
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
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
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;
        
        if (gameEvent.Message == "CHANGE_STATE") {
            SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
        }
    }
}