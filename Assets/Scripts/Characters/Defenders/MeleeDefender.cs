using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Defenders
{
    public class MeleeDefender : DefenderSuper, IDefender
    {
        // Start is called before the first frame update
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
           /* while (targets.Count > 0)
            {
                target = targets[0];
                gameObject.transform.LookAt(target.transform.position);
            }*/
           target = targets[0];
           if (target != null)
           {
               StartCoroutine("Aiming");
               StartCoroutine("Shoot");
           }

           if (targets.Count <= 0)
           {
              StopCoroutine("Aiming");
              StopCoroutine("Shoot");
           }
        }

        //Periodically rotates the tower to point towards the targeted enemy
        IEnumerator Aiming()
        {
            while (targets.Count > 0)
            {
                
                gameObject.transform.LookAt(target.transform.position);
                if (targets.Count <= 0)
                {
                    break;
                }
                yield return new WaitForSeconds(1f); 
            }
        }

        //Passes this game object to the uiManager and the game manager while updating the UI
        private new void OnMouseDown()
        {
            _gameManager.TowerSelected(this.gameObject);
            _uiManager.ShowSelectedTower(this.gameObject);
        }

        //Adds an attacker that moves into range to this towers "targets" list and calls the Aim() method
        void OnTriggerEnter(Collider other)
        {
            
                if (other.gameObject.CompareTag("Attacker") && other.gameObject != target)
                {
                    targets.Add(other.gameObject);
                    Aim();
                }

                return;

        }

        //Removes an enemy that leaves this towers range from the "targets" list and if the target lists new count is less than zero stops the Aiming and Shoot coroutines
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Attacker"))
            {
                
                targets.Remove(other.gameObject);
                
                if (targets.Count > 0)
                {
                    target = targets[0];
                    Aim();
                }

               
                else
                {
                    
                    StopCoroutine("Shoot");
                    StopCoroutine("Aiming");
                    target = null;
                }
            }
        }
        public void Attack()
        {
            StartCoroutine("Shoot");
        }

        //Instantiates bullets and propels them toward appropriate targets
        IEnumerator Shoot()
        {

            while (true)
            {
               /* if (towerAim.active == true)
                {

                    
                    yield return new WaitForSeconds(1);
                }*/
                GameObject bullet = Instantiate(towerGun, towerAim.transform.position,
                    towerAim.transform.rotation);
               // magazine.Add(bullet);
                // SoundManager.Instance.playsound(Shoot_Sound.clip, Shoot_Sound) ;
               /* int counter = magazine.Count;

                if (magazine.Count < counter)
                {

                    for (int i = 0; i < magazine.Count; i++)
                    {
                        Destroy(magazine[i]);

                    }
                }*/

                yield return new WaitForSeconds(1);
            }
        }

       

       
        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null && targets.Count >= 1)
                {
                    targets.Remove(targets[i]);
                    if (targets.Count <= 0)
                    {
                        StopCoroutine("Shoot");
                    }
                }
            }

            if (target == null && targets.Count >= 1)
            {
                Aim();
            }
        }

    
    }
}
