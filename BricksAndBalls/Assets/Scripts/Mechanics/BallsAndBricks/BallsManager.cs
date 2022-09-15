using BricksAndBalls.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    /// <summary>
    /// Manages all current balls, when there are no more balls, proceeds to next level.
    /// </summary>
    public class BallsManager : MonoBehaviour
    {
        [Header("Non-Tweakable Properties")]
        [SerializeField]
        List<Ball> balls = new List<Ball>();

        private void Awake()
        {
            Main.Instance.ballsManager = this;
        }


        internal void DestroyBall(Ball ballToDestroy)
        {
            balls.Remove(ballToDestroy);
            Destroy(ballToDestroy.gameObject);
            if(balls.Count ==0)
            {
                Main.Instance.gameManager.NextLayer();
            }
        }

        internal void RegisterBall(Ball ball)
        {
            balls.Add(ball);
        }
    }
}