using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Skrptr.Components.Video
{
    /// <summary>
    /// Sets and prepares the clip on the targeted gameobject video player.
    /// </summary>
    public class SkrptrAnimSetVideo : SkrptrAnim
    {
        public List<AnimDataVideoClip> animsData;
        /// <summary>
        /// Sets the clip and prepares it on the targeted video player at the specific event.
        /// </summary>
        /// <param name="currentSkrptrEvent">Current event that just occured in the event system.</param>
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animsData.Count; i++)
            {
                if((animsData[i].skrptrEvent &currentSkrptrEvent) == currentSkrptrEvent)
                {
                    SetVideo(i);
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            SetVideo(index);
        }
        private IEnumerator SetVideo(int index)
        {
            animsData[index].IsValid(this);


            if (animsData[index].target.GetComponent<VideoPlayer>() != null)
            {
                yield return new WaitForSeconds(animsData[index].delay);
                if (animsData[index].target.GetComponent<VideoPlayer>() != null)
                {
                    VideoPlayer vp = animsData[index].target.GetComponent<VideoPlayer>();
                    vp.Stop();
                    vp.clip = animsData[index].videoClip;
                    vp.prepareCompleted += Vp_prepareCompleted;
                    vp.Prepare();
                }
                else
                {
                    Debug.LogWarning("Couldn't find VideoPlayer component on: " + animsData[index].target + " | PauseVideo called from : " + gameObject.name);
                }
            }
        }

        private void Vp_prepareCompleted(VideoPlayer source)
        {
            source.Play();
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
