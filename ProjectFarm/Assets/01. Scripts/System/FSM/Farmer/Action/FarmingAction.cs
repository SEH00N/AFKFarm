using H00N.Characters;
using H00N.Extensions;
using H00N.Farms;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public abstract class FarmingAction : FarmerAction
    {
        [SerializeField] protected FarmerStatSO stat = null;
        [SerializeField] protected FieldState targetFieldCondition;

        protected Farmer farmer = null;
        protected Field targetField = null;
        protected Farm targetFarm = null;
        protected CharacterMovement movement = null;

        protected bool isActioning = false;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);

            farmer = brain.GetComponent<Farmer>();
            movement = brain.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if(brainParam.ActionFinished)
                return;

            movement.SetDestination(targetField.transform.position);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(brainParam.ActionFinished)
                return;

            if(isActioning)
                return;

            float distance = (targetField.transform.position - transform.position).magnitude;
            if (distance > stat.Reach)
                return;

            isActioning = true;

            movement.StopImmediately();
            StartCoroutine(this.DelayCoroutine(stat.ActionDuration, FarmingBehaviour));
        }

        public override void ExitState()
        {
            base.ExitState();

            isActioning = false;
            targetField = null;
            targetFarm = null;
        }

        public abstract void FarmingBehaviour();

        protected override bool ActionPossibility()
        {
            return farmer.CurrentFarm.GetField(out targetFarm, out targetField, targetFieldCondition);
        }
    }
}
