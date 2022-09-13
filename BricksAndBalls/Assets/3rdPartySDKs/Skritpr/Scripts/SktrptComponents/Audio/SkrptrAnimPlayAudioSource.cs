using Skrptr;
using Skrptr.Input;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Skrptr.Components.Audio
{
    /// <summary>
    /// Play Sound animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimPlayAudioSource : SkrptrAnim
    {
        public List<AnimDataDelayed> animsDataAudio;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsDataAudio.Count; i++)
            {
                if ((animsDataAudio[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    ExecuteSingle(i);
                }
            }
            
        }

        protected override void ExecuteSingle(int index)
        {
            PlayAudioSource(index);
        }
        private void PlayAudioSource(int index)
        {
            animsDataAudio[index].IsValid(this);
            

            if (animsDataAudio[index].target.GetComponent<AudioSource>() != null)
            {
                if (animsDataAudio[index].delay == 0)
                {
                    animsDataAudio[index].target.GetComponent<AudioSource>().Play();
                }
                else
                {
                    animsDataAudio[index].target.GetComponent<AudioSource>().PlayDelayed(animsDataAudio[index].delay);
                }
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
                    LoopIndexDurations.Add(i, animsDataAudio[i].delay);
                    //Debug.Log("Adding anim: " + i + " " + loopIndexDuration[i]);
                }
            }
        }
    }
}
