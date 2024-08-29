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
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            playerTower = GameObject.FindGameObjectWithTag("Player");
            playerBrain = playerTower.GetComponent<Player.Player>();
            currentHealth = enemyScript.maxHealth;
            value = enemyScript.value;
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
            
            if (other.gameObject.CompareTag("Bullet"))
            {
                var bulletScript = other.gameObject.GetComponent<Bullet>();
                TakeDamage(bulletScript.dmg);
              //  Destroy(other.gameObject);
                //todo TakeDamage(); (Need to add damage variable to bullet
            }
            
        }

        private void OnCollisionEnter(Collision other)
        {
           
        }

        IEnumerator AttackTower()
        {
            playerBrain.TakeDamage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float incomingDamage)
        {
            currentHealth -= incomingDamage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
           // StopCoroutine("AttackTower");
            playerBrain.currentGold += value;
            for (int i = 0; i < _gameManager.spawnedEnemies.Count; i++)
            {
                if (this.gameObject == _gameManager.spawnedEnemies[i])
                {
                    _gameManager.spawnedEnemies.Remove(this.gameObject);
                    Destroy(this.gameObject);
                    if (_gameManager.spawnedEnemies.Count <= 0)
                    {
                        _gameManager.RoundEnd();
                    }
                }
                
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
