using Skrptr.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr.Components.Triggers
{
    /// <summary>
    /// An Action that allows chaining events. On any event of a skrptrElement another one can be chained using this. Can be really useful.
    /// </summary>
    public class SkrptrTrigger : SkrptrAction
    {
        /// <summary>
        /// Contains all trigger events and their respective data.
        /// </summary>
        public List<TriggerTargets> TriggerTargets;

        public override void Execute(SkrptrEvent currentEvent)
        {
            try
            {
                Trigger(currentEvent);
            }
            catch(Exception ex)
            {
                Debug.LogError($"Trigger encountered error on gameobject {gameObject.name}! Check it out asap! \n Error Message : {ex.Message} - \n Trace: {ex.StackTrace}");
            }
        }

        private void Trigger(SkrptrEvent currentEvent)
        {
            for (int i = 0; i < TriggerTargets.Count; i++)
            {
                if ((TriggerTargets[i].onTriggerEvent & currentEvent) == currentEvent)
                {
                    foreach (SkrptrEvent item in Enum.GetValues(typeof(SkrptrEvent)))
                    {
                        if ((TriggerTargets[i].triggerEvent & item) == item && item != SkrptrEvent.None)
                        {
                            if (TriggerTargets[i].targetGO.activeInHierarchy && TriggerTargets[i].targetGO.GetComponent<SkrptrElement>() != null)
                            {
                                StartCoroutine(TriggerEventWithDelay(TriggerTargets[i].targetGO.GetComponent<SkrptrElement>(), item, TriggerTargets[i].delay));
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

