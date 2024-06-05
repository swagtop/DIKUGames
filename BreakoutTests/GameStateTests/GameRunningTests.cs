namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;
using Breakout.GameStates;
using Breakout.LevelHandling;

public class GameRunningTests {
    private GameRunning gameRunning;
    private GameEventBus eventBus;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        eventBus = BreakoutBus.GetBus();
        gameRunning = GameRunning.GetInstance();
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        GameRunning.GetInstance();

        gameRunning.ProcessEvent(new GameEvent {
            Message = "LOAD_LEVEL",
            ObjectArg1 = (object)(new Level())
        });
        
        Queue<Level> levelQueue = new Queue<Level>();
        levelQueue.Enqueue(new Level());
        levelQueue.Enqueue(new Level());
        levelQueue.Enqueue(new Level());
        levelQueue.Enqueue(new Level());
        levelQueue.Enqueue(new Level());
        gameRunning.ProcessEvent(new GameEvent {
            Message = "QUEUE_LEVELS",
            ObjectArg1 = (object)(levelQueue)
        });

        gameRunning.ResetState();
        //gameRunning.UpdateState();   Cannot be tested because of timer.
        gameRunning.RenderState();
        //gameRunning.IterateBalls();  Cannot be tested because of timer.

        //gameRunning.EndLevel();
        gameRunning.EndGame("WON");
        gameRunning.EndGame("LOST");
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.A);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.D);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Space);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.P);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.R);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.B);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Left);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.A);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Right);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.D);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Space);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.P);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.R);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.B);

        gameRunning.FlushQueue();

        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        /*
        for (int i = 0; i < 1000; i++) {
            gameRunning.UpdateState();
        }
        Assert.Pass();
        */
        // Cannot be tested because of timer.
        Assert.Inconclusive();
    }
}