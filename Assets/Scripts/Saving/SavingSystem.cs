using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public object BynaryFormatter { get; private set; }

        public IEnumerator LoadLastScene(string savingFile)
        {
            Dictionary<string, object> state = LoadFile(savingFile);
            int buildIndex = (int)state["lastSceneBuildIndex"];
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            RestoreState(state);
        }

        public void Save(string savingFile)
        {
            Dictionary<string, object> state = LoadFile(savingFile);
            CaptureState(state);
            SaveFile(savingFile, state);
        }

        public void Load(string savingFile)
        {
            RestoreState(LoadFile(savingFile));
        }

        private void SaveFile(string savingFile, object state)
        {
            string path = GetPathFromSaveFile(savingFile);
            print("Saved to: " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string savingFile)
        {
            string path = GetPathFromSaveFile(savingFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            print("Loaded from path: " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        string GetPathFromSaveFile(string savingFile)
        {
            return Path.Combine(Application.persistentDataPath, savingFile + ".sav");
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;        
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }
    }
}