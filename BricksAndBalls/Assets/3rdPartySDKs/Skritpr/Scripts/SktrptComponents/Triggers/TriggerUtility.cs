using Skrptr.Elements;
using System.Threading.Tasks;

namespace Skrptr.Components.Triggers
{
    public static class TriggerUtility
    {
        /// <summary>
        /// Allows to fire an event on an element.
        /// Wins 1 frame over the Invoke Solution.
        /// </summary>
        /// <param name="targetElement">Target on which the event will fire.</param>
        /// <param name="eventToFire">Event to be fired.</param>
        /// <param name="delay">Delay after which the event will fire.</param>
        internal async static void TriggerEvent(this SkrptrElement targetElement, SkrptrEvent eventToFire, float delay = 0)
        {
            await Task.Delay((int)(delay * 1000));
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