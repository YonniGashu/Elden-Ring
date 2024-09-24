 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

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
        

        [Header("Notifications")]
        [SerializeField] GameObject noCharacterSlotsNoti;
        [SerializeField] Button noCharacterSlotsClose;
        [SerializeField] GameObject deleteCharacterSlotNoti;
        [SerializeField] Button deleteCharacterNotiConfirmButton;
        [SerializeField] Button deleteCharacterNotiCancelButton;

        [Header("Character Slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

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
            titleScreenMainMenu.SetActive(false);
            noCharacterSlotsNoti.SetActive(true);
            noCharacterSlotsClose.Select();
        }
    
        public void CloseNoFreeCharacterSlotsNoti() {
            noCharacterSlotsNoti.SetActive(false);
            titleScreenMainMenu.SetActive(true);
            mainMenuNewGameButton.Select();
        }

        #region CHARACTER SLOTS
        public void SelectCharacterSlot(CharacterSlot characterSlot) {
            currentSelectedSlot = characterSlot;
        }

        public void SelectNoSlot() {
            currentSelectedSlot = CharacterSlot.NO_SLOT;
        }
        
        public void AttemptToDeleteCharacterSlot() {
            if (currentSelectedSlot != CharacterSlot.NO_SLOT) {
                deleteCharacterSlotNoti.SetActive(true);
                deleteCharacterNotiCancelButton.Select();
            }
        }

        public void DeleteCharacterSlot() {
            deleteCharacterSlotNoti.SetActive(false);
            WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);

            // REFRESH THE LOAD MENU
            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharacterNoti() {
            deleteCharacterSlotNoti.SetActive(false);
            loadMenuReturnButton.Select();
        }
        #endregion
    }
    
}
