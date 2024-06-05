using H00N.Dates;
using UnityEngine;

namespace H00N.Farms
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private CropSO cropData = null;
        public bool IsEmpty => cropData == null;
        public bool IsFruition => (IsEmpty == false) && growth >= (cropData.GrowthStepCount - 1);

        private bool isWatered = false;
        public bool IsWatered {
            get => isWatered;
            set {
                isWatered = value;
                wet.SetActive(isWatered);
            }
        }

        private GameObject wet = null;
        private SpriteRenderer visual = null;

        private int tickCounter = 0;
        private int growth = 0;

        #if UNITY_EDITOR
        [ContextMenu("Plant")]
        public void TestPlant()
        {
            PlantCrop(cropData);
        }

        [ContextMenu("Watering")]
        public void TestWatering()
        {
            Watering();
        }
        #endif

        private void Awake()
        {
            wet = transform.Find("Wet").gameObject;
            visual = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public void PlantCrop(CropSO crop)
        {
            cropData = crop;
            growth = -1;

            Grow();
            DateManager.Instance.OnTickCycleEvent += HandleTickCycle;
        }

        public void Harvest()
        {
            if(IsFruition == false)
                return;

            Instantiate(cropData.CropPrefab, transform.position, Quaternion.identity);
            cropData = null;
            visual.sprite = null;
        }

        public void Watering()
        {
            IsWatered = true;
        }

        private void Grow()
        {
            growth++;
            visual.sprite = cropData.Growth[growth];
            IsWatered = false;

            if(IsFruition)
            {
                DateManager.Instance.OnTickCycleEvent -= HandleTickCycle;
            }
        }

        private void HandleTickCycle()
        {
            if(IsWatered == false)
                return;

            tickCounter += 1;
            if(tickCounter >= cropData.GrowthRate)
            {
                Grow();
                tickCounter -= cropData.GrowthRate;
            }
        }
    }
}
