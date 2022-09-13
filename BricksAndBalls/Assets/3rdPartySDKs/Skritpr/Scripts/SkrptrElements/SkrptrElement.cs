using Skrptr.Components;
using Skrptr.Components.Triggers;
using Skrptr.Events;
using Skrptr.Input;
using Skrptr.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr.Elements
{
    /// <summary>
    /// Base behavior for any SkrptrElement / Radio Button / Checkbox.
    /// </summary>
    public class SkrptrElement : MonoBehaviour
    {

        #region Private Fields / Properties

        /// <summary>
        /// Contains all derived components of SkrptrActions (Those are not loopable / do not contain animData)
        /// </summary>
        private List<SkrptrAction> skrptrActions;
        private SkrptrKeyboardMapper keyboardMapper;

        #endregion

        #region Public / Serialized properties

        /// <summary>
        /// Flags which indicate the events that will be triggered on Start().
        /// </summary>
        [EnumFlags]
        public SkrptrEvent EventsToCallOnStart;

        #endregion

        #region Unity Monobehavior Functions

        /// <summary>
        /// Used to initialize fields
        /// </summary>
        private void Awake()
        {
            skrptrActions = GetComponents<SkrptrAction>().ToList<SkrptrAction>();
            if(GetComponent<SkrptrKeyboardMapper>() !=null)
            {
                keyboardMapper = GetComponent<SkrptrKeyboardMapper>();
            }
        }

        /// <summary>
        /// Call this if you added more actions dynamically.
        /// </summary>
        public void FetchSkrptrActions()
        {
            skrptrActions = GetComponents<SkrptrAction>().ToList<SkrptrAction>();
        }
        /// <summary>
        /// Call this if you wish to add naother skrptr action to an already existing element.
        /// </summary>
        /// <param name="skrptrAction">Action to be added.</param>
        public void AddSkrptrAction(SkrptrAction skrptrAction)
        {
            skrptrActions.Add(skrptrAction);
        }

        public virtual void Start()
        {
            ExecuteEvents(EventsToCallOnStart);
        }

        #endregion

        /// <summary>
        /// Following functions are the possible events triggered by the input event systems. They all call ExecuteAnims and Actions to run with the specific events on this SkrptrElement.
        /// </summary>
        public virtual void Select()
        {
            SkrptrMain.selectedElem = this;
            ExecuteActions(SkrptrEvent.Select);
        }
        public virtual void Deselect()
        {
            SkrptrMain.lastSelectedElem = SkrptrMain.selectedElem;
            ExecuteActions(SkrptrEvent.Deselect);          
        }
        public virtual void Enable()
        {
            gameObject.SetActive(true);
            ExecuteActions(SkrptrEvent.Enable);
        }
        public virtual void Disable()
        {
            ExecuteActions(SkrptrEvent.Disable);
            gameObject.SetActive(false);
        }
        public virtual void Hide()
        {
            ExecuteActions(SkrptrEvent.Hide);
        }
        public virtual void Show()
        {
            ExecuteActions(SkrptrEvent.Show);
        }
        public virtual void Lock()
        {
            DisableRaycastTarget();
            ExecuteActions(SkrptrEvent.Lock);            
        }
        public virtual void Unlock()
        {
            ExecuteActions(SkrptrEvent.Unlock);
            EnableRaycastTarget();
        }
        public virtual void Click()
        {
            ExecuteActions(SkrptrEvent.Click);
        }
        public virtual void HoverEnter()
        {
            ExecuteActions(SkrptrEvent.HoverEnter);
        }
        public virtual void HoverExit()
        {
            ExecuteActions(SkrptrEvent.HoverExit);
        }
        public virtual void Check()
        {
            ExecuteActions(SkrptrEvent.Check);
        }
        public virtual void Uncheck()
        {
            ExecuteActions(SkrptrEvent.Uncheck);
        }
        public virtual void LongPress()
        {
            ExecuteActions(SkrptrEvent.LongPress);
        }
        public virtual void Loop()
        {
            ExecuteActions(SkrptrEvent.Loop);
        }
        /// <summary>
        /// Runs through all Actions and sends them the current event. These will react based on implementation and animData provided.
        /// </summary>
        /// <param name="currentSkrptrEvent"> Current Event which occured on this Skrptr Element</param>
        protected void ExecuteActions(SkrptrEvent currentSkrptrEvent)
        {
            if(skrptrActions == null)
            {
                Debug.LogWarning($"Skrptr Element {gameObject} has no actions! Probably you attempted to call something before it was ready.");
                return;
            }

            foreach (var action in skrptrActions)
            {
                try
                {
                    action.Execute(currentSkrptrEvent);
                }
                catch
                {
                    Debug.LogWarning($"[Skrptr] Execute of action failed to complete - check gameobject and ensure all references are correctly setup. ");
                }
            }
            //SkrptrEventBroadcaster.Instance?.OnElementStateChanged(new ElementStateChangedEventArgs(this, currentSkrptrEvent, SkrptrMain.inputType));
        }

        /// <summary>
        /// Executes all events based on the flags parameter.
        /// </summary>
        /// <param name="eventsFlag">Flag that represents all events that should run.</param>
        protected void ExecuteEvents(SkrptrEvent eventsFlag)
        {
            // Events to call on start:
            foreach (SkrptrEvent skrptrEvent in Enum.GetValues(typeof(SkrptrEvent)))
            {
                if (skrptrEvent != SkrptrEvent.None)
                {
                    if ((eventsFlag & skrptrEvent) == skrptrEvent)
                    {
                        TriggerUtility.TriggerEvent(this, skrptrEvent, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Disables raycast target on this gameobjects visual component.
        /// </summary>
        private void DisableRaycastTarget()
        {
            try
            {

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().raycastTarget = false;
                }
                if (GetComponent<Text>() != null)
                {
                    GetComponent<Text>().raycastTarget = false;
                }
                if (GetComponent<RawImage>() != null)
                {
                    GetComponent<RawImage>().raycastTarget = false;
                }
            }
            catch { }
        }
        /// <summary>
        /// Enables raycast target on this gameobjects visual component.
        /// </summary>
        private void EnableRaycastTarget()
        {
            try
            {
                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().raycastTarget = true;
                }
                if (GetComponent<Text>() != null)
                {
                    GetComponent<Text>().raycastTarget = true;
                }
                if (GetComponent<RawImage>() != null)
                {
                    GetComponent<RawImage>().raycastTarget = true;
                }
            }
            catch { }
        }

        #region Operator overloads

        /// <summary>
        /// Operator overload for '+'. It is commutative!
        /// Anims and actions of skrptr element A will be Unioned with B.
        /// </summary>
        /// <param name="a">Element A </param>
        /// <param name="b">Element B </param>
        /// <returns> New SkrptrElement with unioned anims and actions.</returns>
        public static SkrptrElement operator +(SkrptrElement a, SkrptrElement b)
        {
            SkrptrElement returnElem = new SkrptrElement();

            returnElem.skrptrActions = a.skrptrActions.Union(b.skrptrActions).ToList();

            return returnElem;
        }

        /// <summary>
        /// Operator overload for '-'. It is not commutative!
        /// Anims and actions of skrptr element B will be substracted from A.
        /// </summary>
        /// <param name="a">Element A </param>
        /// <param name="b">Element B </param>
        /// <returns> New SkrptrElement with intersected anims and actions.</returns>
        public static SkrptrElement operator -(SkrptrElement a, SkrptrElement b)
        {
            SkrptrElement returnElem = new SkrptrElement();

            returnElem.skrptrActions = a.skrptrActions.Intersect(b.skrptrActions).ToList();

            return returnElem;
        }

        #endregion
    }
}