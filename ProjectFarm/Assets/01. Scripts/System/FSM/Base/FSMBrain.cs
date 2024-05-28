using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace H00N.FSM
{
    public class FSMBrain : MonoBehaviour
    {
        // <prev, new>
        public UnityEvent<FSMState, FSMState> OnStateChangedEvent = null;

        [Space(15f)]
        [SerializeField] List<FSMParamSO> fsmParams = null;
        private Dictionary<Type, FSMParamSO> fsmParamDictionary = null;

        [Space(15f)]
        [SerializeField] FSMState currentState = null;
        public FSMState CurrentState => currentState;

        protected virtual void Awake()
        {
            Transform statesTrm = transform.Find("States");

            fsmParamDictionary = new Dictionary<Type, FSMParamSO>();
            fsmParams.ForEach(i => {
                Type type = i.GetType();
                if (fsmParamDictionary.ContainsKey(type))
                    return;
                fsmParamDictionary.Add(type, ScriptableObject.Instantiate(i));
            });

            List<FSMState> states = new List<FSMState>();
            statesTrm.GetComponentsInChildren<FSMState>(states);
            states.ForEach(i => i.Init(this));
        }

        protected virtual void Start()
        {
            currentState?.EnterState();
        }

        protected virtual void Update()
        {
            currentState?.UpdateState();
        }

        private void OnValidate()
        {
            if (transform.Find("States") == null)
                new GameObject("States").transform.SetParent(transform);
        }

        public void ChangeState(FSMState targetState)
        {
            OnStateChangedEvent?.Invoke(currentState, targetState);

            currentState?.ExitState();
            currentState = targetState;
            currentState?.EnterState();
        }

        public T GetFSMParam<T>() where T : FSMParamSO
        {
            return fsmParamDictionary[typeof(T)] as T;
        }
    }
}