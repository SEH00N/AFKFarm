using System;
using UnityEngine;

namespace H00N.Dates
{
    public class DateManager : MonoBehaviour
    {
        private static DateManager instance = null;
        public static DateManager Instance => instance;

        [SerializeField] float tickDuration = 7f;
        [SerializeField] int dateChangeTick = 43;

        public event Action OnTickCycleEvent = null;
        public event Action OnDateCycleEvent = null;

        private float timer = 0f;
        private int tickCounter = 0;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                tickCounter--;
                if(tickCounter <= 0)
                {
                    tickCounter = dateChangeTick;
                    HandleDateChanged();
                }

                timer = tickDuration;
                HandleTick();
            }
        }

        private void HandleDateChanged()
        {
            Debug.Log("Date Cycle");
            OnDateCycleEvent?.Invoke();
        }

        private void HandleTick()
        {
            Debug.Log("Tick Cycle");
            OnTickCycleEvent?.Invoke();
        }
    }
}
