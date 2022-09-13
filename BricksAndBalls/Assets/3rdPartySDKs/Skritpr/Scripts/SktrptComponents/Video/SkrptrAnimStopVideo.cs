using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Skrptr.Components.Video
{
    /// <summary>
    /// Stops the videoplayer on the target gameobject.
    /// </summary>
    public class SkrptrAnimStopVideo : SkrptrAnim
    {
        public List<AnimDataDelayed> animsData;
        /// <summary>
        /// Stop the current video player from playing.
        /// </summary>
        /// <param name="currentSkrptrEvent">Current event that just occured in the event system.</param>
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
            StopVideo(index);
        }
        private IEnumerator StopVideo(int index)
        {
            animsData[index].IsValid(this);

            yield return new WaitForSeconds(animsData[index].delay);
            if (animsData[index].target.GetComponent<VideoPlayer>() != null)
            {
                VideoPlayer vp = animsData[index].target.GetComponent<VideoPlayer>();
                vp.Stop();
            }
            else
            {
                Debug.LogWarning("Couldn't find VideoPlayer component on: " + animsData[index].target + " | StopVideo called from : " + gameObject.name);
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