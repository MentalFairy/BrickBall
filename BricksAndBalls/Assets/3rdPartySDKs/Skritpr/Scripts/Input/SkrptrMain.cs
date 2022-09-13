using Skrptr.Components.LastPanelAndElement;
using Skrptr.Elements;
using System.Collections.Generic;


namespace Skrptr.Input
{
    /// <summary>
    /// Class that syncronizes input between multiple Input types.
    /// </summary>
    public static class SkrptrMain 
    {
        /// <summary>
        /// Currently used input type.
        /// </summary>
        public static SkrptrInputType inputType = SkrptrInputType.None;

        /// <summary>
        /// References to currently / last interracted with elements.
        /// </summary>
        public static SkrptrElement selectedElem, hoveredElem, lastHoveredElem, lastSelectedElem;

        /// <summary>
        /// Reference for back-functionality. 
        /// </summary>
        public static List<SkrptrRegisterLastPanel> lastRegisteredPanels;
    }
}