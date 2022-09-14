using BricksAndBalls.Utils;
using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class GameStats : MonoBehaviour
    {
        public int playerBallsCount = 3;
        public int playerScore = 0;
        private void Awake()
        {
            Main.Instance.gameStats = this;
        }
    }
}