using Skrptr.Elements;
using Skrptr.Utility;
using UnityEngine;

namespace Skrptr.Components.LastPanelAndElement
{
    public class SkrptrRegisterLastElemOfPanel : SkrptrAction
    {
        /// <summary>
        /// Event for which the panel should register as last element of a panel.
        /// </summary>
        [EnumFlags]
        public SkrptrEvent eventsToRegisterElemOn;

        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if ((eventsToRegisterElemOn & currentSkrptrEvent) == currentSkrptrEvent)
            {
                Debug.Log("Registering: " + this.gameObject.name + " on " + GetComponentInParent<SkrptrRegisterLastPanel>().gameObject.name);
                GetComponentInParent<SkrptrRegisterLastPanel>().lastSelectedElement = this.GetComponent<SkrptrElement>();
            }
        }
    }
}