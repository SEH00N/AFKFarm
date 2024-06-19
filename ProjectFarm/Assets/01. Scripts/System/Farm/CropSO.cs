using UnityEngine;

namespace H00N.Farms
{
    [CreateAssetMenu(menuName = "SO/Farm/Crop")]
    public class CropSO : ScriptableObject
    {
        public string CropName;
        public Sprite[] Growth;
        public int GrowthStepCount => Growth.Length;
        public int GrowthRate;
        public Crop CropPrefab;
    }
}
