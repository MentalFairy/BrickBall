using Skrptr.Utility;
using UnityEngine;

namespace Skrptr.Input
{
    /// <summary>
    /// Class to store the target Gameobject that the selection will move onto based on the direction of the last selected element. (Hope that made sense)
    /// </summary>
    [System.Serializable]
    public class SkrptrNeighbour
    {
        /// <summary>
        /// Points which input will trigger the selection of the new target.
        /// </summary>
        [EnumToggle]
        public NeighbourDirection direction;

        /// <summary>
        /// If the above direction input is received, this target will be selected.
        /// </summary>
        public GameObject target;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public SkrptrNeighbour(NeighbourDirection direction, GameObject target)
        {
            this.direction = direction;
            this.target = target;
        }
    }
}