using Skrptr.Components.LastPanelAndElement;
using Skrptr.Elements;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr.Input
{
    /// <summary>
    /// Class used to debug the static class SkrptrMain in Inspector. Remove in release builds if you wish to save every bit of performance.
    /// </summary>
    public class SkrptrMainDebugger : MonoBehaviour
    {      
        public SkrptrElement selectedElem, hoveredElem, lastHoveredElem,lastSelectedElem;
        public List<SkrptrRegisterLastPanel> lastRegisteredPanel;
        public SkrptrInputType inputType;
        // Update is called once per frame
        void Update()
        {
            lastSelectedElem = SkrptrMain.lastSelectedElem;
            selectedElem = SkrptrMain.selectedElem;
            hoveredElem = SkrptrMain.hoveredElem;
            lastHoveredElem = SkrptrMain.lastHoveredElem;
            inputType = SkrptrMain.inputType;
            lastRegisteredPanel = SkrptrMain.lastRegisteredPanels;
        }
    }
}
