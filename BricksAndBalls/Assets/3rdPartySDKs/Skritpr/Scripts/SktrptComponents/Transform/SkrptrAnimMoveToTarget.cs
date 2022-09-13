using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Skrptr.Components.Transform
{
    /// <summary>
    /// Slide animation to Target. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimMoveToTarget : SkrptrAnim
    {
        public List<AnimDataGO> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if (animsData[i].target.GetComponent<RectTransform>() != null)
                    {
                        Move(i);
                    }
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            Move(index);
        }
        private void Move(int index)
        {
            animsData[index].IsValid(this);

            RectTransform rectTf = animsData[index].target.GetComponent<RectTransform>();
            rectTf.DOMove(animsData[index].targetGameObject.transform.position, animsData[index].duration)
                         .SetDelay(animsData[index].delay);
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
                    LoopIndexDurations.Add(i, animsData[i].duration + animsData[i].delay);
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }
    }
}