using Skrptr.Elements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Skrptr.Input
{
    /// <summary>
    /// Class that acts as an event system for Mouse Input (if available)
    /// </summary>
    public class SkrptrMouseInput : StandaloneInputModule
    {
        public float longPressDelay = 0.5f;
        //Long press available also for mouse for easier simulation in inspector.
        public void LongPress()
        {
            SkrptrMain.hoveredElem?.LongPress();
        }
        private void Update()
        {          
            //Set current interaction to Mouse if detected
            if(UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1) && SkrptrMain.inputType != SkrptrInputType.Mouse)
            {
                SkrptrMain.inputType = SkrptrInputType.Mouse;
            }

            if (SkrptrMain.inputType == SkrptrInputType.Mouse)
            {
                //Fetch pointer data
                PointerEventData pointerData = GetLastPointerEventData(-1);
                if (pointerData == null)
                    return;

                if (pointerData.pointerCurrentRaycast.isValid)
                {
                    if (pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>() != null)
                    {
                        //Hover enter / exit
                        if (SkrptrMain.hoveredElem != null)
                        {
                            if (pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>() != SkrptrMain.hoveredElem)
                            {
                                SkrptrMain.hoveredElem.HoverExit();
                                SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                                SkrptrMain.hoveredElem = null;
                                CancelInvoke(nameof(LongPress));
                            }
                        }
                        if (SkrptrMain.hoveredElem == null)
                        {
                            SkrptrMain.hoveredElem = pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>();
                            SkrptrMain.hoveredElem.HoverEnter();                            
                        }

                        //Click Down
                        if (UnityEngine.Input.GetMouseButtonDown(0))
                        {
                            pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>().Select();
                            Invoke(nameof(LongPress), longPressDelay);
                        }
                        // Click Held down
                        if (UnityEngine.Input.GetMouseButton(0))
                        {
                            if (SkrptrMain.selectedElem != null)
                            {
                                if (SkrptrMain.hoveredElem != SkrptrMain.selectedElem)
                                {
                                    SkrptrMain.selectedElem.Deselect();
                                    SkrptrMain.selectedElem = null;
                                    CancelInvoke(nameof(LongPress));
                                }
                            }
                        }
                        // Click Release
                        if (UnityEngine.Input.GetMouseButtonUp(0))
                        {
                            if (SkrptrMain.selectedElem != null)
                            {
                                SkrptrMain.selectedElem.Click();
                                SkrptrMain.selectedElem = null;
                                CancelInvoke(nameof(LongPress));
                            }
                        }
                    }
                    // Hit no SkrptrElem ==> Deselect
                    else
                    {
                        if (SkrptrMain.hoveredElem != null)
                        {
                            SkrptrMain.hoveredElem.HoverExit();
                            SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                            SkrptrMain.hoveredElem = null;
                            CancelInvoke(nameof(LongPress));
                        }
                        if (SkrptrMain.selectedElem != null)
                        {
                            SkrptrMain.selectedElem.Deselect();
                            SkrptrMain.selectedElem = null;
                            CancelInvoke(nameof(LongPress));
                        }
                    }
                }
                //Invalid Raycast == > Deselect
                else 
                {
                    if (SkrptrMain.hoveredElem != null)
                    {
                        SkrptrMain.hoveredElem.HoverExit();
                        SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                        SkrptrMain.hoveredElem = null;
                        CancelInvoke(nameof(LongPress));
                    }
                    if (SkrptrMain.selectedElem != null)
                    {
                        SkrptrMain.selectedElem.Deselect();                        
                        SkrptrMain.selectedElem = null;
                        CancelInvoke(nameof(LongPress));
                    }
                }
            }
        }

    }
}

