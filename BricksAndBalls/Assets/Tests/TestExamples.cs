using System.Collections;
using System.Collections.Generic;
using BricksAndBalls.Core;
using BricksAndBalls.Gameplay;
using BricksAndBalls.Mechanics;
using BricksAndBalls.Utils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestExamples
{
    [UnityTest]
    public IEnumerator TestExamplesWithEnumeratorPasses()
    {
        GameObject sim = new GameObject();
        sim.AddComponent<SimulationTicker>();
        sim.AddComponent<GameStats>();

        var e = Simulation.Schedule<BallHitBrick>();
        e.incrementValue = 10;        
        yield return null;

        Assert.IsTrue(e.incrementValue == Main.Instance.gameStats.playerScore);
    }
}
