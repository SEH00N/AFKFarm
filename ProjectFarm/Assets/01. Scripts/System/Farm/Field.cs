using UnityEngine;

namespace H00N.Farms
{
    public class Field : MonoBehaviour
    {
        private CropSO cropData = null;
        public bool IsEmpty => cropData == null;

        private GameObject wet = null;
        private SpriteRenderer visual = null;

        private void Awake()
        {
            wet = transform.Find("Wet").gameObject;
            visual = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public void PlantCrop(CropSO crop)
        {
            cropData = crop;
            // Do Something
        }

        public void Harvest()
        {
            
        }
    }
}
