using H00N.Characters;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class PatrolAction : FarmerAction
    {
        [Header("Patrol Radius")]
        [SerializeField] float patrolInRadius = 5f;
        [SerializeField] float patrolOutRadius = 5f;
        
        [Header("Count")]
        [SerializeField] int patrolCount = 3;
        [SerializeField] int countRandomness = 2;
        
        [Header("Delay")]
        [SerializeField] float coolDown = 1f;
        [SerializeField] float coolDownRandomness = 0.5f;

        private CharacterMovement movement = null;

        private Vector3 patrolPos = Vector3.zero;
        private bool isCoolDown = false;
        private int count = 0;
        private float timer = 0f;

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

            isCoolDown = true;
            timer = 0f;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(brainParam.ActionFinished)
                return;
            
            bool isArrived = (patrolPos - transform.position).sqrMagnitude <= 0.1f;
            if(isArrived)
            {
                if(isCoolDown)
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0f)
                        Patrol();
                }
                else
                {
                    isCoolDown = true;
                    timer = coolDown + Random.Range(-coolDownRandomness, coolDownRandomness);
                }
            }
        }

        private void Patrol()
        {
            count--;
            if (count <= 0)
            {
                brainParam.ActionFinished = true;
                return;
            }

            isCoolDown = false;
            patrolPos = GetPatrolPos();
            movement.SetDestination(patrolPos);
        }

        private Vector3 GetPatrolPos()
        {
            return Random.insideUnitCircle * Random.Range(patrolInRadius, patrolOutRadius);
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