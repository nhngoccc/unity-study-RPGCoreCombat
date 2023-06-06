using UnityEngine;
namespace RPG.Saving
{
    // [ExecuteAlways] //alway running this scripts
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string UUID = System.Guid.NewGuid().ToString();
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


