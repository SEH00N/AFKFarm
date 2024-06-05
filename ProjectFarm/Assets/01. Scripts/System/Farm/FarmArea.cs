using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace H00N.Farms
{
    public class FarmArea : MonoBehaviour
    {
        #region Test 
        #endregion
        [SerializeField] private List<Field> fields = new List<Field>();

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

            foreach(Field f in fields)
            {
                if(f.CurrentState == condition)
                {
                    field = f;
                    return true;
                }
            }

            return false;
        }

        public void AddField(Field field)
        {
            fields.Add(field);
        }

        public void RemoveField(Field field)
        {
            fields.Remove(field);
        }
    }
}
