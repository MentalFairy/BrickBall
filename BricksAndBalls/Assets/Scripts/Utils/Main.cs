using BricksAndBalls.Mechanics;
using BricksAndBalls.Utils;
using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Utils
{
    public class Main : Singleton<Main>
    {
        [Header("References")]
        public BrickSpawner brickSpawner;
        public GameStats gameStats;
        public DragAndShoot dragAndShooter;
        public ScaleableColliders scaleableColliders;
        internal BallsManager ballsManager;
        internal GameManager gameManager;
    }
}