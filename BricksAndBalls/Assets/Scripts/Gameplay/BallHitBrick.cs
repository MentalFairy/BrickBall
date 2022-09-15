using BricksAndBalls.Core;
using BricksAndBalls.Utils;

namespace BricksAndBalls.Gameplay
{
    /// <summary>
    /// Fired when A ball hits a brick.
    /// </summary>
    public class BallHitBrick : Simulation.Event<BallHitBrick>
    {
        /// <summary>
        /// Value by which local player score will be incremented.
        /// </summary>
        public int incrementValue = 0;
        public override void Execute()
        {
            //Play audio if you want or smth
            Main.Instance.gameStats.playerScore += incrementValue;
            UiMain.Instance.panelHuD?.UpdateScore();
        }
    }
}