using DG.Tweening;
using Skrptr;
using Skrptr.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Skrptr.Components.Audio
{
    /// <summary>
    /// Play Sound animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimAudioSetVolume : SkrptrAnim
    {
        public List<AnimDataFloatDurationDelay> animsDataAudio;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if (Time.realtimeSinceStartup > 3)
            {
                for (int i = 0; i < animsDataAudio.Count; i++)
                {
                    if ((animsDataAudio[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                    {
                        SetVolume(i);
                    }
                }
            }
        }

        protected override void ExecuteSingle(int index)
        {
            SetVolume(index);
        }
        private void SetVolume(int index)
        {
            animsDataAudio[index].IsValid(this);

            if (animsDataAudio[index].target.GetComponent<AudioSource>() != null)
            {
                AudioSource audioSource = animsDataAudio[index].target.GetComponent<AudioSource>();
                //Shoot me. Or the guy who reads this.
                DOTween.To(() => audioSource.volume, x => audioSource.volume = x, animsDataAudio[index].normalizedValue, animsDataAudio[index].duration).SetDelay(animsDataAudio[index].delay);
            }
        }
        protected override void InitLoopingAnims()
        {
            if (animsDataAudio == null)
                return;

            LoopIndexDurations = new Dictionary<int, float>();
            for (int i = 0; i < animsDataAudio.Count; i++)
            {
                if ((animsDataAudio[i].skrptrEvent & SkrptrEvent.Loop) == SkrptrEvent.Loop)
                {
                    LoopIndexDurations.Add(i, animsDataAudio[i].delay + animsDataAudio[i].duration);
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }
    }
}
