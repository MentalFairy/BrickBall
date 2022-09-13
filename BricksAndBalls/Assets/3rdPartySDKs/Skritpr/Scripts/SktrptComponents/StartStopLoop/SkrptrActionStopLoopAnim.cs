using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr.Components.StartStopLoop
{
    /// <summary>
    /// Stops the Looping process on a skrptrAnim. Please refer to SkrptrAnim.cs and SkrptrAction.cs for more comments / explanations.
    /// </summary>
    public class SkrptrActionStopLoopAnim : SkrptrAction
    {
        public List<AnimDataDelayed> animsData;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            foreach (var animData in animsData)
            {
                if((animData.skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    foreach (var anim in animData.target.GetComponents<SkrptrAnim>())
                    {
                        anim.StopLoopingAnims(animData.delay);
                    }   
                }   
            }
        }
    }
}
