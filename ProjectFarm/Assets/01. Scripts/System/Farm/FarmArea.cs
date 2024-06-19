using System.Collections.Generic;
using H00N.Dates;
using UnityEngine;

namespace H00N.Farms
{
    public class FarmArea : MonoBehaviour
    {
        #region Test 
        #endregion
        [SerializeField] private List<Farm> farms = new List<Farm>();
        [SerializeField] CropStorage storage = null;
        public CropStorage Storage => storage;

        #if UNITY_EDITOR
        public int DriedFieldCount = 0;
        public int FruitionFieldCount = 0;
        public int EmptyFieldCount = 0;
        #endif

        private void Awake()
        {
            #if UNITY_EDITOR
            DateManager.Instance.OnTickCycleEvent += HandleTickCycle;
            #endif
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") == false)
                return;

            if(other.TryGetComponent<Farmer>(out Farmer farmer))
                farmer.SetCurrentFarm(this);
        }

        public bool GetField(out Field field, FieldState condition)
        {
            field = null;

            foreach(Farm i in farms)
            {
                foreach(Field j in i)
                {
                    if(j.CurrentState == condition)
                    {
                        field = j;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetField(out Farm farm, out Field field, FieldState condition)
        {
            field = null;
            farm = null;

            foreach(Farm i in farms)
            {
                foreach(Field j in i)
                {
                    if(j.CurrentState == condition)
                    {
                        farm = i;
                        field = j;
                        return true;
                    }
                }
            }

            return false;
        }

        public void AddFarm(Farm farm)
        {
            farms.Add(farm);
        }

        public void RemoveFarm(Farm farm)
        {
            farms.Remove(farm);
        }

        #if UNITY_EDITOR
        private void HandleTickCycle()
        {
            DriedFieldCount = 0;
            EmptyFieldCount = 0;
            FruitionFieldCount = 0;

            foreach(Farm i in farms)
            {
                foreach(Field j in i)
                {
                    switch(j.CurrentState)
                    {
                        case FieldState.Empty:
                            EmptyFieldCount++;
                            break;
                        case FieldState.Dried:
                            DriedFieldCount++;
                            break;
                        case FieldState.Fruition:
                            FruitionFieldCount++;
                            break;
                    }
                }
            }
        }
        #endif
    }
}
