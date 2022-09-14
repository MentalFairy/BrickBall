using BricksAndBalls.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics.PowerUps
{
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
