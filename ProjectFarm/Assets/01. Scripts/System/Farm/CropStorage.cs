using System.Collections.Generic;
using UnityEngine;

namespace H00N.Farms
{
    public class CropStorage : MonoBehaviour
    {
        private Dictionary<CropSO, int> storages = null;

        private void Awake()
        {
            storages = new Dictionary<CropSO, int>();
        }

        public void AddCrop(Crop crop)
        {
            if(storages.ContainsKey(crop.CropData) == false)
                storages.Add(crop.CropData, 0);

            storages[crop.CropData]++;
        }
    }
}
