using BricksAndBalls.Core;
using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Gameplay
{
    public class AddExtraBall : Simulation.Event<AddExtraBall>
    {
        public override void Execute()
        {
            Main.Instance.gameStats.playerBallsCount++;
        }
    }
}
