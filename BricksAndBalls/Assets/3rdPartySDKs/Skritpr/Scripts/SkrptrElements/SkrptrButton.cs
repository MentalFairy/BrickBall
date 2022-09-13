using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr.Elements
{
    /// <summary>
    /// SkrptrElement with a few more properties to access in code easier for dynamic UIs.
    /// </summary>
    public class SkrptrButton : SkrptrElement
    {
        /// <summary>
        /// Unraycastable text label
        /// </summary>
        public Text label;

        /// <summary>
        /// Images for the background image, the fill, the hover effect and selection.
        /// </summary>
        public Image background, fill, hover,select;
    }
}