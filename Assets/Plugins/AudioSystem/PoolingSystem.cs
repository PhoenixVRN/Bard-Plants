using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace WebGame
{
    public class PoolingSystem<T> : MonoBehaviour where T : Object
    {
        [Header("Count prefabs")]
        [SerializeField] private int count;
        [Header("Current prefab")]
        [SerializeField] private T prefab;
        private List<T> _pool = new List<T>();

        public virtual void Initialization()
        {
            CreatePool();
        }
    
        public virtual void CreatePool()
        {
            for (int i = 0; i < count; i++)
            {
                T newItem = Instantiate(prefab, Vector3.zero, quaternion.identity,transform);
                newItem.GameObject().SetActive(false);
                _pool.Add(newItem);
            }
        }
    
        public virtual T GetItem()
        {
            if (_pool.Count == 0)
            {
                T newItem = Instantiate(prefab, Vector3.zero, quaternion.identity, transform);
                _pool.Add(newItem);
            }
            T get = _pool[0];
            _pool.RemoveAt(0);
            return get;
        }
    
        public virtual void SetItem(T item)
        {
            item.GameObject().SetActive(false);
            _pool.Add(item);
        }
    }
}
