using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    private void Awake()
    {
        currentHealth = maxHealth;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage)
    {
        damage -= Armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}
