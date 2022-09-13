using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Skrptr.Utility;

namespace Skrptr.Input
{
    /// <summary>
    /// Class that acts as a data storage for directional input (Keyboard / Joystic / Other HMI interaction types)
    /// </summary>
    public class SkrptrKeyboardMapper : MonoBehaviour
    {
        public List<SkrptrNeighbour> neighbours;

        [EnumFlags]
        public SkrptrEvent returnToLastPanelEventsCallback;
        /// <summary>
        /// Checks for duplicates and removes if any.
        /// </summary>
        private void Start()
        {
            foreach (NeighbourDirection direction in Enum.GetValues(typeof(NeighbourDirection)))
            {
                if (direction != NeighbourDirection.None)
                {
                    int foundCount = 0;
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        if ((neighbours[i].direction & direction) == direction)
                        {
                            foundCount++;
                            if (foundCount >= 2)
                            {
                                neighbours[i].direction = neighbours[i].direction ^ direction;
                                Debug.LogWarning("Element: " + gameObject.name + " has been found having two different targetting neighoburs for direction : " + direction.ToString() + " on index: " + i + ". Removing duplicate.");
                            }
                        }
                    }
                }
            }
        }
    }
   
}