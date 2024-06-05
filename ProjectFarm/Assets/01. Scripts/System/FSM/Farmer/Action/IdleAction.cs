using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class IdleAction : FarmerAction
    {
        [SerializeField] float idleDuration = 3f;
        [SerializeField] float durationRandomness = 1f;

        private float duration = 0f;

        public override void EnterState()
        {
            base.EnterState();

            duration = idleDuration + Random.Range(-durationRandomness, durationRandomness);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            duration -= Time.deltaTime;
            if(duration <= 0f)
                brainParam.ActionFinished = true;
        }

        protected override bool ActionPossibility()
        {
            return true;
        }
    }
}
