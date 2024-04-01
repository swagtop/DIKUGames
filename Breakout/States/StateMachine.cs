using System;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;
using Breakout.States;

namespace Breakout.States;
public class StateMachine : IGameEventProcessor {
    private static StateMachine instance;
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        ActiveState = TestingGrounds.GetInstance();
        //ActiveState = MainMenu.GetInstance();
        //GameRunning.GetInstance();
        //GamePaused.GetInstance();
    }

    public static StateMachine GetInstance() {
        return instance ?? (instance = new StateMachine());
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