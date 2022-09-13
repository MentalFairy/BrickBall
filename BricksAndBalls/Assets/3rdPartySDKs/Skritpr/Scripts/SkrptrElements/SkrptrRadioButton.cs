using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr.Elements
{
    /// <summary>
    /// Skrptr Element with radiobutton behavior. It checks for other radio buttons under this parents transform and unchecks if any are found.
    /// </summary>
    public class SkrptrRadioButton : SkrptrCheckbox
    {
        /// <summary>
        /// Overriden behavior for OnClick - It also unchecks all radiobuttons in same hierarchy.
        /// </summary>
        public override void Click()
        {
            ExecuteActions(SkrptrEvent.Click);
            if(!isChecked)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    if(transform.parent.GetChild(i).GetComponent<SkrptrRadioButton>() != null)
                    {
                        SkrptrRadioButton auxRadioButton = transform.parent.GetChild(i).GetComponent<SkrptrRadioButton>();
                        if (auxRadioButton != this)
                        {
                            auxRadioButton.Uncheck();
                        }
                    }
                }
                Check();
            }
        }
    }
}
