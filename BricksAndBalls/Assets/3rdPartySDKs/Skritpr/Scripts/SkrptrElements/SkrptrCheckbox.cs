using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr.Elements
{
    /// <summary>
    /// Refer to SkrptrElement for more comments / explanations.
    /// Similar SkrptrElement behavior with a twist: It can act as a checkbox by clicking on it.
    /// </summary>
    public class SkrptrCheckbox : SkrptrElement
    {
        /// <summary>
        /// States the status of the checkbox.
        /// </summary>
        public bool isChecked = false;

        /// <summary>
        /// Overrides Click functionality to give it two states.
        /// </summary>
        public override void Click()
        {
            if (isChecked)
                Uncheck();
            else
                Check();
            base.Click();           
        }

        /// <summary>
        /// Overrides to also set property.
        /// </summary>
        public override void Check()
        {
            isChecked = true;
            base.Check();
        }

        /// <summary>
        /// Overrides to also set property.
        /// </summary>
        public override void Uncheck()
        {
            isChecked = false;
            base.Uncheck();
        }
    }
}