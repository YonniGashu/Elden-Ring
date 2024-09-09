using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;

namespace YG
{
    public class TitleScreenManager : MonoBehaviour 
    { 
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        public void StartNewGame()
        {
            WorldSaveGameManager.instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }
    }
    
}
