using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr.Components.Audio
{
    /// <summary>
    /// Sets the fill amount based on the current time of the audioSource and the duration of it's clip.
    /// </summary>
    public class SkrptrAnimSetFillFromAudioSource : MonoBehaviour
    {
        public GameObject audioSource;
        public GameObject imageToFill;

        private Image img;
        private AudioSource audioS;

        private void Awake()
        {
            audioS = audioSource.GetComponent<AudioSource>();
            img = imageToFill.GetComponent<Image>();

            if (img == null)
            {
                Debug.LogWarning("Couldn't find Image component to fill on: " + gameObject.name + " from Set Fill From Audio Source Component");
            }
            else
            {
                if (img.type != Image.Type.Filled)
                {
                    Debug.LogWarning("Image type not set to Fill on " + imageToFill.name + " from Set Fill From Audio Source Component on " + gameObject.name);
                }
            }
            if (audioS == null)
            {
                Debug.LogWarning("Couldn't find AudioSource component on: " + gameObject.name + " from Set Fill From Audio Source Component");
            }
        }


        // Update is called once per frame
        void Update()
        {
            //img.fillAmount = audioS.time / audioS.clip.length;
        }
    }
}