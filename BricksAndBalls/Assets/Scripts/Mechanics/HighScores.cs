using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{
    /// <summary>
    /// Wrapper class to avoid using dictionaries because of read-only values in foreach loops
    /// </summary>
    public class HighScore
    {
        public string username;
        public int score;

        public HighScore(string username, int score)
        {
            this.username = username;
            this.score = score;
        }
    }

    public static class HighScores 
    {
        /// <summary>
        /// Used to store as keys in player prefs procedurally.
        /// Definitely not a solution for thousands of players!
        /// </summary>
        const string playerScoreKey = "player";
        /// <summary>
        /// Contains all currently loaded scores.
        /// </summary>
        static List<HighScore> scores = new List<HighScore>();

        /// <summary>
        /// Loads all scores and generates random scores if asked to load more than it has.
        /// </summary>
        /// <param name="playersToLoad">How many players should load.</param>
        /// <returns>The loaded scores. </returns>
        internal static List<HighScore> LoadScores(int playersToLoad)
        {
            scores.Clear();
            for (int i = 0; i < playersToLoad; i++)
            {
                string playerKey = $"{playerScoreKey}{i}";
                if (PlayerPrefs.HasKey(playerKey))
                {
                    string loadedScore = PlayerPrefs.GetString(playerKey);
                    var splitString = loadedScore.Split('-');
                    scores.Add(new HighScore(splitString[0], int.Parse(splitString[1])));
                }
                else
                {
                    scores.Add(new HighScore(RandomString(5), Random.Range(0, playersToLoad)));
                }
            }
            return scores;
        }


        /// <summary>
        /// FOR DEBUGGIN / TESTING ONLY
        /// Clears , regenerates random scores, and saves them.
        /// </summary>
        internal static void RegenerateCleanScores(int playersToLoad)
        {
            scores.Clear();
            for (int i = 0; i < playersToLoad; i++)
            {
                scores.Add(new HighScore(RandomString(5), Random.Range(0, 500)));
            }
            SaveScores();
            LoadScores(100);
        }

        /// <summary>
        /// Adds a new score to the current loaded list.
        /// </summary>
        internal static void AddScore(string username, int score)
        {
            scores.Add(new HighScore(username, score));
        }

        /// <summary>
        /// Saves all scores to player prefs.
        /// </summary>
        /// <returns>True, if successful.</returns>
        internal static bool SaveScores()
        {
            int i = 0;
            scores.ForEach(s => PlayerPrefs.SetString($"{playerScoreKey}{i++}", $"{s.username}-{s.score}"));
            PlayerPrefs.Save();
            return true;
        }

        /// <summary>
        /// Multiplies all scores in current load by value given.
        /// </summary>
        internal static void MultiplyScores(int multiplier)
        {
            foreach (var score in scores)
            {
                score.score *= multiplier;
            }
        }

        /// <summary>
        /// Attempts to increment the score of requested player by value, if it exists in current load.
        /// </summary>
        internal static void IncrementScoreOfPlayer(string username, int value)
        {
            var score = scores.FirstOrDefault(s => s.username == "You");
            if (score == null)
                return;
            else
                score.score += value;
        }

        internal static List<HighScore> GetScores()
        {
            return scores;
        }

        #region Random Generation

        //Used for random string generation
        private static System.Random random = new System.Random();
        /// <summary>
        /// Generates a random string of requested length.
        /// </summary>
        /// <returns>The generated string</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}