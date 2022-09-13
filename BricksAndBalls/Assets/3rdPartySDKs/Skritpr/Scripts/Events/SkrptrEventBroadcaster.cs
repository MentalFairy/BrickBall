using Skrptr.Utility;
using System;
using UnityEngine;

namespace Skrptr.Events
{
    /// <summary>
    /// The broadcaster contains all definitions and Invokes of events happening within Skrptr.
    /// Once I find more cases in which it would make sense to fire more vents, I will add them here.
    /// </summary>
    public class SkrptrEventBroadcaster : Singleton<SkrptrEventBroadcaster>
    {
        /// <summary>
        /// Event which is fired when a elements state changes. 
        /// </summary>
        public event EventHandler<ElementStateChangedEventArgs> ElementStateChanged;

        /// <summary>
        /// Invoke for ElementStateChange event.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnElementStateChanged(ElementStateChangedEventArgs e)
        {
         //   Debug.Log($"Invoked : {e.ToString()}");
            ElementStateChanged?.Invoke(this, e);
        }
    }
}