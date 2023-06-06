using UnityEngine;
namespace RPG.Saving
{
    [System.Serializable]
    public class PlayerData
    {
        public Vector3 playerPos;
        public float playerHealth;

        // public Vector3 GetPlayerPos()
        // {
        //     Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        //     return playerPos;
        // }
        public PlayerData()
        {
            
        }


    }
}
