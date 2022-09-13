using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Skrptr.Components
{
    /// <summary>
    /// Base Class for any animation. Difference between an animation and an Action is the lack of looping possibility and Ease of DoTeen
    /// </summary>
    public abstract class SkrptrAnim : SkrptrAction
    {
        /// <summary>
        /// Contains indexes of animations which should be looped, and for how long.
        /// </summary>
        protected Dictionary<int,float> LoopIndexDurations;

        /// <summary>
        /// States whether current animation is looping or not.
        /// </summary>
        public bool IsLooping = false;

        private int coroutineCounter = 0;

        /// <summary>
        /// Used to initialize looping values for LOOP animations.
        /// </summary>
        protected virtual void InitLoopingAnims() { }

        /// <summary>
        /// Executes a single index of the animData. Must be overriden for any animation that needs looping.
        /// </summary>
        /// <param name="index"></param>
        protected virtual void ExecuteSingle(int index) { }
        protected virtual void Start()
        {
            InitLoopingAnims();
        }
        protected virtual void Awake()
        {
            InitLoopingAnims();
        }

        /// <summary>
        /// Starts looping animations if it isn't already running.
        /// </summary>
        public void StartLoopingAnims(float delay = 0)
        {
            StartCoroutine(StartLooping(delay));
        }
        private IEnumerator StartLooping(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            if (!IsLooping)
            {
                if (LoopIndexDurations != null)
                {
                    if (LoopIndexDurations.Count > 0)
                    {
                        IsLooping = true;                        
                        StartCoroutine(LoopAnimations());
                    }
                }
            }
        }

        /// <summary>
        /// Stops looping animations. Animation will first end before it stops.
        /// </summary>
        public void StopLoopingAnims(float delay = 0)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(SetIsLooping(false, delay));           
        }
        private IEnumerator SetIsLooping(bool flag, float delay = 0)
        {
            if (delay != 0)
                yield return new WaitForSeconds(delay);
            DOTween.Kill(transform);
            IsLooping = flag;
        }
       
        /// <summary>
        /// Async Method to loop animations correctly based on an index of an animData.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator LoopAnimations()
        {
            int localCoroutineCounter = coroutineCounter++;
            float loopDuration = 0;
            for (int i = 0; i < LoopIndexDurations.Count; i++)
            {
                loopDuration += LoopIndexDurations.ElementAt(i).Value;
                ExecuteSingle(LoopIndexDurations.ElementAt(i).Key);
            }
            
            while (IsLooping)
            {
                for (int i = 0; i < LoopIndexDurations.Count; i++)
                {
                    ExecuteSingle(LoopIndexDurations.ElementAt(i).Key);
                }
                yield return new WaitForSeconds(loopDuration);
                if (localCoroutineCounter + 1 != coroutineCounter)
                    break;
            }
        }       
    }


}
