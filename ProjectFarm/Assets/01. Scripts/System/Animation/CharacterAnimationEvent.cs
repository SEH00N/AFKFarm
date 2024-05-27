using System;
using UnityEngine;

namespace H00N.Animations
{
    public class CharacterAnimationEvent : MonoBehaviour
    {
        public event Action OnAnimationEndEvent;
        public event Action OnAnimationTriggerEvent;

        public void AnimationEnd()
        {
            OnAnimationEndEvent?.Invoke();
        }

        public void AnimationTrigger()
        {
            OnAnimationTriggerEvent?.Invoke();
        }
    }
}
