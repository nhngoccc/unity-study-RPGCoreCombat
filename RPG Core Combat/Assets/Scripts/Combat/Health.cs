using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int health = 100;
    public void TakeDamager(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log("Current health" + health);
    }
}