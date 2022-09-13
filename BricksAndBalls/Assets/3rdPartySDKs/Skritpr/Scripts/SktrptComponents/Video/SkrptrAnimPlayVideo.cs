using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Skrptr.Components.Video
{
    /// <summary>
    /// Pauses the playing on the targeted gameobject video player.
    /// </summary>
    public class SkrptrAnimPlayVideo : SkrptrAnim
    {
        public List<AnimDataDelayed> animsData;
        /// <summary>
        /// Pauses the playing on the targeted video player at the specific event.
        /// </summary>
        /// <param name="currentSkrptrEvent">Current event that just occured in the event system.</param>
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if ((animsData[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if (animsData[i].target.GetComponent<VideoPlayer>() != null)
                    {
                        VideoPlayer vp = animsData[i].target.GetComponent<VideoPlayer>();
                        vp.Play();
                    }
                    else
                    {
                        Debug.LogWarning("Couldn't find VideoPlayer component on: " + animsData[i].target + " | PlayVideo called from : " + gameObject.name);
                    }
                }
            }
        }

        protected override void ExecuteSingle(int index)
        {
            PlayVideo(index);
        }
        private IEnumerator PlayVideo(int index)
        {
            animsData[index].IsValid(this);

            yield return new WaitForSeconds(animsData[index].delay);
            if (animsData[index].target.GetComponent<VideoPlayer>() != null)
            {
                VideoPlayer vp = animsData[index].target.GetComponent<VideoPlayer>();
                vp.Pause();
            }
            else
            {
                Debug.LogWarning("Couldn't find VideoPlayer component on: " + animsData[index].target + " | PauseVideo called from : " + gameObject.name);
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
                    LoopIndexDurations.Add(i, animsData[i].delay);
                }
            }
        }
    }
}
