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
                GetComponent<SavingWrapper>().SaveFile(fileName);
            }
            if(Input.GetKey(KeyCode.L))
            {
                GetComponent<SavingWrapper>().LoadFile(fileName);
            }
        }

    }
}
