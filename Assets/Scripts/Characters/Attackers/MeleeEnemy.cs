using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Attackers
{
    public class MeleeEnemy : EnemySuper, IEnemy
    {
    
        // Start is called before the first frame update
        void Start()
        {
            playerTower = GameObject.FindGameObjectWithTag("Player");
            playerBrain = playerTower.GetComponent<Player.Player>();
            currentHealth = enemyScript.maxHealth;
            damage = enemyScript.damage;
            moveSpeed = enemyScript.moveSpeed;
            attackSpeed = enemyScript.attackSpeed;
        }

        public void Attack()
        {
            StartCoroutine("AttackTower");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Attack();
            }
        }

        IEnumerator AttackTower()
        {
            
            yield return new WaitForSeconds(attackSpeed);
        }
        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float incomingDamage)
        {
            currentHealth = currentHealth - damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            //todo Add money to wallet
            Destroy(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
