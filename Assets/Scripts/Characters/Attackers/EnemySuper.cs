using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Attackers
{
    public class EnemySuper : MonoBehaviour
    {
        public static event Action removeFromTarget;
        public List<Vector3> positionHistory = new List<Vector3>(); 
        public EnemySpawnManager _enemySpawnManager;
        public NavMeshAgent _navMesh;
        public GameManager _gameManager;
        public GameObject playerTower;
        public Player.Player playerBrain;
        public EnemyScriptable enemyScript;
        public GameObject target;
        public float currentHealth, damage, moveSpeed, attackSpeed, value, myPath;

        public int waypointsPassed;
        // Start is called before the first frame update
        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
