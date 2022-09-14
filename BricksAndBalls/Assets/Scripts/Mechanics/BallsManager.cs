using BricksAndBalls.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
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