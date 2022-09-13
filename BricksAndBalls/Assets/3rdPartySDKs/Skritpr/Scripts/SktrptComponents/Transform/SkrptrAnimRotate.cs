using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Skrptr.Components.Transform
{
    /// <summary>
    /// Rotation animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimRotate : SkrptrAnimDoTween
    {      
        public List<AnimDataRotate> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if (animsData[i].target.GetComponent<RectTransform>() != null)
                    {
                        Rotate(i);
                    }
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            Rotate(index);
        }
        private void Rotate(int index)
        {
            animsData[index].IsValid(this);

            if (animsData[index].rotateType == RotateType.Absolute)
            {
                animsData[index].target.transform.DORotate(animsData[index].targetV3, animsData[index].duration, RotateMode.FastBeyond360)
                                .SetDelay(animsData[index].delay).SetEase(ease);
            }
            else
            {
                Vector3 rotateTo = animsData[index].target.transform.eulerAngles + animsData[index].targetV3;
                animsData[index].target.transform.DORotate(rotateTo, animsData[index].duration, RotateMode.FastBeyond360)
                                .SetDelay(animsData[index].delay).SetEase(ease);
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
    }
}