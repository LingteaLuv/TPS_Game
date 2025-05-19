using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Stack<T> _stack;
        private T _targetPrefab;
        private GameObject _poolObject;

        public ObjectPool(T targetPrefab, int initSize = 5) => Init(targetPrefab, initSize); 


        private void Init(T targetPrefab, int initSize)
        {
            _stack = new Stack<T>(initSize);
            _targetPrefab = targetPrefab;
            _poolObject = new GameObject($"{targetPrefab.name} Pool");

            for (int i = 0; i < initSize; i++)
            {
                CreatePooledObject();
            }
        }

        public T Get()
        {
            Debug.Log("1번");
            if(_stack.Count == 0)
            {
                CreatePooledObject();
            }
            T pooledObject = _stack.Pop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnPool(T target)
        {
            Debug.Log("2번");
            target.transform.parent = _poolObject.transform;
            target.gameObject.SetActive(false);
            _stack.Push(target);
        }

        private void CreatePooledObject()
        {
            Debug.Log("3번");
            T obj = MonoBehaviour.Instantiate(_targetPrefab);
            ReturnPool(obj);
        }
    }
}

