using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class FarmerStateSelector : Agent
    {
        [SerializeField] FarmerStatSO stat = null;
        [SerializeField] FarmerRewardTableSO rewardTable = null;

        private Dictionary<FarmerStateType, FarmerState> states = null;
        private FSMBrain brain = null;

        private BrainParam brainParam = null;
        private FarmerStateType selectedState;

        protected override void Awake()
        {
            base.Awake();

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

        public override void CollectObservations(VectorSensor sensor)
        {
            // 현재 스테이트
            // 밭 정보
            //  - 안 갈려있는 밭, 갈려있는 밭, 씨앗이 심어져 있는 밭, 작물이 자란 밭의 갯수
            // 농부 스탯 (넣어야 되나?)
            //  - 행동 우선순위
            sensor.AddObservation((int)selectedState);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            if(brainParam.ActionFinished == false)
                return;
            brainParam.ActionFinished = false;
            
            ActionSegment<int> discreteActions = actions.DiscreteActions;
            int stateData = Mathf.Clamp(discreteActions[0], 0, (int)FarmerStateType.END);
            FarmerStateType state = (FarmerStateType)stateData;

            float baseReward = rewardTable.AcitonSelection;
            float threshold = rewardTable.ActionSelectionThreshold;
            float actionReward = (stat[state] - threshold) / (FarmerStatSO.MAX_VALUE - threshold);
            
            SetReward(baseReward * actionReward);
            SelectState(state);
        }

        private void SelectState(FarmerStateType stateType)
        {
            if(states.ContainsKey(stateType) == false)
                return;

            EndEpisode();

            FarmerState state = states[stateType];
            brain.ChangeState(state);
        }
    }
}
