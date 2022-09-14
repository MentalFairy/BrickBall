using BricksAndBalls.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Non-Tweakable Properties")]
        [SerializeField]
        SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

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

        private void DecrementHitPoints()
        {
            hitPointsText.text = $"{--hitPoints}";
            if (hitPoints == 0)
                Destroy(gameObject);
        }
    }
}