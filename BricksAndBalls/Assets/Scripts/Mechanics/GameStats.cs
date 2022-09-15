using BricksAndBalls.Utils;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class GameStats : MonoBehaviour
    {
        /// <summary>
        /// Indicates how many balls will shoot upon release.
        /// </summary>
        public int playerBallsCount = 3;

        /// <summary>
        /// Contains current score of local player in this game.
        /// </summary>
        public int playerScore = 0;
        private void Awake()
        {
            Main.Instance.gameStats = this;
        }
    }
}