using H00N.Farms;
using UnityEngine;

namespace H00N
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance = null;
        public static GameManager Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Time.timeScale = 5f;
        }
    }
}
