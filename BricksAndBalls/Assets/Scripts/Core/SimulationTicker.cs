using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Core
{
    /// <summary>
    /// Ensures the simulation actually executes.
    /// </summary>
    public class SimulationTicker : MonoBehaviour
    {
        void Update()
        {
            Simulation.Tick();
        }
    }
}
