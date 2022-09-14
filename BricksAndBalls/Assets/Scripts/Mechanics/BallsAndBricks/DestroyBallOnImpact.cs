using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class DestroyBallOnImpact : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case UnityTags.BallTag:
                    {
                        Main.Instance.ballsManager.DestroyBall(collision.collider.gameObject.GetComponent<Ball>());
                        break;
                    }
                default:
                    break;
            }
        }
    }
}