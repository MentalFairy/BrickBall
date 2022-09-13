using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr.Components.ImageAndText
{
    /// <summary>
    /// Slide animation outside of screen. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimSprite : SkrptrAnim
    {
        public List<AnimDataSprites> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    ExecuteSingle(i);
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            StartCoroutine(PlaySpriteAnim(index));
        }
        private IEnumerator PlaySpriteAnim(int index)
        {
            if (animsData[index].sprites != null)
            {
                animsData[index].IsValid(this);

                float elapsedTime = 0;
                float tick = animsData[index].duration / (animsData[index].sprites.Count);
                yield return new WaitForSeconds(animsData[index].delay);
                if (animsData[index].target.GetComponent<Image>() != null)
                {
                    Image targetImage = animsData[index].target.GetComponent<Image>();
                    int i = 0;
                    while (i < animsData[index].sprites.Count)
                    {
                        targetImage.sprite = animsData[index].sprites[i];
                        elapsedTime += Time.deltaTime;
                        if (elapsedTime > tick * i)
                            i++;
                        yield return null;
                    }
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
                }
            }
        }
    }
}