using Skrptr.Input;
using Skrptr.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Skrptr
{
    /// <summary>
    /// Data structures to hold information for different kind of animations. 
    /// I am afraid I didn't find a more optimal solution without using something like Odin Inspector.
    /// </summary>

    [System.Serializable]
    public class AnimData
    {
        [BitMask(typeof(SkrptrEvent))]
        public SkrptrEvent skrptrEvent;       
        public GameObject target ;

        public virtual void IsValid(object sender)
        {
            if(target == null)
            {
                Debug.LogWarning($"Target has been not assigned on gameobject '{sender.ToString()}'");
            }
        }
    }
    [System.Serializable]
    public class AnimDataDuration : AnimData
    {
        public float duration;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if(duration < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                duration = 0;
            }
        }
    }
    [System.Serializable]
    public class AnimDataDelayed:AnimData
    {
        public float delay;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (delay < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                delay = 0;
            }
        }
    }

    [System.Serializable]
    public class AnimDataDurationDelayed : AnimDataDuration
    {
        public float delay;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (delay < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                delay = 0;
            }
        }
    }
    [System.Serializable]
    public class AnimDataDelayedString:AnimDataDelayed
    {
        public string text;
    }

    [System.Serializable]
    public class AnimDataFloatDurationDelay : AnimDataDurationDelayed
    {
        [Range(0,1)]
        public float normalizedValue;
    }
    [System.Serializable]
    public class AnimDataColor : AnimDataDurationDelayed
    {
        public Color targetColor;     
    }
    [System.Serializable]
    public class AnimDataVector3 : AnimDataDurationDelayed
    {
        public Vector3 targetV3;
    }
    [System.Serializable]
    public class AnimDataSlideOutside : AnimDataDurationDelayed
    {        
        public SlideDirection slideDirection = SlideDirection.Up;
    }
    [System.Serializable]
    public class AnimDataGO : AnimDataDurationDelayed
    {
        public GameObject targetGameObject;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (target == null)
            {
                Debug.LogWarning($"Target GameObject has been not assigned on gameobject '{sender.ToString()}'");
            }
        }
    }
    [System.Serializable]
    public class AnimDataRotate : AnimDataVector3
    {
        public RotateType rotateType = RotateType.Absolute;
    }
    [System.Serializable]
    public class AnimDataVideoClip : AnimDataDelayed
    {
        public VideoClip videoClip;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (videoClip == null)
            {
                Debug.LogWarning($"Video Clip has been not assigned on gameobject '{sender.ToString()}'");
            }
        }
    }
    [System.Serializable]
    public class AnimDataSprites : AnimDataDurationDelayed
    {
        public List<Sprite> sprites;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (sprites?.Count == 0)
            {
                Debug.LogWarning($"Sprites has been not assigned on gameobject '{sender.ToString()}'");
            }
        }
    }
    [System.Serializable]
    public class AnimDataString
    {
        [BitMask(typeof(SkrptrEvent))]
        public SkrptrEvent skrptrEvent;
        public string stringValue;
        public virtual void IsValid(object sender) { }
    }
    [System.Serializable]
    public class AnimDataStringDuration : AnimDataString
    {
        public float duration;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (duration < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                duration = 0;
            }
        }
    }
    [System.Serializable]
    public class AnimDataStringDelay : AnimDataString
    {
        public float delay;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (delay < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                delay = 0;
            }
        }
    }
    [System.Serializable]
    public class AnimDataStringDurationDelay : AnimDataStringDuration
    {
        public float delay;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (delay < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                delay = 0;
            }
        }
    }
    [System.Serializable]
    public class AnimDataStringDurationDelayFloat : AnimDataStringDurationDelay
    {
        public float value;
    }
    [System.Serializable]
    public class AnimDataStringDurationDelayed : AnimDataStringDuration
    {
        public float delay;
        public override void IsValid(object sender)
        {
            base.IsValid(sender);
            if (delay < 0)
            {
                Debug.LogWarning($"Found negavite duration on: '{sender.ToString()}' - setting to 0. Sorry, no time travelling!");
                delay = 0;
            }
        }
    }
   

}
