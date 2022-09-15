using BricksAndBalls.Utils;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    /// <summary>
    /// Destroy the ball if it comes in contact with this GO attached collider.
    /// </summary>
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