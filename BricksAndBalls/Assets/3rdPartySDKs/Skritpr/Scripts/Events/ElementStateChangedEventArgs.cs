using Skrptr.Elements;

namespace Skrptr.Events
{
    public class ElementStateChangedEventArgs
    {
        /// <summary>
        /// Element on which the state changed
        /// </summary>
        public readonly SkrptrElement SkrptrElement;

        /// <summary>
        /// The new state which just got triggered.
        /// </summary>
        public readonly SkrptrEvent StateTriggered;
        
        /// <summary>
        /// The Input Type used to trigger the change.
        /// </summary>
        public readonly SkrptrInputType InputType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skrptrElement"></param>
        /// <param name="stateTriggered"></param>
        /// <param name="inputType">SkrptrElement Component on which the event has / will happen.</param>
        public ElementStateChangedEventArgs(SkrptrElement skrptrElement, SkrptrEvent stateTriggered, SkrptrInputType inputType)
        {
            SkrptrElement = skrptrElement;
            StateTriggered = stateTriggered;
            InputType = inputType;
        }
        /// <summary>
        /// Overriden ToString() for ease of debugging. 
        /// </summary>
        /// <returns>Returns the object in following string format : Format: "SkrptrElement: '{SkrptrElement.gameObject.name}' - StateTriggered: '{StateTriggered}' - InputType: '{InputType}'"</returns>
        public override string ToString()
        {
            return $"SkrptrElement: '{SkrptrElement.gameObject.name}' - StateTriggered: '{StateTriggered}' - InputType: '{InputType}'";
        }
    }
}