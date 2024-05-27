using UnityEngine;

namespace H00N.Animations
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator animator = null;

        private CharacterAnimationEvent animationEvent = null;
        public CharacterAnimationEvent AnimationEvent => animationEvent;

        private readonly int IS_WALK_HASH = Animator.StringToHash("IsWalk");
        private readonly int IS_IDLE_HASH = Animator.StringToHash("IsIdle");
        private readonly int IS_INTERACT_HASH = Animator.StringToHash("IsInteract");

        private readonly int DIRECTION_HASH = Animator.StringToHash("Direction");
        private readonly int INTERACT_TYPE_HASH = Animator.StringToHash("InteractType");

        private int interactLayerIndex = 0;

        private void Awake()
        {
            animator = transform.Find("Visual").GetComponent<Animator>();
            animationEvent = animator.GetComponent<CharacterAnimationEvent>();

            interactLayerIndex = animator.GetLayerIndex("Interact Layer");
        }

        public void ToggleIdle(bool value) => ToggleBoolean(IS_IDLE_HASH, value);
        public void ToggleWalk(bool value) =>  ToggleBoolean(IS_WALK_HASH, value);
        public void ToggleInteract(bool value)
        {
            animator.SetLayerWeight(interactLayerIndex, value ? 1 : 0);
            ToggleBoolean(IS_INTERACT_HASH, value);
        }

        public void SetDirection(float direction) => SetFloat(DIRECTION_HASH, direction);
        public void SetInteractType(float interactType) => SetFloat(INTERACT_TYPE_HASH, interactType);

        private void SetFloat(int hash, float value) => animator.SetFloat(hash, value);
        private void ToggleBoolean(int hash, bool value) => animator.SetBool(hash, value);
    }
}
