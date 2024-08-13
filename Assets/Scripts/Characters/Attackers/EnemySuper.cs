using UnityEngine;

namespace Characters.Attackers
{
    public class EnemySuper : MonoBehaviour
    {
        public GameObject playerTower;
        public ScriptableObject enemyScript;
        public GameObject target;
        public float maxHealth, damage, moveSpeed;
        // Start is called before the first frame update
        void Start()
        {
            playerTower = GameObject.FindGameObjectWithTag("PlayerTower");
            target = playerTower;
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
