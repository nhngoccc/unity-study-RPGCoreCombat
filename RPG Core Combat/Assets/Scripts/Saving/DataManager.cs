using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace RPG.Saving
{
    public class DataManager
    {
        string fileName = "a";
        public PlayerData playerData;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static DataManager instance;
        public DataManager()
        {
            playerData = Load();
        }


        public PlayerData Load()
        {
            string filePath = GetFilePath(fileName);

            return DeserializeObject(filePath);

        }

        public void Save()
        {
            string filePath = GetFilePath(fileName);
            SerializeObject(playerData, filePath);
        }

        private static void SerializeObject(PlayerData playerData, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(filePath);
            formatter.Serialize(fileStream, playerData);
            fileStream.Close();
        }
        private static PlayerData DeserializeObject(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                PlayerData playerData = (PlayerData)formatter.Deserialize(fileStream);
            }
            else
            {
                FileStream newFileSteam = new 
            }

            return playerData;
        }
        private string GetFilePath(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile);
        }
    }
}

