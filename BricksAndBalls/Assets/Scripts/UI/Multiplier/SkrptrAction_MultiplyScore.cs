using BricksAndBalls.Mechanics;
using BricksAndBalls.Utils;
using Skrptr;
using Skrptr.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BricksAndBalls.Ui
{
    public class SkrptrAction_MultiplyScore : SkrptrAction
    {
        [SerializeField]
        SkrptrEvent eventToTriggerOn = SkrptrEvent.Click;
        [SerializeField]
        int multiplyValue;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if ((eventToTriggerOn & currentSkrptrEvent) == currentSkrptrEvent)
            {
                MultiplyAndUpdateScores();
            }
        }

        private void MultiplyAndUpdateScores()
        {
            var scores = HighScores.LoadScores(100);
            var localScore = scores.FirstOrDefault(s => s.username == "You");
            if (localScore == null)
            {
                HighScores.AddScore("You", Main.Instance.gameStats.playerScore);              
            }   
            else
            {
                HighScores.IncrementScoreOfPlayer("You", Main.Instance.gameStats.playerScore);
            }
            scores = HighScores.GetScores();
            HighScores.MultiplyScores(multiplyValue);
            HighScores.SaveScores();

            UiMain.Instance.panelHuD.UpdateScore();
        }
    }
}
