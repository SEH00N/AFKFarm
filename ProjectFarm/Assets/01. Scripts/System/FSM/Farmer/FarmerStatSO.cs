using System.Collections.Generic;
using UnityEngine;

namespace H00N.FSM.Farmers
{
    [CreateAssetMenu(menuName = "SO/FSM/FarmerStat")]
    public class FarmerStatSO : ScriptableObject
    {
        [System.Serializable]
        public class StatTable
        {
            public FarmerStateType StateType;
            [Range(0, MAX_VALUE)] public int Weight;
        }

        public const int MAX_VALUE = 10;

        public float ActionDuration = 1f;
        public float Reach = 0.5f;
        public float Sight = 50f;

        [SerializeField] List<StatTable> stats = new List<StatTable>();
        private Dictionary<FarmerStateType, int> statTables = new Dictionary<FarmerStateType, int>();

        public int this[FarmerStateType stateType] => statTables[stateType];

        #if UNITY_EDITOR
        private void OnValidate()
        {
            CreateTable();    
        }
        #else
        private void OnEnable()
        {
            CreateTable();
        }
        #endif

        private void CreateTable()
        {
            statTables = new Dictionary<FarmerStateType, int>();
            stats.ForEach(t => {
                if (statTables.ContainsKey(t.StateType))
                    return;

                statTables.Add(t.StateType, t.Weight);
            });
        }
    }
}
