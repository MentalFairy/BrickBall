using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.Ui
{
    public class Ui_HighScore : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        Text textUsername;
        [SerializeField]
        Text textScore;
        [SerializeField]
        Text textIndex;

        internal void Init(string username, int score, int index)
        {
            textUsername.text = username;
            textScore.text = $"{score}";
            textIndex.text = $"{index}";

            if(username == "You")
            {
                GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
