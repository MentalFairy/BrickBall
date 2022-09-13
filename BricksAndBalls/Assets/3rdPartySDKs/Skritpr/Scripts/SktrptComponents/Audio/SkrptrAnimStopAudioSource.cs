using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Skrptr.Components.Audio
{
    /// <summary>
    /// Play Sound animations. Refer to SkrptrAnim.cs for comments / explanations.
    /// </summary>
    public class SkrptrAnimStopAudioSource : SkrptrAnimPlayAudioSource
    {
        protected override void ExecuteSingle(int index)
        {
            StartCoroutine(StopAudioSource(index));
        }
        private IEnumerator StopAudioSource(int index)
        {
            animsDataAudio[index].IsValid(this);
            yield return new WaitForSeconds(animsDataAudio[index].delay);
            animsDataAudio[index].target.GetComponent<AudioSource>().Stop();
        }      
    }
}
