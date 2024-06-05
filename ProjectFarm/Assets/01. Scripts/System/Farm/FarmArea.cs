using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace H00N.Farms
{
    public class FarmArea : MonoBehaviour
    {
        #region Test 
        #endregion
        [SerializeField] private List<Farm> farms = new List<Farm>();

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

        public void RemoveFamr(Farm farm)
        {
            farms.Remove(farm);
        }
    }
}
