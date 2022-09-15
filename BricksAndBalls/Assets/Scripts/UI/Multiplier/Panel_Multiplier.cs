using BricksAndBalls.Utils;
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