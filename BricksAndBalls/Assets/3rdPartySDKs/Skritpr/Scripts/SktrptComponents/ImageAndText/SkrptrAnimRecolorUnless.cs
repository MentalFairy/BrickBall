﻿using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using Skrptr.Elements;

namespace Skrptr.Components.ImageAndText
{
    /// <summary>
    /// Recolor animation for Images / Text / Raw Image. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimRecolorUnless : SkrptrAnim
    {
        public bool UnlessIsChecked;
        public bool UnlessIsUnchecked;
        public List<AnimDataColor> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    Recolor(i);
                }
            }
        }

        protected override void ExecuteSingle(int index)
        {
            Recolor(index);
        }
        public void Recolor(int index)
        {
            animsData[index].IsValid(this);

            if (ValidateChecks())
            {
                if (animsData[index].target.GetComponent<Image>() != null)
                {
                    animsData[index].target.GetComponent<Image>().DOColor(animsData[index].targetColor, animsData[index].duration).SetDelay(animsData[index].delay);
                }
                else if (animsData[index].target.GetComponent<Text>() != null)
                {
                    animsData[index].target.GetComponent<Text>().DOColor(animsData[index].targetColor, animsData[index].duration).SetDelay(animsData[index].delay);
                }
                else if (animsData[index].target.GetComponent<RawImage>() != null)
                {
                    animsData[index].target.GetComponent<RawImage>().DOColor(animsData[index].targetColor, animsData[index].duration).SetDelay(animsData[index].delay);
                }
            }
        }     

        protected override void InitLoopingAnims()
        {
            if (animsData == null)
                return;

            LoopIndexDurations = new Dictionary<int, float>();
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & SkrptrEvent.Loop) == SkrptrEvent.Loop)
                {
                    LoopIndexDurations.Add(i, animsData[i].duration);
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }

        private bool ValidateChecks()
        {
            if (UnlessIsChecked || UnlessIsUnchecked)
            {
                if (GetComponent<SkrptrCheckbox>() != null)
                {
                    var checkBox = GetComponent<SkrptrCheckbox>();
                    if (UnlessIsChecked && checkBox.isChecked)
                        return true;
                    if(UnlessIsUnchecked && !checkBox.isChecked)
                        return true;
                }
            }

            return false;
        }
    }
    
}