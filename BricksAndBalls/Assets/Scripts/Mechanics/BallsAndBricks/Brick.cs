using BricksAndBalls.Core;
using BricksAndBalls.Gameplay;
using BricksAndBalls.Utils;
using TMPro;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class Brick : MonoBehaviour
    {
        [Header("Tweakables")]
        public int hitPoints = 0;
        [SerializeField]
        TextMeshPro hitPointsText;
        [SerializeField]
        int scoreIncrement = 1;

        [Header("Non-Tweakable Properties")]
        [SerializeField]
        SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Initialize the brick with values.
        /// </summary>
        public void Init(int hitPoints, Color color)
        {
            this.hitPoints = hitPoints;
            spriteRenderer.color = color;
            hitPointsText.text = $"{hitPoints}";
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case UnityTags.BallTag:
                    {
                        DecrementHitPoints();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Decrements the hitpoints, and updates label.
        /// </summary>
        private void DecrementHitPoints()
        {
            hitPointsText.text = $"{--hitPoints}";
            var v = Simulation.Schedule<BallHitBrick>();
            v.incrementValue = scoreIncrement;
            if (hitPoints == 0)
            {
                if (transform.parent.childCount > 1)
                    Destroy(gameObject);
                else
                    Destroy(transform.parent.gameObject);
            }
        }
    }
}