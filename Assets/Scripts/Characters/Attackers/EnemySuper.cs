using UnityEngine;

namespace Characters.Attackers
{
    public class EnemySuper : MonoBehaviour
    {
        
        public GameManager _gameManager;
        public GameObject playerTower;
        public Player.Player playerBrain;
        public EnemyScriptable enemyScript;
        public GameObject target;
        public float currentHealth, damage, moveSpeed, attackSpeed, value;
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
