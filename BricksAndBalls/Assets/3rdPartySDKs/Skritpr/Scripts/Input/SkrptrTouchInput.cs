using Skrptr.Elements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Skrptr.Input
{
    /// <summary>
    /// Class that acts as an event system for the touch interaction (if supported by device)
    /// </summary>
    public class SkrptrTouchInput : MonoBehaviour
    {
        /// <summary>
        /// Contains all graphical raycasters in the scene. Popualated on Start.
        /// </summary>
        [SerializeField]
        private GraphicRaycaster[] graphicRaycasters;

        /// <summary>
        /// Contains the results of the graphic raycasts shot.
        /// </summary>
        [SerializeField]
        private List<RaycastResult> raycastResults;

        /// <summary>
        /// Contains all skrptr elements hit by the current raycast.
        /// </summary>
        [SerializeField]
        private List<GameObject> hitSkrptrElements;

        /// <summary>
        /// Current touch phase.
        /// </summary>
        [SerializeField]
        private TouchPhase phase;

        /// <summary>
        /// Auxiliary reference to the last touch / current one.
        /// </summary>
        private Touch touch;

        /// <summary>
        /// Delta time between mouse click down and longpress event (if mouse is held down)
        /// </summary>
        /// 
        public float LongPressDelay = .5f;

        /// <summary>
        /// Event Called wwhen an element is longpressed.
        /// Used with Monobehavior.Invoke(nameOf(LongPress))
        /// ASync solution / Task with cancellation would be over-engineering it.
        /// </summary>
        public void LongPress()
        {
            SkrptrMain.selectedElem?.LongPress();
            SkrptrMain.selectedElem = null;
        }

        protected void Start()
        {
            touch = new Touch();
            hitSkrptrElements = new List<GameObject>();
            raycastResults = new List<RaycastResult>();
            graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            StartCoroutine(FetchRaycasts());
        }
        private IEnumerator FetchRaycasts()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return null;
            }
            graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
        }


        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
            StartCoroutine(FetchRaycasts());
        }

        public void Update()
        {
            try
            {
                //Assign current input method to touch if touch is detected
                if (UnityEngine.Input.touchCount > 0)
                {
                    SkrptrMain.inputType = SkrptrInputType.Touch;
                }

                if (SkrptrMain.inputType == SkrptrInputType.Touch)
                {
                    hitSkrptrElements.Clear();
                    raycastResults.Clear();
                    if (UnityEngine.Input.touchCount > 0)
                    {
                        touch = UnityEngine.Input.GetTouch(0);
                        PointerEventData ped;
                        //Get touch and all items hit by raycasts from all canvases.                

                        ped = new PointerEventData(null);
                        ped.position = touch.position;

                        foreach (GraphicRaycaster raycaster in graphicRaycasters)
                        {
                            raycaster?.Raycast(ped, raycastResults);
                        }

                        //Check the hit elements and add them
                        if (raycastResults.Count > 0)
                        {
                            for (int i = 0; i < raycastResults.Count; i++)
                            {
                                if (raycastResults[i].gameObject.GetComponent<SkrptrElement>() != null && !hitSkrptrElements.Contains(raycastResults[i].gameObject))
                                {
                                    hitSkrptrElements.Add(raycastResults[i].gameObject);
                                }
                            }
                        }

                        //TOUCH input management
                        //Fetch first touch and hit element
                        if (touch.phase == TouchPhase.Began)
                        {
                            phase = TouchPhase.Began;
                            if (hitSkrptrElements.Count > 0)
                            {
                                if (hitSkrptrElements[0].GetComponent<SkrptrElement>() != null)
                                {
                                    //Deselect current element
                                    if (SkrptrMain.selectedElem != null)
                                        SkrptrMain.selectedElem.Deselect();

                                    //Select new element
                                    if (SkrptrMain.selectedElem != hitSkrptrElements[0].GetComponent<SkrptrElement>())
                                    {
                                        hitSkrptrElements[0].GetComponent<SkrptrElement>().Select();
                                        Invoke(nameof(LongPress), LongPressDelay);
                                    }
                                }
                            }
                        }
                        //Reset if finger moved outside of original element
                        if (touch.phase == TouchPhase.Moved || (touch.phase == TouchPhase.Stationary))
                        {
                            if (touch.phase == TouchPhase.Moved)
                                phase = TouchPhase.Moved;
                            else if (touch.phase == TouchPhase.Stationary)
                                phase = TouchPhase.Stationary;
                            if (SkrptrMain.selectedElem != null)
                            {
                                if (hitSkrptrElements.Count > 0)
                                {
                                    if (hitSkrptrElements[0].GetComponent<SkrptrElement>() == null ||
                                        hitSkrptrElements[0].GetComponent<SkrptrElement>() != SkrptrMain.selectedElem)
                                    {
                                        if (SkrptrMain.lastSelectedElem != null)
                                            SkrptrMain.lastSelectedElem.Deselect();
                                        SkrptrMain.selectedElem.Deselect();
                                        SkrptrMain.selectedElem = null;
                                        CancelInvoke(nameof(LongPress));                                        
                                    }
                                }
                                //Deselect if no element is hit anymore
                                else
                                {
                                    if (SkrptrMain.lastSelectedElem != null)
                                        SkrptrMain.lastSelectedElem.Deselect();
                                    SkrptrMain.selectedElem.Deselect();
                                    SkrptrMain.selectedElem = null;
                                    CancelInvoke(nameof(LongPress));
                                }

                            }

                            if (SkrptrMain.hoveredElem == null)
                            {
                                if (hitSkrptrElements.Count > 0)
                                {
                                    if (hitSkrptrElements[0].GetComponent<SkrptrElement>() != null)
                                    {
                                        SkrptrMain.hoveredElem = hitSkrptrElements[0].GetComponent<SkrptrElement>();
                                        SkrptrMain.hoveredElem.HoverEnter();
                                    }
                                }
                            }
                            else
                            {
                                if (hitSkrptrElements.Count == 0)
                                {
                                    SkrptrMain.hoveredElem.HoverExit();
                                    SkrptrMain.hoveredElem = null;
                                }
                                //Something hit, check if hit the same thing
                                else
                                {
                                    if (hitSkrptrElements[0].GetComponent<SkrptrElement>() == null)
                                    {
                                        SkrptrMain.hoveredElem.HoverExit();
                                        SkrptrMain.hoveredElem = null;
                                    }
                                    else if(hitSkrptrElements[0].GetComponent<SkrptrElement>() != SkrptrMain.hoveredElem)
                                    {
                                        SkrptrMain.hoveredElem.HoverExit();
                                        SkrptrMain.hoveredElem = hitSkrptrElements[0].GetComponent<SkrptrElement>();
                                        SkrptrMain.hoveredElem.HoverEnter();
                                    }
                                }
                            }
                        }
                        // End the touch phase, click it.
                        if (touch.phase == TouchPhase.Ended)
                        {
                            if (SkrptrMain.selectedElem != null)
                            {
                                // Debug.Log("Touch Ended");
                                if (SkrptrMain.lastSelectedElem != null)
                                {
                                    SkrptrMain.lastSelectedElem.Deselect();
                                }
                                SkrptrMain.selectedElem.Deselect();
                                SkrptrMain.selectedElem.Click();
                                SkrptrMain.lastSelectedElem = SkrptrMain.selectedElem;
                                SkrptrMain.selectedElem = null;
                                CancelInvoke(nameof(LongPress));
                            }
                            if (SkrptrMain.hoveredElem != null)
                            {

                                SkrptrMain.hoveredElem.HoverExit();
                                SkrptrMain.hoveredElem = null;
                            }
                        }
                       

                    }
                    //Reset touch phase.
                    else
                    {
                        phase = touch.phase;
                    }
                }
            }
            catch
            {
                graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
            }
        }
    }
}