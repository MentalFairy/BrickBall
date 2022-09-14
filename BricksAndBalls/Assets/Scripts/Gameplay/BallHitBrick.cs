using BricksAndBalls.Core;
using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Gameplay
{
    public class BallHitBrick : Simulation.Event<BallHitBrick>
    {
        public int incrementValue = 0;
        public override void Execute()
        {
            //Play audio if you want or smth
            Main.Instance.gameStats.playerScore += incrementValue;
            UiMain.Instance.panelHuD.UpdateScore();
        }
    }
}