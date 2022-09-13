using Skrptr.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr.Components.Triggers
{
    /// <summary>
    /// Class that works exactly like the Trigger, but it takes the children of the target instead.
    /// </summary>
    public class SkrptrTriggerAllElementsUnderTarget : SkrptrAction
    {
        public bool ReverseTriggerOrder = false;

        /// <summary>
        /// Contains all trigger events and their respective data.
        /// </summary>
        public List<TriggerTargetsDelayedBetween> triggerTargets;
        
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            if (!ReverseTriggerOrder)
                ExecuteTrigger(currentSkrptrEvent);
            else
                ExecuteTriggerReversed(currentSkrptrEvent);
        }

        private void ExecuteTrigger(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < triggerTargets.Count; i++)
            {
                if ((triggerTargets[i].onTriggerEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    foreach (SkrptrEvent item in Enum.GetValues(typeof(SkrptrEvent)))
                    {
                        if ((triggerTargets[i].triggerEvent & item) == item && item != SkrptrEvent.None)
                        {
                            for (int j = 0; j < triggerTargets[i].targetGO.transform.childCount; j++)
                            {
                                if (triggerTargets[i].targetGO.transform.GetChild(j).GetComponent<SkrptrElement>() != null)
                                {
                                    StartCoroutine(TriggerEventWithDelay(triggerTargets[i].targetGO.transform.GetChild(j).GetComponent<SkrptrElement>(), item, triggerTargets[i].delay + j * triggerTargets[i].delayBetween));
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ExecuteTriggerReversed(SkrptrEvent currentSkrptrEvent)
        {
            //Debug.Log("Executing reversed order");
            for (int i = 0; i < triggerTargets.Count; i++)
            {
                if ((triggerTargets[i].onTriggerEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    foreach (SkrptrEvent item in Enum.GetValues(typeof(SkrptrEvent)))
                    {
                        if ((triggerTargets[i].triggerEvent & item) == item && item != SkrptrEvent.None)
                        {
                            for (int j = triggerTargets[i].targetGO.transform.childCount-1; j >= 0; j--)
                            {
                                if (triggerTargets[i].targetGO.transform.GetChild(j).GetComponent<SkrptrElement>() != null)
                                {
                                    //Debug.Log($"Executing reversed order {j}: {triggerTargets[i].targetGO.transform.GetChild(j).gameObject.name} ");
                                    StartCoroutine(TriggerEventWithDelay(triggerTargets[i].targetGO.transform.GetChild(j).GetComponent<SkrptrElement>(), item, triggerTargets[i].delay + (triggerTargets[i].targetGO.transform.childCount - 1 -j) * triggerTargets[i].delayBetween));
                                }
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator TriggerEventWithDelay(SkrptrElement targetElement, SkrptrEvent eventToFire, float delay)
        {
            if (delay != 0)
                yield return new WaitForSeconds(delay);

            switch (eventToFire)
            {
                case SkrptrEvent.Click:
                    targetElement.Click();
                    break;
                case SkrptrEvent.Select:
                    targetElement.Select();
                    break;
                case SkrptrEvent.Deselect:
                    targetElement.Deselect();
                    break;
                case SkrptrEvent.Enable:
                    targetElement.Enable();
                    break;
                case SkrptrEvent.Disable:
                    targetElement.Disable();
                    break;
                case SkrptrEvent.Hide:
                    targetElement.Hide();
                    break;
                case SkrptrEvent.Show:
                    targetElement.Show();
                    break;
                case SkrptrEvent.Lock:
                    targetElement.Lock();
                    break;
                case SkrptrEvent.Unlock:
                    targetElement.Unlock();
                    break;
                case SkrptrEvent.HoverEnter:
                    targetElement.HoverEnter();
                    break;
                case SkrptrEvent.HoverExit:
                    targetElement.HoverExit();
                    break;
                case SkrptrEvent.Check:
                    targetElement.Check();
                    break;
                case SkrptrEvent.Uncheck:
                    targetElement.Uncheck();
                    break;
                case SkrptrEvent.LongPress:
                    targetElement.LongPress();
                    break;
                case SkrptrEvent.Loop:
                    targetElement.Loop();
                    break;
                default:
                    break;
                case SkrptrEvent.None:
                    break;
            }
        }
    }
}
