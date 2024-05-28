using System.Collections.Generic;
using Unity.MLAgents;

namespace H00N.FSM.Farmer
{
    public class FarmerStateSelector : Agent
    {
        private FSMBrain brain = null;
        private Dictionary<FarmerStateType, FarmerState> states = null;

        protected override void Awake()
        {
            base.Awake();

            brain = GetComponent<FSMBrain>();

            List<FarmerState> stateList = new List<FarmerState>();
            transform.Find("States").GetComponentsInChildren<FarmerState>(stateList);

            stateList.ForEach(state => {
                if(states.ContainsKey(state.StateType))
                    return;

                states.Add(state.StateType, state);
            });
        }

        private void SelectState(FarmerStateType stateType)
        {
            if(states.ContainsKey(stateType) == false)
                return;

            FarmerState state = states[stateType];
            brain.ChangeState(state);
        }
    }
}
