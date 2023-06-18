using UnityEngine;

namespace ZombieSniper
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] GameObject _gameObjectToPool;
        [SerializeField] int _poolSize = 20;

        GameObject _waitingPool = null;
        ObjectPool _objectPool;

        bool _nestWaitingPool;

        void Awake()
        {
            FillObjectPool();    
        }

        void OnDestroy()
        {
            DestroyObjectPool();   
        }

        public void Initialize(int poolSize, bool nestWaitingPool, GameObject gameObjectToPool)
        {
            _gameObjectToPool = gameObjectToPool;
            _nestWaitingPool= nestWaitingPool;
            FillObjectPool(poolSize);            
        }
        public void Initialize(GameObject gameObjectToPool)
        {
            _gameObjectToPool = gameObjectToPool;
            FillObjectPool();
        }

        public void FillObjectPool()
        {
            FillObjectPool(_poolSize);
        }

        public void FillObjectPool(int poolSize)
        {
            if (_gameObjectToPool == null)
            {
                return;
            }

            if (_objectPool != null && _objectPool.pooledObjects.Count > poolSize)
            {
                return;
            }

            CreateWaitingPool();

            if (_objectPool == null)
            {
                _objectPool = new ObjectPool(_gameObjectToPool);
            }
            _objectPool.CreateObjects(poolSize, _waitingPool.transform);
        }
        
        public GameObject GetPooledGameObject()
        {
            if (_objectPool == null)
            {
                Debug.LogError("object pool is null");
                return null;
            }

            return _objectPool.GetObject();
        }

        public void Clear()
        {
            if (_objectPool != null)
            {
                _objectPool.Clear();
            }
        }

        void CreateWaitingPool()
        {
            if (_waitingPool != null)
            {
                return;
            }

            _waitingPool = new GameObject();
            _waitingPool.name = string.Format("[ObjectPooler] {0}", _gameObjectToPool.name);
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(_waitingPool, this.gameObject.scene);           
            if (_nestWaitingPool)
            {
                _waitingPool.transform.SetParent(this.transform);
            }
        }

        void DestroyObjectPool()
        {
            if (_waitingPool != null)
            {
                Destroy(_waitingPool.gameObject);
            }
        }
    }
}
