using BricksAndBalls.Utils;
using Skrptr.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.Ui
{
    public class Panel_HuD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Text highscoreText;

        private void Awake()
        {
            UiMain.Instance.panelHuD = this;
        }

        internal void UpdateScore()
        {
            highscoreText.text = $"{Main.Instance.gameStats.playerScore}";
            highscoreText.GetComponent<SkrptrElement>().Show();
        }
    }
}