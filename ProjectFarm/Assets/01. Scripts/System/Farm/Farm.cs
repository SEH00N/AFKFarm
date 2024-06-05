using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace H00N.Farms
{
    public class Farm : MonoBehaviour, IEnumerable
    {
        #region Test
        #endregion
        [SerializeField] private CropSO cropData = null;
        public CropSO CropData => cropData;

        private List<Field> fields = null;

        private void Awake()
        {
            fields = new List<Field>();
            transform.GetComponentsInChildren<Field>(fields);
        }

        public void SetCrop(CropSO crop)
        {
            cropData = crop;
        }

        public IEnumerator GetEnumerator()
        {
            return fields.GetEnumerator();
        }
    }
}
