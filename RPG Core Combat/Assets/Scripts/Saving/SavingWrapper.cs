using System;
using System.IO;
using System.Text;
using UnityEngine;
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
                Transform playerTransformData = GetPlayerTransformData();
                byte[] buffer = SerializeVector(playerTransformData.position);
                steam.Write(buffer, 0, buffer.Length);
            }

        }

        private Transform GetPlayerTransformData()
        {
            return GameObject.FindWithTag("Player").transform;
        }
        private byte[] SerializeVector(Vector3 v) //mã hóa dữ liệu
        {
            byte[] vectorData = new byte[3 * 4]; // 3 tham số, mỗi byte chiếm 4 đơn vị
            BitConverter.GetBytes(v.x).CopyTo(vectorData, 0);
            BitConverter.GetBytes(v.y).CopyTo(vectorData, 4);
            BitConverter.GetBytes(v.z).CopyTo(vectorData, 8);
            return vectorData;


        }
        private Vector3 DesrializeVector(byte[] bytes) //biên dịch giữ liệu
        {
            Vector3 vector3 = new Vector3();
            vector3.x = BitConverter.ToSingle(bytes, 0);
            vector3.y = BitConverter.ToSingle(bytes, 4);
            vector3.z = BitConverter.ToSingle(bytes, 8);
            return vector3;

        }

        public void LoadFile(string fileName)
        {
            string filePath = GetPathFormSaveFile(fileName);
            Debug.Log("Loading from" + fileName);
            using (FileStream steam = File.Open(filePath, FileMode.Open)) //Load bytes data to console
            {
                byte[] buffer = new byte[steam.Length];
                steam.Read(buffer, 0, buffer.Length);
                Vector3 playerPastPos = DesrializeVector(buffer);
                GameObject.FindWithTag("Player").transform.position = playerPastPos;

            }

        }
        private string GetPathFormSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".text");
        }
    }
}
