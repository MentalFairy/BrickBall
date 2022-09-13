using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Skrptr.Components.Transform
{
    /// <summary>
    /// Rotation animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimMoveOutsideOfScreen : SkrptrAnimDoTween
    {
        public List<AnimDataSlideOutside> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if (animsData[i].target.GetComponent<RectTransform>() != null)
                    {
                        MoveOutside(i);
                    }
                    else
                    {
                        Debug.LogWarning("Traget : " + animsData[i].target.name + " has no RectTransform attached. " +
                            "Animation from: " + gameObject.name + "will not run.");
                    }
                }
            }
        }

        protected override void ExecuteSingle(int index)
        {
            MoveOutside(index);
        }
        private void MoveOutside(int index)
        {
            animsData[index].IsValid(this);

            RectTransform rectTf = animsData[index].target.GetComponent<RectTransform>();
            Vector2 targetPosition;
            switch (animsData[index].slideDirection)
            {
                case SlideDirection.Left:
                    targetPosition = new Vector2(-rectTf.rect.size.x * 1.5f, rectTf.position.y);
                    break;
                case SlideDirection.Right:
                    targetPosition = new Vector2(Screen.width + rectTf.rect.size.x *1.5f , rectTf.position.y);
                    break;
                case SlideDirection.Up:
                    targetPosition = new Vector2(rectTf.position.x, Screen.height + rectTf.rect.size.y * 1.5f);
                    break;
                case SlideDirection.Down:
                    targetPosition = new Vector2(rectTf.position.x, -rectTf.rect.size.y * 1.5f);
                    break;
                default:
                    targetPosition = Vector2.zero;
                    break;
            }
            animsData[index].target.GetComponent<RectTransform>().DOMove(targetPosition, animsData[index].duration).SetEase(ease).SetDelay(animsData[index].delay);

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
