using System.Collections.Generic;
using UnityEngine;

namespace Skrptr.Components.StartStopLoop
{
    /// <summary>
    /// Starts the Looping process on a skrptrAnim. Please refer to SkrptrAnim.cs and SkrptrAction.cs for more comments / explanations.
    /// </summary>
    public class SkrptrActionStartLoopAnim : SkrptrAction
    {
        public List<AnimDataDelayed> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            foreach (var animData in animsData)
            {
                if ((animData.skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if (animData.target.GetComponent<SkrptrAnim>() != null)
                    {
                        SkrptrAnim[] anims = animData.target.GetComponents<SkrptrAnim>();
                        foreach (var anim in anims)
                        {
                            anim.StartLoopingAnims(animData.delay);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Couldn't find a SkrptrAnimation on " + animData.target);
                    }
                }
            }
        }
    }
}
