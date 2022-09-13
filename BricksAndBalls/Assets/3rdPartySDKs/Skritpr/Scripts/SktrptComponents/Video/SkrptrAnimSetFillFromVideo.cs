using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Skrptr.Components.Video
{
    /// <summary>
    /// Sets the fill amount based on the current time of the video and the duration of it's clip.
    /// </summary>
    public class SkrptrAnimSetFillFromVideo : MonoBehaviour
    {
        public GameObject videoPlayer;
        public GameObject imageToFill;

        private Image img;
        private VideoPlayer videoP;

        private void Awake()
        {
            videoP = videoPlayer.GetComponent<VideoPlayer>();
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
            if (videoP == null)
            {
                Debug.LogWarning("Couldn't find Video Player component on: " + gameObject.name + " from Set Fill From Audio Source Component");
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (videoP.isPlaying)
                img.fillAmount = (float)(videoP.time / videoP.clip.length);
        }
    }
}
