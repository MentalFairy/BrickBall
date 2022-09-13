using DG.Tweening;
using System.Collections.Generic;

namespace Skrptr.Components.Transform
{
    /// <summary>
    /// Resize target to Vector 3 XYZ. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimResizeToXYZ : SkrptrAnimDoTween
    {
        public List<AnimDataVector3> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    Resize(i);                    
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            Resize(index);
        }
        private void Resize(int index)
        {
            animsData[index].IsValid(this);

            animsData[index].target.transform.DOScale(animsData[index].targetV3, animsData[index].duration)
                          .SetDelay(animsData[index].delay).SetEase(ease);
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
                    LoopIndexDurations.Add(i, animsData[i].duration );
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }
    }
    
}