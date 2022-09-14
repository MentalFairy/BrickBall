using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Main.Instance.gameManager = this;
        }
        private void Start()
        {
            NextLayer();
        }
        internal void NextLayer()
        {
            Main.Instance.brickSpawner.SpawnLayer();
            Main.Instance.dragAndShooter.mayShoot = true;
        }
    }
}