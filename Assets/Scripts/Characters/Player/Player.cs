using UnityEngine;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        public GameManager _gameManger;
        
        public float maxHealth, currentHealth, currentGold;
        // Start is called before the first frame update
        void Start()
        {
            _gameManger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            currentGold = 10;
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            _gameManger.GameOver();
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
