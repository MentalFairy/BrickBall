using Skrptr.Elements;
using UnityEngine;

namespace Skrptr.Components
{
    /// <summary>
    /// Abstract class that calls its Execute function on any child whenever a specific event occurs on any SkrptrElement.cs
    /// </summary>
    public abstract class SkrptrAction : MonoBehaviour
    {
        /// <summary>
        /// Event called when a specific action took place on the attached skrptr Element.
        /// </summary>
        /// <param name="currentSkrptrEvent">Current event that just occured in the event system.</param>
        public abstract void Execute(SkrptrEvent currentSkrptrEvent);

        /// <summary>
        /// Used to OnValidate Method call.
        /// </summary>
        protected bool isInit = false;

        /// <summary>
        /// Checks for a skrptr element, adds one if it doesn't exist.
        /// </summary>
        public virtual void OnValidate()
        {
            #if UNITY_EDITOR
            if (!isInit)
            {
                if (GetComponent<SkrptrElement>() == null)
                {
                    SkrptrElement elem = gameObject.AddComponent<SkrptrElement>();
                
                    UnityEditorInternal.ComponentUtility.MoveComponentUp(elem);
                   
                }
                isInit = true;
            }
            #endif
        }
    }
  
}