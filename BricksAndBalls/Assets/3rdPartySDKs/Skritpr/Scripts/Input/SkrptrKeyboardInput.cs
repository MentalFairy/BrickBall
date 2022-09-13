using Skrptr.Components.Triggers;
using Skrptr.Elements;
using Skrptr.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr.Input
{
    public class SkrptrKeyboardInput : MonoBehaviour
    {
        /// <summary>
        /// Keycombos to interract with keyboard.
        /// </summary>
        public KeyCombo upCombo = new KeyCombo (new List<KeyCode>() {  KeyCode.UpArrow}), 
                        downCombo = new KeyCombo(new List<KeyCode>() { KeyCode.DownArrow }),
                        leftCombo = new KeyCombo(new List<KeyCode>() { KeyCode.LeftArrow }),
                        rightcombo = new KeyCombo(new List<KeyCode>() { KeyCode.RightArrow }),
                        backCombo = new KeyCombo(new List<KeyCode>() { KeyCode.Escape }),
                        clickCombo = new KeyCombo(new List<KeyCode>() { KeyCode.Return });

        void Update()
        {
            //Manage Input
            if(upCombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Up);
            }
            else if (downCombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Down);
            }
            else if (leftCombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Left);
            }
            else if (rightcombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Right);
            }
            else if (backCombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Back);
            }
            else if (clickCombo.IsKeyComboDown())
            {
                InputTriggered(NeighbourDirection.Click);
            }
        }
        /// <summary>
        /// Manages input based on direction and updates SkrptrMain.cs 
        /// </summary>
        /// <param name="direction"></param>
        private void InputTriggered(NeighbourDirection direction)
        {
            //Definitely Input is triggered from keyboard ==> Update input method
            SkrptrMain.inputType = SkrptrInputType.Keyboard;

            //Check for any last selected element in case none, select the last one
            if (SkrptrMain.selectedElem == null)
            {
                if (SkrptrMain.lastSelectedElem != null)
                {
                    SkrptrMain.lastSelectedElem.Select();
                }
            }
            //Element already selected ==> Time to move.
            else
            {
                if (SkrptrMain.selectedElem.GetComponent<SkrptrKeyboardMapper>() != null)
                {
                    SkrptrKeyboardMapper mapper = SkrptrMain.selectedElem.GetComponent<SkrptrKeyboardMapper>();
                    if (direction != NeighbourDirection.Back && direction != NeighbourDirection.Click)
                    {
                        //Fetch correct direction and the respective game object                                      
                        GameObject targetNeighbour = null;
                        for (int i = 0; i < mapper.neighbours.Count; i++)
                        {
                            if ((mapper.neighbours[i].direction & direction) == direction)
                            {
                                targetNeighbour = mapper.neighbours[i].target.gameObject;
                                break;
                            }
                        }
                        if (targetNeighbour != null)
                        {
                            SkrptrMain.selectedElem.Deselect();
                            targetNeighbour.GetComponent<SkrptrElement>().Select();
                        }
                    }
                    //back
                    else if (direction == NeighbourDirection.Back)
                    {
                        //Deselect and select ne element.
                        foreach (SkrptrEvent item in Enum.GetValues(typeof(SkrptrEvent)))
                        {
                            if ((mapper.returnToLastPanelEventsCallback & item) == item && item != SkrptrEvent.None)
                            {
                                TriggerUtility.TriggerEvent(SkrptrMain.lastRegisteredPanels[SkrptrMain.lastRegisteredPanels.Count - 1].GetComponent<SkrptrElement>(), item);
                            }
                        }
                        SkrptrMain.selectedElem.Deselect();
                        SkrptrMain.lastRegisteredPanels[SkrptrMain.lastRegisteredPanels.Count - 1].lastSelectedElement.Select();
                        SkrptrMain.lastRegisteredPanels.RemoveAt(SkrptrMain.lastRegisteredPanels.Count - 1);
                    }
                    //click
                    else if(direction == NeighbourDirection.Click)
                    {
                        SkrptrMain.selectedElem.Click();
                    }
                }
            }
        }
    }
}