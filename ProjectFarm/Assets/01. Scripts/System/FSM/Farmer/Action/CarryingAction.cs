using H00N.Characters;
using H00N.Extensions;
using H00N.Farms;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class CarryingAction : FarmerAction
    {
        public enum CarryingActionState
        {
            None,
            ChasingCrop,
            ChasingStorage
        }

        [SerializeField] Transform holdingPosition = null;
        private Farmer farmer = null;
        private CharacterMovement movement = null;

        private Collider2D[] containers = new Collider2D[5];
        private Crop currentCrop = null;

        private CarryingActionState carryingState;
        private Vector3 targetPosition;

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

            carryingState = CarryingActionState.ChasingCrop;
            targetPosition = currentCrop.transform.position;
            movement.SetDestination(targetPosition);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(brainParam.ActionFinished)
                return;

            if(carryingState == CarryingActionState.None)
                return;

            switch (carryingState)
            {
                case CarryingActionState.ChasingCrop:
                    HandleChasingCrop();
                    break;
                case CarryingActionState.ChasingStorage:
                    HandleChasingStorage();
                    break;
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            currentCrop = null;
            carryingState = CarryingActionState.None;
        }

        private void HandleChasingCrop()
        {
            float distance = (targetPosition - transform.position).magnitude;
            if (distance > stat.Reach)
                return;

            movement.StopImmediately();
            carryingState = CarryingActionState.None;
            StartCoroutine(this.DelayCoroutine(stat.ActionDuration, HoldCrop));
        }

        private void HandleChasingStorage()
        {
            float distance = (targetPosition - transform.position).magnitude;
            if (distance > stat.Reach)
                return;

            movement.StopImmediately();
            carryingState = CarryingActionState.None;
            StartCoroutine(this.DelayCoroutine(stat.ActionDuration, Stroage));
        }

        private void HoldCrop()
        {
            carryingState = CarryingActionState.ChasingStorage;
            targetPosition = farmer.CurrentFarm.Storage.transform.position;
            movement.SetDestination(targetPosition);

            currentCrop.transform.SetParent(holdingPosition);
            currentCrop.transform.localPosition = Vector3.zero;
        }

        private void Stroage()
        {
            farmer.CurrentFarm.Storage.AddCrop(currentCrop);
            Destroy(currentCrop.gameObject);
            brainParam.ActionFinished = true;
        }

        protected override bool ActionPossibility()
        {
            int found = Physics2D.OverlapCircleNonAlloc(transform.position, stat.Sight, containers, DEFINE.HOLDABLE_LAYER);
            if(found <= 0)
                return false;

            for(int i = 0; i < found; ++i)
                if(containers[i].TryGetComponent<Crop>(out currentCrop))
                    return true;

            return false;
        }
    }
}
