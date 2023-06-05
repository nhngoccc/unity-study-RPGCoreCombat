using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {

        public void SaveFile(string fileName)
        {
            string filePath = GetPathFormSaveFile(fileName);
            Debug.Log("Save to" + GetPathFormSaveFile(fileName));
            using (FileStream steam = File.Open(filePath, FileMode.Create)) //Save bytes data to file
            {
                // Transform playerTransformData = GetPlayerTransformData();
                BinaryFormatter fommatter = new BinaryFormatter();
                // SerializableVector3 playerPos = new SerializableVector3(playerTransformData.position);
                fommatter.Serialize(steam, CaptureState());

            }

        }

        public void LoadFile(string fileName)
        {
            string filePath = GetPathFormSaveFile(fileName);
            Debug.Log("Loading from" + fileName);
            using (FileStream steam = File.Open(filePath, FileMode.Open)) //Load bytes data to console
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreCaptureState(formatter.Deserialize(steam));
                // GameObject.FindWithTag("Player").transform.position = playerPasPos.ToVector();

            }

        }


        private object CaptureState() //Save data to dictionary
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUUID()] = saveable.CaptureState(); //add ID of saveable to state(dictionary)
            }
            return state;

        }

        private void RestoreCaptureState(object saveableState)
        {
            Dictionary<string, object> stateDic = (Dictionary<string, object>)saveableState;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.RestoreState(stateDic[saveable.GetUUID()]);
            }
        }


        private string GetPathFormSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".text");
        }

        // private Transform GetPlayerTransformData()
        // {
        //     return GameObject.FindWithTag("Player").transform;
        // }
        // private byte[] SerializeVector(Vector3 v) //mã hóa dữ liệu
        // {
        //     byte[] vectorData = new byte[3 * 4]; // 3 tham số, mỗi byte chiếm 4 đơn vị
        //     BitConverter.GetBytes(v.x).CopyTo(vectorData, 0);
        //     BitConverter.GetBytes(v.y).CopyTo(vectorData, 4);
        //     BitConverter.GetBytes(v.z).CopyTo(vectorData, 8);
        //     return vectorData;


        // }
        // private Vector3 DesrializeVector(byte[] bytes) //biên dịch giữ liệu
        // {
        //     Vector3 vector3 = new Vector3();
        //     vector3.x = BitConverter.ToSingle(bytes, 0);
        //     vector3.y = BitConverter.ToSingle(bytes, 4);
        //     vector3.z = BitConverter.ToSingle(bytes, 8);
        //     return vector3;

        // }
    }

}
