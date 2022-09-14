using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Core
{
    public class SimulationTicker : Singleton<SimulationTicker>
    {
        void Update()
        {
            if (Instance == this)
            {
                Simulation.Tick();
            }
        }
    }
}
