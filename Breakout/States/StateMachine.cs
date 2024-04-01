using Breakout;
using DIKUArcade.Events;
using DIKUArcade.State;
using System;

namespace Breakout.States;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    private static StateMachine instance = null;
    public StateMachine() {
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        ActiveState = MainMenu.GetInstance();
        //GameRunning.GetInstance();
        //GamePaused.GetInstance();
    }

    public static StateMachine GetInstance() {
        if (instance == null) {
            instance = new StateMachine();
        }
        return instance;
    }

    public void SwitchState(GameStateType stateType) {
        /*
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
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {stateType}");
        }
        */
    }

    public void ProcessEvent(GameEvent gameEvent) {
        /*
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
            if (gameEvent.Message == "CHANGE_STATE") {
                if (gameEvent.StringArg2 == "RESET_GAME") {
                    Console.WriteLine("Resetting game");
                    GameRunning.GetInstance().ResetState();
                }
                SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
            }
        }
        */
    }
}