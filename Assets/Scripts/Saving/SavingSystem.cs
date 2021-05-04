using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string savingFile)
        {
            string path = GetPathFromSaveFile(savingFile);
            print("Saved to: " + path); ;
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();
                byte[] buffer = SerializeVector(playerTransform.position);
                stream.Write(buffer, 0, buffer.Length);
            }
            
        }


        public void Load(string savingFile)
        {
            string path = GetPathFromSaveFile(savingFile);
            print("Load from: " + GetPathFromSaveFile(savingFile));
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Transform playerTransform = GetPlayerTransform();
                playerTransform.position = DeserializeVector(buffer);
            }
        }

        string GetPathFromSaveFile(string savingFile)
        {
            return Path.Combine(Application.persistentDataPath, savingFile + ".sav");
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindGameObjectWithTag("Player").transform;
        }

        byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            return result;
        }

    }
}