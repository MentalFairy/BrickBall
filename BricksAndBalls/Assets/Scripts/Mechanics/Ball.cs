using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class Ball : MonoBehaviour
    {
        [Header("Tweakable Values")]
        public float speed = 50;
        
        [Header("Non-tweakable Proeprties")]
        [SerializeField]
        Rigidbody2D rigidBody2D;


        private void Awake()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        internal void Throw(Vector2 direction)
        {
            rigidBody2D.AddForce(direction * speed);
        }
    }
}