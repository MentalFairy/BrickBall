using UnityEngine;
using DG.Tweening;

namespace Skrptr.Components
{
    /// <summary>
    /// Base Class for any animation. Difference between an animation and an Action is the lack of looping possibility and Ease of DoTeen
    /// </summary>
    public abstract class SkrptrAnimDoTween : SkrptrAnim
    {
        /// <summary>
        /// Contains an ease for DoTween.
        /// </summary>
        [SerializeField]
        protected Ease ease = Ease.OutQuad;        
    }
}
