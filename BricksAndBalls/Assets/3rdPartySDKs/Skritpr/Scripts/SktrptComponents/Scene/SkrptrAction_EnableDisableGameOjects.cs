using Skrptr;
using Skrptr.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrptrAction_EnableDisableGameOjects : SkrptrAction
{
    public SkrptrEvent EventToTriggerOn = SkrptrEvent.None;
    public List<GameObject> GameObjectsToEanble = new List<GameObject>();
    public bool Enable = true;
    public override void Execute(SkrptrEvent currentSkrptrEvent)
    {
        if((EventToTriggerOn & currentSkrptrEvent) == currentSkrptrEvent)
        {
            foreach (var go in GameObjectsToEanble)
            {
                go.SetActive(Enable);
            }
        }
    }
}
