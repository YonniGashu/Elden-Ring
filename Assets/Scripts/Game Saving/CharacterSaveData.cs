using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    // SINCE WE WANT TO REFERENCE THIS DATA FOR EVERY SAVE FILE, THIS SCRIPT IS NOT A MONOBEHAVIOR AND IS INSTEAD SERIALIZABLE
    public class CharacterSaveData
    {
        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public float secondsPlayed;

        // Q: WHY NOT USE A VECTOR3?
        // A: CAN ONLY SAVE DATA FROM BASIC/PRIMITIVE VARIABLE TYPES. NOT UNITY SPECIFIC.
        [Header("World Coordinates")]
        public float xCoord;
        public float yCoord;
        public float zCoord;
    }
}