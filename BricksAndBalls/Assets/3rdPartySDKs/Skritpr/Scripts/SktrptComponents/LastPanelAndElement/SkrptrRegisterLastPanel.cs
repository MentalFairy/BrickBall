using Skrptr.Elements;
using Skrptr.Input;
using Skrptr.Utility;
using System.Collections.Generic;

namespace Skrptr.Components.LastPanelAndElement
{
    [System.Serializable]
    public class SkrptrRegisterLastPanel : SkrptrAction
    {
        [EnumFlags]
        public SkrptrEvent eventsToRegisterPanelOn;

        public SkrptrElement lastSelectedElement;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if((eventsToRegisterPanelOn & currentSkrptrEvent)== currentSkrptrEvent)
            {
                if (SkrptrMain.lastRegisteredPanels == null)
                    SkrptrMain.lastRegisteredPanels = new List<SkrptrRegisterLastPanel>();
                SkrptrMain.lastRegisteredPanels.Add(this);
            }
        }
    }
}
