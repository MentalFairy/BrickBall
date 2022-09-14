using BricksAndBalls.Utils;
using Skrptr;
using Skrptr.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Ui
{
    public class SkrptrAction_MultiplyScore : SkrptrAction
    {
        SkrptrEvent eventToTriggerOn = SkrptrEvent.Click;
        [SerializeField]
        int multiplyValue;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            Main.Instance.gameStats.playerScore *= multiplyValue;
            UiMain.Instance.panelHuD.UpdateScore();
        }
    }
}
