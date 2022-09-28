using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyHitExplosion;
    [SerializeField] GameObject enemyDeathExplosion;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] int enemyHP = 3;
    [SerializeField] int points = 5;

    ScoreKeeper scoreKeeper;
    GameObject spawnAtRunTime;
    AudioSource enemyAudio;
    bool isDead;

    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        enemyAudio = FindObjectOfType<AudioSource>();
        spawnAtRunTime = GameObject.FindWithTag("SpawnAtRunTime");
    }

    private void OnParticleCollision(GameObject other) 
    {
        
        if(enemyHP != 0)
        {
            ProcessHit();
        }
        else if(enemyHP == 0)
        {
            Die();
        }
           
    }

    void ProcessHit()
    {
        enemyHP -= 1;
        enemyAudio.PlayOneShot(explosionSFX);
        GameObject vfx = Instantiate(enemyHitExplosion,transform.position,Quaternion.identity);
        vfx.transform.SetParent(spawnAtRunTime.transform);
        
    }

    void Die()
    {
        if(!isDead)
        {
            isDead = true;
            scoreKeeper.UpdateScore(points);
            enemyAudio.PlayOneShot(explosionSFX);
            GameObject vfx = Instantiate(enemyDeathExplosion,transform.position,Quaternion.identity);
            vfx.transform.SetParent(spawnAtRunTime.transform);
            Destroy(gameObject, 1f); 
        }

    }

}
