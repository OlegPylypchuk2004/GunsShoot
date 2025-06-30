using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Patterns.ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;

        public ObjectPool(T prefab, int initialCount)
        {
            _prefab = prefab;
            _objects = new List<T>();

            for (int i = 0; i < initialCount; i++)
            {
                T gameObject = GameObject.Instantiate(_prefab);
                gameObject.gameObject.SetActive(false);

                _objects.Add(gameObject);
            }
        }

        public T Get()
        {
            T gameObject = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (gameObject == null)
            {
                gameObject = Create();
            }

            gameObject.gameObject.SetActive(true);

            return gameObject;
        }

        public void Release(T gameObject)
        {
            gameObject.gameObject.SetActive(false);
        }

        private T Create()
        {
            T gameObject = GameObject.Instantiate(_prefab);
            _objects.Add(gameObject);

            return gameObject;
        }
    }
}