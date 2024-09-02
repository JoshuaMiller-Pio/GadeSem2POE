using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Attackers
{
    public class MeleeEnemy : EnemySuper, IEnemy
    {
    
        // Start is called before the first frame update
        void Start()
        {
            waypointsPassed = 1;
            _navMesh = gameObject.GetComponent<NavMeshAgent>();
            _enemySpawnManager =
                GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            playerTower = GameObject.FindGameObjectWithTag("Player");
            playerBrain = playerTower.GetComponent<Player.Player>();
            myPath = _enemySpawnManager.spawnPoint;
            currentHealth = enemyScript.maxHealth;
            value = enemyScript.value;
            damage = enemyScript.damage;
            moveSpeed = enemyScript.moveSpeed;
            attackSpeed = enemyScript.attackSpeed;
            _navMesh.speed = moveSpeed;
            _navMesh.destination = _gameManager.pathWaypoints[Convert.ToInt32(myPath)].positions[waypointsPassed];
        }

        public void Attack()
        {
            playerBrain.TakeDamage(damage);
            for (int i = 0; i < _gameManager.spawnedEnemies.Count ; i++)
            {
                if (gameObject == _gameManager.spawnedEnemies[i])
                {
                    _gameManager.spawnedEnemies.Remove(gameObject);
                    Destroy(gameObject);
                    if (_gameManager.spawnedEnemies.Count <= 0)
                    {
                        _gameManager.RoundEnd();
                    }
                }
            }
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
            }
            
            
        }

        private void OnTriggerExit(Collider other)
        {
           
        }

        private void OnCollisionEnter(Collision other)
        {
           
        }

        void ResetStateIfStuck()
        {
            
       
            if (positionHistory.Count > 5)
            {
                positionHistory.RemoveAt(0);
            }
            positionHistory.Add(transform.position);

            foreach (Vector3 historicPoint in positionHistory)
            {
                if (historicPoint != transform.position)
                {
                    return;
                }
            }
        
            UpdateTarget();
        }
        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float incomingDamage)
        {
            currentHealth -= incomingDamage;
            if (this.currentHealth <= 0)
            {
                Die();
            }
        }

        public void UpdateTarget()
        {
            waypointsPassed += 1;
            if (waypointsPassed >= 17)
            {
                _navMesh.destination = playerTower.transform.position;
            }
            _navMesh.destination = _gameManager.pathWaypoints[Convert.ToInt32(myPath)].positions[waypointsPassed];
        }
        public void Die()
        {
            
           // StopCoroutine("AttackTower");
            playerBrain.currentGold += value;
            _gameManager.deadEnemies += 1;
            for (int i = 0; i < _gameManager.spawnedEnemies.Count; i++)
            {
                if (this.gameObject == _gameManager.spawnedEnemies[i])
                {
                    _gameManager.spawnedEnemies.Remove(this.gameObject);
                    
                    if (_gameManager.spawnedEnemies.Count <= 0)
                    {
                        _gameManager.RoundEnd();
                    }
                    Destroy(this.gameObject);
                }
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            ResetStateIfStuck();
            if (gameObject.transform.position.x == _navMesh.destination.x && gameObject.transform.position.z == _navMesh.destination.z)
            {
                UpdateTarget();
            }
        }
    }
}
