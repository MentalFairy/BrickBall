using BricksAndBalls.Utils;
using Skrptr.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        int maxLayersBeforeLoss = 7;
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
            //Check end game
            if (Main.Instance.brickSpawner.GameOver(maxLayersBeforeLoss))
            {
                UiMain.Instance.panelMultiplier.GetComponent<SkrptrElement>().Unlock();
            }
            else
            {
                Main.Instance.brickSpawner.SpawnLayer();
                Main.Instance.dragAndShooter.mayShoot = true;
            }
        }
    }
}