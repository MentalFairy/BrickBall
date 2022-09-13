using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Skrptr.Components.Transform
{
    /// <summary>
    /// Slide animation to XY Point. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimMoveToXY : SkrptrAnim
    {
        public List<AnimDataVector3> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    Move(i);
                    
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
            if (rectTf != null)
                rectTf.DOAnchorPos(animsData[index].targetV3, animsData[index].duration).SetDelay(animsData[index].delay);
            else
                animsData[index].target.transform.DOMove(animsData[index].targetV3, animsData[index].duration).SetDelay(animsData[index].delay);
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