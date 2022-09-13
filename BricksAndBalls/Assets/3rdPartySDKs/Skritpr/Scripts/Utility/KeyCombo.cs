using System.Collections.Generic;
using UnityEngine;
#if ODYN_IS_IMPORTED
using Sirenix.OdinInspector;
#endif

namespace Skrptr.Utility
{
    [System.Serializable]
    // Class that allowws fetching Input from Key Combos unlike Unity's Input Manager 
    public class KeyCombo
    {
        public List<KeyCode> keys;
        public bool hasBeenPressed;

        public KeyCombo(List<KeyCode> keys)
        {
            this.keys = keys;
        }
        /// <summary>
        /// Similar to Input.GetKeyDown(KeyCode.Key);
        /// </summary>
        /// <returns></returns>
        public bool IsKeyComboDown()
        {
            //Combo key checker
            if (keys.Count > 1)
            {
                if (UnityEngine.Input.GetKey(keys[0]))
                {
                    for (int i = 1; i < keys.Count; i++)
                    {
                        if (i != keys.Count - 1)
                        {
                            if (!UnityEngine.Input.GetKey(keys[i]))
                            {
                                hasBeenPressed = false;
                                return false;

                            }
                        }
                        //Last Key
                        else
                        {
                            if (!UnityEngine.Input.GetKeyDown(keys[i]))
                            {
                                hasBeenPressed = false;
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                hasBeenPressed = true;
                return true;
            }
            // Single Key 
            else
            {
                if (UnityEngine.Input.GetKeyDown(keys[0]))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// Similar to Input.GetKey(KeyCode.SomeKey);
        /// </summary>
        /// <returns></returns>
        public bool IsKeyComboHeldDown()
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (!UnityEngine.Input.GetKey(keys[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Overriden ToString to write return all keys in the form of a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string returnString = "KeyCombo: ";
            for (int i = 0; i < keys.Count; i++)
            {
                returnString += keys[i].ToString() + "-";
            }
            returnString = returnString.Remove(returnString.Length - 1);

            return returnString;
        }
    }
}