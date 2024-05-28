using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace H00N.FSM
{
    public class FSMState : MonoBehaviour
    {
        [SerializeField] bool autoTransitioning = true;

        [Space(15f)]
        [SerializeField] UnityEvent onStateEnterEvent = null;
        [SerializeField] UnityEvent onStateExitEvent = null;

        private FSMBrain brain;

        private List<FSMAction> actions;
        private List<FSMTransition> transitions;

        public void Init(FSMBrain brain)
        {
            this.brain = brain;

            actions = new List<FSMAction>();
            GetComponents<FSMAction>(actions);
            actions.ForEach(i => i.Init(brain, this));

            transitions = new List<FSMTransition>();
            GetComponentsInChildren<FSMTransition>(transitions);
            transitions.ForEach(i => i.Init(brain, this));
        }

        public void EnterState()
        {
            actions.ForEach(i => i.EnterState());

            if (autoTransitioning)
                transitions.ForEach(i => i.EnterState());

            onStateEnterEvent?.Invoke();
        }

        public void UpdateState()
        {

            if (autoTransitioning)
            {
                foreach (FSMTransition transition in transitions)
                {
                    if (transition.CheckDecisions())
                    {
                        brain.ChangeState(transition.TargetState);
                        return;
                    }
                }
            }

            actions.ForEach(i => i.UpdateState());
        }

        public void ExitState()
        {
            actions.ForEach(i => i.ExitState());

            if (autoTransitioning)
                transitions.ForEach(i => i.ExitState());

            onStateExitEvent?.Invoke();
        }
    }
}