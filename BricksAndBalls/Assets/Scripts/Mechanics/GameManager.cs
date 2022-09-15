using BricksAndBalls.Utils;
using Skrptr.Elements;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Represents how many layers can exist before it ends the game with a loss.
        /// </summary>
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

        /// <summary>
        /// Loads next layer if game did not meet end conditions.
        /// </summary>
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