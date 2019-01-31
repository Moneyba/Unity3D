using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    public static event System.Action OnDeathStatic;

    public override void Die()
    {
        base.Die();
        
        AudioManager.instance.PlaySound("DeathSound", transform.position);
        
       
        if (OnDeathStatic != null)
        {
            OnDeathStatic();
        }

        Destroy(gameObject);
    }
}
