using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    bool isDeath;
    public bool IsDeath()
    {
        return isDeath;
    }
    public void TakeDamager(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log("Current health" + health);
        if(health == 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isDeath == true) return;
        else
        {
            GetComponent<Animator>().SetTrigger("die");
            isDeath = true;
        }
    }
}