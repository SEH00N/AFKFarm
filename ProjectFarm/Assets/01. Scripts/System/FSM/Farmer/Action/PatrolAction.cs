using H00N.Characters;
using UnityEngine;

namespace H00N.FSM.Farmer
{
    public class PatrolAction : FarmerAction
    {
        [SerializeField] float patrolInRadius = 5f;
        [SerializeField] float patrolOutRadius = 5f;
        [SerializeField] int patrolCount = 3;
        [SerializeField] int countRandomness = 2;

        private CharacterMovement movement = null;

        private Vector3 patrolPos = Vector3.zero;
        private bool isPatrolling = false;
        private int count = 0;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);

            movement = brain.GetComponent<CharacterMovement>();
        }

        public override void EnterState()
        {
            base.EnterState();

            count = patrolCount + Random.Range(-countRandomness, countRandomness);
            patrolPos = transform.position;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(brainParam.ActionFinished)
                return;

            bool isArrived = (patrolPos - transform.position).sqrMagnitude <= 0.1f;
            if(isArrived)
            {
                count--;
                if(count <= 0)
                {
                    brainParam.ActionFinished = true;
                    return;
                }

                patrolPos = Random.insideUnitCircle * Random.Range(patrolInRadius, patrolOutRadius);
                movement.SetDestination(patrolPos);

                isPatrolling = true;
            }
        }

        protected override bool ActionPossibility()
        {
            return true;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, patrolOutRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, patrolInRadius);
        }
        #endif
    }
}