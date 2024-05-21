using System;
using System.Reflection;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;

namespace Breakout.GameStates;
public class StateMachine : IGameEventProcessor
{
    private static StateMachine instance = new StateMachine();
    public IGameState ActiveState { get; private set; }
    public StateMachine()
    {
        MainMenu.GetInstance().ResetState();
        ChooseLevel.GetInstance().ResetState();
        GameRunning.GetInstance().ResetState();
        GamePaused.GetInstance().ResetState();
        GameOver.GetInstance().ResetState();
        GameWon.GetInstance().ResetState();

        ActiveState = MainMenu.GetInstance();

        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
    }

    public static StateMachine GetInstance()
    {
        return StateMachine.instance;
    }

    public void SwitchState(GameStateType stateType)
    {
        switch (stateType)
        {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                GameRunning.GetInstance().DumpQueue();
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.ChooseLevel:
                ChooseLevel.GetInstance().ResetState();
                ActiveState = ChooseLevel.GetInstance();
                break;
            case GameStateType.GameOver:
                ActiveState = GameOver.GetInstance();
                break;
            case GameStateType.GameWon:
                ActiveState = GameWon.GetInstance();
                break;
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {stateType}");
        }
    }

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;

        if (gameEvent.Message == "CHANGE_STATE")
        {
            SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
        }
    }
}