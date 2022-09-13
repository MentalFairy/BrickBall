using Skrptr.Input;
using UnityEngine;

namespace Skrptr.Components.Triggers
{
    /// <summary>
    /// Contains data required to chain link a SkrptrEvent to other elements.
    /// </summary>
    [System.Serializable]
    public class TriggerTargets
    {
        /// <summary>
        /// Event on which this trigger will fire.
        /// </summary>
        public SkrptrEvent onTriggerEvent;

        /// <summary>
        /// Target on which the event shall be further fire / linked with.
        /// </summary>
        public GameObject targetGO;

        /// <summary>
        /// Event to be fired on the target - doesn't have to be the same!
        /// </summary>
        public SkrptrEvent triggerEvent;

        /// <summary>
        /// Delay after which the even will be chained / linked.
        /// </summary>
        public float delay = 0;
    }
    [System.Serializable]
    public class TriggerTargetsDelayedBetween:TriggerTargets
    {
        /// <summary>
        /// Delay between each two elements in case of a multiple trigger.
        /// </summary>
        public float delayBetween;
    }
}