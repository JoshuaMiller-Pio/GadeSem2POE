using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Defenders
{
    public class DefenderSuper : MonoBehaviour
    {
        public GameManager _gameManager;
        public UIManager _uiManager;
        public GameObject target;
        public SphereCollider towerTrigger;
        public DefenderScriptable defenderScript;
        public List<GameObject> targets;
        public string towerName;
        public float atkSpd, damage, cost;
        public GameObject towerGun;
        public GameObject towerAim;
        public ButtonManager _buttonManager;
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

        public void SetUp()
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
            _buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        }
        public void OnMouseDown()
        {
            _gameManager.selectedTower = this.gameObject;
            _gameManager.sellCost = cost;
            _uiManager.ShowSelectedTower(this.gameObject);
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
