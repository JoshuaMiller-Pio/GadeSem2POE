using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Attackers;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody _rigComp;

    public float dmg = 6;

    public List<GameObject> explosionTargets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _rigComp = GetComponent<Rigidbody>();
        _rigComp.AddForce(transform.forward * 500, ForceMode.Force);
        StartCoroutine(ExplosionCountdown());
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Attacker"))
        {
            explosionTargets.Add(other.gameObject);
        }
     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker")  && explosionTargets.Contains(other.gameObject) == false)
        {
            explosionTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < explosionTargets.Count; i++)
        {
            if (other.gameObject == explosionTargets[i])
            {
                explosionTargets.Remove(other.gameObject);
            }
        }
    }

    public void Explode()
    {
        for (int i = 0; i < explosionTargets.Count; i++)
        {
            
            if (explosionTargets[i].name == "BasicEnemy(Clone)")
            {
                var enemyBrain = explosionTargets[i].gameObject.GetComponent<MeleeEnemy>();
                enemyBrain.TakeDamage(dmg);
            }
            if (explosionTargets[i].name == "RangedEnemy(Clone)")
            {
                var enemyBrain = explosionTargets[i].gameObject.GetComponent<RangedEnemy>();
                enemyBrain.TakeDamage(dmg);
            }
            if (explosionTargets[i].name == "TankEnemy(Clone)")
            {
                var enemyBrain = explosionTargets[i].gameObject.GetComponent<TankEnemy>();
                enemyBrain.TakeDamage(dmg);
            }
        }
        explosionTargets.Clear();
        Destroy(this.gameObject);
    }
    IEnumerator ExplosionCountdown()
    {
        int timer = 5;
        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        Explode();
        StopCoroutine("ExplosionCountdown");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
