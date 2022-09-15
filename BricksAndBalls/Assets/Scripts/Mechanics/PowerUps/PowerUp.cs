using BricksAndBalls.Utils;
using UnityEngine;

namespace BricksAndBalls.Mechanics.PowerUps
{
    /// <summary>
    /// Abstract implementation of a power up - inherit from this to implement custom ones.
    /// </summary>
    public abstract class PowerUp : MonoBehaviour
    {
       
        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case UnityTags.BallTag:
                    {
                        Execute();
                        Destroy(gameObject);
                        break;
                    }
                default:
                    break;
            }
        }

        protected abstract void Execute();
    }
}
