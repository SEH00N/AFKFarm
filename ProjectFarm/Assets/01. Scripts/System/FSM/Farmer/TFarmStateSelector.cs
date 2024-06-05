using System.Collections.Generic;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class TFarmStateSelector : MonoBehaviour
    {
        private BrainParam brainParam = null;

        private Dictionary<FarmerStateType, FarmerState> states = null;
        private FSMBrain brain = null;

        private void Awake()
        {
            brain = GetComponent<FSMBrain>();

            List<FarmerState> stateList = new List<FarmerState>();
            transform.Find("States").GetComponentsInChildren<FarmerState>(stateList);

            states = new Dictionary<FarmerStateType, FarmerState>();
            stateList.ForEach(state => {
                if(states.ContainsKey(state.StateType))
                    return;

                states.Add(state.StateType, state);
            });
        }

        private void Start()
        {
            brainParam = brain.GetFSMParam<BrainParam>();
            brainParam.ActionFinished = true;
        }

        private void Update()
        {
            if(brainParam.ActionFinished == false)
                return;
            brainParam.ActionFinished = false;
            
            int stateData = Random.Range(0, (int)FarmerStateType.END);
            FarmerStateType stateType = (FarmerStateType)stateData;

            if(states.ContainsKey(stateType) == false)
                return;

            FarmerState state = states[stateType];
            brain.ChangeState(state);
        }
    }
}
