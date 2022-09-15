using BricksAndBalls.Ui;
using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Utils
{
    public class UiMain : Singleton<UiMain>
    {
        [SerializeField]
        internal Panel_HuD panelHuD;
        [SerializeField]
        internal Panel_Multiplier panelMultiplier;
        [SerializeField]
        internal Panel_HighsScores panelHighScores;
    }
}
