using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YG
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
        [SerializeField] int worldSceneIndex = 1;

        private void Awake()
        {
            //There can only be one instance of this script at one time. If there's another, destroy it.
            if (instance == null)
            {
                instance = this;
            } else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }
        
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}