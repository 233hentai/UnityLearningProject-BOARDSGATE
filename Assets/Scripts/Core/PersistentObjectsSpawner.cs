using UnityEngine;
namespace BOARDSGATE.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned=false;

        void Awake(){
            if(hasSpawned) return;
            SpawnPersistentObject();
            hasSpawned=true;
        }

        void SpawnPersistentObject(){
            GameObject persistentObject=Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}

