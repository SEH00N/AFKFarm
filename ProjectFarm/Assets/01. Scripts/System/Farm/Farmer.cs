using UnityEngine;

namespace H00N.Farms
{
    public class Farmer : MonoBehaviour
    {
        [SerializeField] FarmArea currentFarm = null;
        public FarmArea CurrentFarm => currentFarm;

        public void SetCurrentFarm(FarmArea farm)
        {
            currentFarm = farm;
        }
    }
}