 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace YG
{
    public class TitleScreenManager : MonoBehaviour 
    { 
        public static TitleScreenManager instance;

        [Header("Menus")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Buttons")]
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button mainMenuNewGameButton;

        [Header("Pop Ups")]
        [SerializeField] GameObject noCharacterSlotsNoti;
        [SerializeField] Button noCharacterSlotsClose;

        public void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        public void StartNewGame()
        {
            WorldSaveGameManager.instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu() {
            // CLOSE MAIN MENU
            titleScreenMainMenu.SetActive(false);

            // OPEN LOAD MENU
            titleScreenLoadMenu.SetActive(true);

            // SELECT THE RETURN BUTTON
            loadMenuReturnButton.Select();
        }

        public void ReturnToMainMenu() {
            // CLOSE MAIN MENU
            titleScreenLoadMenu.SetActive(false);

            // OPEN LOAD MENU
            titleScreenMainMenu.SetActive(true);

            // SELECT THE MAIN MENU LOAD GAME BUTTON
            mainMenuLoadGameButton.Select();
        }
    
        public void DisplayNoFreeCharacterSlotsNoti() {
            noCharacterSlotsNoti.SetActive(true);
            noCharacterSlotsClose.Select();
        }
    
        public void CloseNoFreeCharacterSlotsNoti() {
            noCharacterSlotsNoti.SetActive(false);
            mainMenuNewGameButton.Select();
        }
    }
    
}
