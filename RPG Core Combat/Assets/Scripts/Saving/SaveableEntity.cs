using UnityEngine;
namespace RPG.Saving
{
    public class SaveableEntity : MonoBehaviour
    {
        public string GetUUID()
        {
            return "aaaaa";
        }
        public object CaptureState()
        {
            return null;
            print("CaptureSate by ID" + GetUUID());
        }
        public void RestoreState(object state)
        {
            print("Restore state ID " + GetUUID());
        }


    }
}


