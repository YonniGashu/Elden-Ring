using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace YG
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        // BEFORE WE CREATE A NEW FILE, MAKE SURE THE FILE DOESN'T ALREADY EXISTS SO THAT WE DONT OVERRRIDE SAVE DATA
        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            return false;
        }

        // USED TO DELETE CHARACTER SLOTS
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        // USED TO CREATE A SAVE FILE UPON STARTING A NEW GAME 
        public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
        {
            // MAKE A PATH TO SAVE THE DATA TO
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                // CREATE THE DIRECTORY IF ITS NOT ALREADY THERE
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("CREATING SAVE FILE AT SAVE PATH " + savePath);

                // SERIALIZE THE DATA INTO JSON
                string dataToStore = JsonUtility.ToJson(characterData, true);

                // WRITE THE FILE TO OUR SYSTEM
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            } catch (Exception ex)
            {
                Debug.LogError("ERROR WHILE TRYING TO SAVE DATA CHARACTER DATA, GAME NOT SAVED" + savePath + "\n");
            }
        }

        // USED TO LOAD A SAVE FILE UPON LOADING A PREVIOUS GAME
        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;
            // MAKE A PATH TO LOAD THE FILE (A LOCATION ON THE MACHINE)
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try
                {
                    String dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader fileReader = new StreamReader(stream))
                        {
                            dataToLoad = fileReader.ReadToEnd();
                        }
                    }

                    // DESERIALIZE THE DATA FROM JSON BACK TO UNITY C#
                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                } catch (Exception ex)
                {
                    characterData = null;
                }
                
            }

            return characterData;
        }
    }
}