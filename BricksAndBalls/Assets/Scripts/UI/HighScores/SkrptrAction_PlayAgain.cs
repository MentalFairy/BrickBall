using BricksAndBalls.Mechanics;
using BricksAndBalls.Utils;
using Skrptr;
using Skrptr.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BricksAndBalls.Ui
{
    public class SkrptrAction_PlayAgain : SkrptrAction
    {
        [SerializeField]
        SkrptrEvent eventToTriggerOn = SkrptrEvent.Click;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if ((eventToTriggerOn & currentSkrptrEvent) == currentSkrptrEvent)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}