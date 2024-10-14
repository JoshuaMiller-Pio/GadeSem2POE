using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;

public class BuffTower : DefenderSuper, IDefender
{
    public float buffRadius = 10f;
    public List<GameObject> buffedTowers = new List<GameObject>();
  
   void Start()
        {
            SetUp();
            atkSpd = defenderScript.atkSpeed;
            towerName = defenderScript.name;
            damage = defenderScript.damage;
            cost = defenderScript.cost;
            towerTrigger = gameObject.GetComponentInChildren<SphereCollider>();
            magazine = new List<GameObject>();
            targets = new List<GameObject>();
            
        }
    
        //Updates the towers target object and starts the aiming and shoot coroutines
        public void Aim()
        {
          
        }

        //Periodically rotates the tower to point towards the targetted enemy
     

        private void OnMouseOver()
        {
            _gameManager.TowerSelected(this.gameObject);
            _uiManager.ShowSelectedTower(this.gameObject);
        }

        public void FindTowersInRange()
        {
            
            buffedTowers.Clear();
            
            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Defender");

            foreach (GameObject tower in allTowers)
            {
                
                float distance = Vector3.Distance(transform.position, tower.transform.position);

                
                if (distance <= buffRadius && tower != this.gameObject)  
                {
                    buffedTowers.Add(tower);  
                }
            }
            BuffTowers();
        }

        public void BuffTowers()
        {
            foreach (var tower in buffedTowers)
            {
                if (tower.gameObject.name == "BasicTower(Clone)")
                {
            
                    var targetScript = tower.gameObject.GetComponent<MeleeDefender>();
                    targetScript.atkSpd = atkSpd * 2;
                }
            
                if (tower.gameObject.name == "BombTower(Clone)")
                {
                
                    var targetScript = tower.gameObject.GetComponent<AOEDefender>();
                    targetScript.atkSpd = atkSpd * 2;
                }
            }
        }

        
        public void Attack()
        {
           
        }

        
        
        // Update is called once per frame
        void Update()
        {
            
        }
    }
