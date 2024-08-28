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

        IEnumerator Aiming()
        {
            while (targets.Count > 0)
            {
                gameObject.transform.LookAt(target.transform.position);
                yield return new WaitForSeconds(1f); 
            }
        }

        void OnTriggerStay(Collider other)
        {
            
                if (other.gameObject.CompareTag("Attacker") && other.gameObject != target)
                {
                    targets.Add(other.gameObject);
                    Aim();
                }

                return;

        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Attacker"))
            {
                
                targets.Remove(other.gameObject);
                target = null;
                if (targets.Count > 0)
                {
                    target = targets[0];
                    Aim();
                }
                
                else
                {
                    target = null;
                    StopCoroutine("Shoot");
                    StopCoroutine("Aiming");
                }
            }
        }
        public void Attack()
        {
            StartCoroutine("Shoot");
        }

        IEnumerator Shoot()
        {

            while (true)
            {


                if (towerAim.active == true)
                {

                    GameObject bullet = Instantiate(towerGun, towerAim.transform.position,
                        towerAim.transform.rotation);
                    magazine.Add(bullet);
                    // SoundManager.Instance.playsound(Shoot_Sound.clip, Shoot_Sound) ;
                    yield return new WaitForSeconds(1);
                }

                int counter = magazine.Count;

                if (magazine.Count < counter)
                {

                    for (int i = 0; i < magazine.Count; i++)
                    {
                        Destroy(magazine[i]);

                    }
                }

                yield return new WaitForSeconds(1);
            }
        }

       

       
        // Update is called once per frame
        void Update()
        {
        
        }

    
    }
}
