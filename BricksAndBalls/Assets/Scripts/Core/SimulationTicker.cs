using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Core
{
    public class SimulationTicker : MonoBehaviour
    {
        void Update()
        {
            Simulation.Tick();
        }
    }
}
