using Skrptr;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Skrptr.Components.Audio
{
    /// <summary>
    /// Play Sound animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimPauseAudioSource : SkrptrAnimPlayAudioSource
    {
        protected override void ExecuteSingle(int index)
        {
            StartCoroutine(PauseAudioSource(index));
        }
        private IEnumerator PauseAudioSource(int index)
        {
            if (animsDataAudio[index].target.GetComponent<AudioSource>() != null)
            {
                yield return new WaitForSeconds(animsDataAudio[index].delay);
                animsDataAudio[index].target.GetComponent<AudioSource>().Pause();
            }
        }        
    }
}
