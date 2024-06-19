using UnityEngine;

namespace H00N.Farms
{
    public class Crop : MonoBehaviour
    {
        [SerializeField] CropSO cropData = null;
        public CropSO CropData => cropData;

        public static int CropCount = 0;

        private void Awake()
        {
            CropCount++;
        }

        private void OnDestroy()
        {
            CropCount--;
        }
    }
}
