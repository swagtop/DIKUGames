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
        foreach (GameStateType stateType in Enum.GetValues(typeof(GameStateType)))
        {
            // Get the class type corresponding to the enum value
            Type stateClass = Type.GetType(stateType.ToString());
            // Ensure the class exists and has a GetInstance method
            if (stateClass != null)
            {
                // Dynamically create an instance of the state class
                IGameState stateInstance = Activator.CreateInstance(stateClass) as IGameState;
                // Reset the state
                stateInstance.ResetState();
            }
        }

        ActiveState = MainMenu.GetInstance();

        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
    }

    public static StateMachine GetInstance()
    {
        return instance;
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
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.ChooseLevel:
                ChooseLevel.GetInstance().ResetState();
                ActiveState = ChooseLevel.GetInstance();
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