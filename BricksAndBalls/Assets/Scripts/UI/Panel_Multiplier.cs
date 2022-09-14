using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Ui
{
    public class Panel_Multiplier : MonoBehaviour
    {
        private void Awake()
        {
            UiMain.Instance.panelMultiplier = this;
        }
    }
}