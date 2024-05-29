using UnityEngine;

namespace H00N.FSM.Farmer
{
    [CreateAssetMenu(menuName = "SO/FSM/FarmerRewardTable")]
    public class FarmerRewardTableSO : ScriptableObject
    {
        [Header("Action Selection")]
        public float AcitonSelection = 1f;
        public float ActionSelectionThreshold = 3f;

        [Header("Perform Action")]
        public float ImpossibleAction = -5f;
        public float CompleteAction = 1f;
    }
}
