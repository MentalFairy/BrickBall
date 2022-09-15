using BricksAndBalls.Core;
using BricksAndBalls.Utils;

namespace BricksAndBalls.Gameplay
{
    /// <summary>
    /// Fired to add one extra ball to the player shooter.
    /// </summary>
    public class AddExtraBall : Simulation.Event<AddExtraBall>
    {
        public override void Execute()
        {
            Main.Instance.gameStats.playerBallsCount++;
        }
    }
}
