using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public float attackDelay = .5f;    

    public bool Incombat { get; private set; }
    public event System.Action OnAttack;

    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(Time.time - lastAttackTime > combatCooldown)
        {
            Incombat = false;
        }
    }

    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
            Incombat = true;
            lastAttackTime = Time.time;

            AudioManager.instance.PlaySound("SwordCollide", transform.position);
        }
       
    }

    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
        if(stats.currentHealth <= 0)
        {            
            Incombat = false;
        }
    }
}
