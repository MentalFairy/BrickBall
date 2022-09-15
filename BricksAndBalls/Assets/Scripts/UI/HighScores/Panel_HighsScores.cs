using BricksAndBalls.Mechanics;
using BricksAndBalls.Utils;
using Skrptr;
using Skrptr.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BricksAndBalls.Ui
{
    public class Panel_HighsScores : SkrptrAction
    {
        [Header("References")]
        [SerializeField]
        Transform highScoresContent;
        [SerializeField]
        GameObject highscorePrefab;

        [SerializeField]
        SkrptrEvent eventToLoadScoresOn = SkrptrEvent.Unlock;

        private void Awake()
        {
            UiMain.Instance.panelHighScores = this;
            //HighScores.RegenerateCleanScores(100);
        }
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if ((eventToLoadScoresOn & currentSkrptrEvent) == currentSkrptrEvent)
            {
                SpawnHighscores();                
            }
        }

        private void SpawnHighscores()
        {
            var scores = HighScores.GetScores().OrderByDescending(s => s.score).ToList();
            int i = 0;
            foreach (var score in scores)
            {
                var highScore = Instantiate(highscorePrefab, highScoresContent).GetComponent<Ui_HighScore>();
                highScore.Init(score.username, score.score, i++);
            }
        }
    }
}
