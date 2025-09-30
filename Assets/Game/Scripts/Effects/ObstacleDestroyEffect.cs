using UnityEngine;

namespace Effects
{
    public class ObstacleDestroyEffect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}