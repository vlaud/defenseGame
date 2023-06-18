using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieSniper
{
    public class ObjectPool : IDisposable
    {
        List<GameObject> _pooledObjects = new List<GameObject>();
        public List<GameObject> pooledObjects
        {
            get
            {
                return _pooledObjects;
            }
        }

        GameObject _pooledObj = null;
        Transform _parent = null;

        public ObjectPool(GameObject obj)
        {
            _pooledObj = obj;
        }

        public void Dispose() 
        {
            _pooledObj = null;

            Destroy();
        }

        public void CreateObjects(int poolSize, Transform parent)
        {
            if (_pooledObjects.Count > poolSize)
            {
                Debug.LogErrorFormat("already create objects, {0}", _pooledObjects.Count);
                return;
            }

            _parent = parent;

            for (int i = _pooledObjects.Count; i < poolSize; ++i)
            {
                NewObject(false, _parent);
            }
        }

        public GameObject GetObject()
        {
            if (_pooledObjects.Count > 0)
            {
                for (int i = 0; i < _pooledObjects.Count; ++i)
                {
                    if (!_pooledObjects[i].activeSelf)
                    {
                        _pooledObjects[i].SetActive(true);
                        return _pooledObjects[i];
                    }
                }
            }

            GameObject newObj = NewObject(true, _parent);
            return newObj;
        }

        public void ReturnObject(GameObject obj)
        {
            for (int i = 0; i < _pooledObjects.Count; ++i)
            {
                if (_pooledObjects[i] == obj)
                {
                    _pooledObjects[i].SetActive(false);
                    break;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _pooledObjects.Count; ++i)
            {
                if (_pooledObjects[i] != null)
                {
                    _pooledObjects[i].SetActive(false);
                }
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _pooledObjects.Count; ++i)
            {
                if (_pooledObjects[i] != null)
                {
                    MonoBehaviour.Destroy(_pooledObjects[i]);
                }
            }

            _pooledObjects.Clear();
        }
        
        GameObject NewObject(bool active = true, Transform parent = null)
        {
            if (_pooledObj == null)
            {
                Debug.LogError("pooledObj is null");
                return null;
            }

            GameObject newObj = GameObject.Instantiate(_pooledObj);
            if (parent != null)
            {
                newObj.transform.SetParent(parent);
            }

            newObj.name = _pooledObj.name;
            newObj.SetActive(active);
            _pooledObjects.Add(newObj);
            return newObj;
        }
    }
}
