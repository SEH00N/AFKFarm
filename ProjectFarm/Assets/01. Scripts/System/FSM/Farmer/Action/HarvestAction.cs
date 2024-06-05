using H00N.Characters;
using H00N.Farms;
using OMG.Extensions;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class HarvestAction : FarmerAction
    {
        [SerializeField] float distanceMargin = 0.5f;
        [SerializeField] float harvestDuration = 1f;

        private Farmer farmer = null;
        private CharacterMovement movement = null;

        private Field targetField = null;

        private bool isHarvesting = false;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);

            movement = brain.GetComponent<CharacterMovement>();
            farmer = brain.GetComponent<Farmer>();
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

            if(isHarvesting)
                return;

            float distance = (targetField.transform.position - transform.position).magnitude;
            if (distance > distanceMargin)
                return;

            isHarvesting = true;

            movement.StopImmediately();
            StartCoroutine(this.DelayCoroutine(harvestDuration, Harvest));
        }

        public override void ExitState()
        {
            base.ExitState();

            isHarvesting = false;
            targetField = null;
        }

        private void Harvest()
        {
            targetField.Harvest();
            brainParam.ActionFinished = true;
        }

        protected override bool ActionPossibility()
        {
            return farmer.CurrentFarm.GetFruitionField(out targetField);
        }
    }
}
