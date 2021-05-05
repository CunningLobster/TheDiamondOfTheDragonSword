using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public object BynaryFormatter { get; private set; }

        public void Save(string savingFile)
        {
            string path = GetPathFromSaveFile(savingFile);
            print("Saved to: " + path); ;
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }

        }

        public void Load(string savingFile)
        {
            string path = GetPathFromSaveFile(savingFile);
            print("Load from: " + GetPathFromSaveFile(savingFile));
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        string GetPathFromSaveFile(string savingFile)
        {
            return Path.Combine(Application.persistentDataPath, savingFile + ".sav");
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
        }

        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                 saveable.RestoreState(stateDict[saveable.GetUniqueIdentifier()]);
            }
        }
    }
}