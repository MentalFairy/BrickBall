using BricksAndBalls.Core;
using BricksAndBalls.Gameplay;

namespace BricksAndBalls.Mechanics.PowerUps
{
    /// <summary>
    /// Adds one extra ball to the players next shoot.
    /// </summary>
    public class ExtraBall : PowerUp
    {
        protected override void Execute()
        {
            Simulation.Schedule<AddExtraBall>();
        }
    }
}
