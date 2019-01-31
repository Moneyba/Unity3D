using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper : MonoBehaviour {

    public static int score { get; private set; }
    public static int timer { get; private set; }

    float lastEnemyKillTime;
    int streakCount;
    float streakExpiryTime = 1f;   

    // Use this for initialization
    void Start () {
           
        EnemyStats.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<PlayerStats>().OnDeath += OnPlayerDeath;

        timer = 60;
        score = 1;
        StartCoroutine("LoseTime");
       

    }

    private void Update()
    {
        if(timer <= 0)
        {
            StopCoroutine("LoseTime");
            FindObjectOfType<PlayerStats>().Die();
        }

        if(score == 0)
        {
            StopCoroutine("LoseTime");
            FindObjectOfType<PlayerStats>().Win();
        }
    }

    void OnEnemyKilled()
    {
        if(Time.time < lastEnemyKillTime + streakExpiryTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }
        lastEnemyKillTime = Time.time;
        timer += 10 + (int)Mathf.Pow(2,streakCount);
        score--;
    }

    void OnPlayerDeath()
    {
        EnemyStats.OnDeathStatic -= OnEnemyKilled;
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer--;           
        }
       
    }
}
