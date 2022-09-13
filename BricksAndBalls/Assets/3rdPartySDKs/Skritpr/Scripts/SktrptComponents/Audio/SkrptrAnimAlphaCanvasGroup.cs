using DG.Tweening;
using Skrptr.Components;
using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr.Components.Audio
{
    public class SkrptrAnimAlphaCanvasGroup : SkrptrAnim
    {
        public List<AnimDataFloatDurationDelay> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if (Time.realtimeSinceStartup > 3)
            {
                for (int i = 0; i < animsData.Count; i++)
                {
                    if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                    {
                        SetAlpha(i);
                    }
                }
            }
        }

        protected override void ExecuteSingle(int index)
        {
            SetAlpha(index);
        }
        private void SetAlpha(int index)
        {
            animsData[index].IsValid(this);

            if (animsData[index].target.GetComponent<CanvasGroup>() != null)
            {
                CanvasGroup canvasGroup = animsData[index].target.GetComponent<CanvasGroup>();
                //Shoot me. Or the guy who reads this.
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, animsData[index].normalizedValue, animsData[index].duration).SetDelay(animsData[index].delay);
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
                    LoopIndexDurations.Add(i, animsData[i].delay + animsData[i].duration);
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }
    }
}
