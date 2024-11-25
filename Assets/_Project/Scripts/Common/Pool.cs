using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Queue<T> _queue = new();

        public Pool(T prefab, int count)
        {
            _prefab = prefab;

            T item;

            for (int i = 0; i < count; i++)
            {
                item = Object.Instantiate(_prefab);
                item.gameObject.SetActive(false);
                _queue.Enqueue(item);
            }
        }

        public T Get()
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            T item = Object.Instantiate(_prefab);
            item.gameObject.SetActive(false);
            return item;
        }

        public void Return(T item)
        {
            _queue.Enqueue(item);
        }
    }
}