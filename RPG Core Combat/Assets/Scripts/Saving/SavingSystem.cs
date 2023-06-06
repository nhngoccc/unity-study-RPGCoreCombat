using UnityEngine;
using System.IO;
namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        string fileName = "file o day";
        private void Update() {
            if(Input.GetKey(KeyCode.S))
            {
                DataManager.Instance.Load();
                Debug.Log(DataManager.Instance.playerData.playerHealth);
                // GetComponent<SavingWrapper>().SaveFile(fileName);
            }
            if(Input.GetKey(KeyCode.L))
            {
                DataManager.Instance.playerData.playerHealth +=1;
                DataManager.Instance.Save();
                // GetComponent<SavingWrapper>().LoadFile(fileName);
            }
        }

    }
}
