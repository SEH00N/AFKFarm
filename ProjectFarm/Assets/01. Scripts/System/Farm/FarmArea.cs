using System.Collections.Generic;
using UnityEngine;

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

        public bool GetEmptyField(out Field field)
        {
            field = null;

            foreach(Field f in fields)
            {
                if(f.IsEmpty)
                {
                    field = f;
                    return true;
                }
            }

            return false;
        }

        public bool GetFruitionField(out Field field)
        {
            field = null;

            foreach(Field f in fields)
            {
                if(f.IsFruition)
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
