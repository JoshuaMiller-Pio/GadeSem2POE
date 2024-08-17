using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Defenders
{
    public class DefenderSuper : MonoBehaviour
    {
        public GameObject target;
        public SphereCollider towerTrigger;
        public DefenderScriptable defenderScript;
        public List<GameObject> targets;
        public float damage, cost;
        public GameObject towerGun;
        public GameObject towerAim;

        public LinkedList<GameObject> pathWaypoints;
       // public AudioSource Shoot_Sound;
        Vector3 mousePos;
        Vector2 treeaim;
        Transform treeRotate;

        public List<GameObject> magazine;
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
