using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDeath;


    private void Awake()
    {
        currentHealth = maxHealth
;
    }


    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;       

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {

            Die();

        }
    }

    [ContextMenu("Self Destruct")]
    public virtual void Die()
    {        
        if (OnDeath != null)
        {
            OnDeath();
        }
    }
}
