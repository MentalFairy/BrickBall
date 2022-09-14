using BricksAndBalls.Core;
using BricksAndBalls.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics.PowerUps
{
    public class ExtraBall : PowerUp
    {
        protected override void Execute()
        {
            Simulation.Schedule<AddExtraBall>();
        }
    }
}
