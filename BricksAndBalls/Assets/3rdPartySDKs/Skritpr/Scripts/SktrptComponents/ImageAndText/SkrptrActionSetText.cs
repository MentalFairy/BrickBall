using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr.Components.ImageAndText
{
    public class SkrptrActionSetText : SkrptrAnim
    {
        public List<AnimDataDelayedString> animDataDelayedString;
        public override void Execute(SkrptrEvent currentSkrptrEvent)
        {
            for (int i = 0; i < animDataDelayedString.Count; i++)
            {
                if((animDataDelayedString[i].skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    if(animDataDelayedString[i].target != null)
                    {
                        ExecuteSingle(i);
                    }
                    else
                    {
                        Debug.LogWarning("Found empty target on Set text of : " + gameObject.name +" during event: " + currentSkrptrEvent.ToString());
                    }
                }
            }
        }
        protected override void ExecuteSingle(int index)
        {
            StartCoroutine(SetText(index));
        }
        private IEnumerator SetText(int index)
        {
            yield return new WaitForSeconds(animDataDelayedString[index].delay);
            if(animDataDelayedString[index].target.GetComponent<Text>() !=null)
            {
                animDataDelayedString[index].target.GetComponent<Text>().text = animDataDelayedString[index].text;
            }
            if (animDataDelayedString[index].target.GetComponent<InputField>() != null)
            {
                animDataDelayedString[index].target.GetComponent<InputField>().text = animDataDelayedString[index].text;
            }
        }
    }
}