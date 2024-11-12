using System;
using System.Collections;
using UnityEngine;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        public GameManager _gameManger;
        public GameObject quad;
        public float maxHealth, currentHealth, currentGold;
        // Start is called before the first frame update
        void Start()
        {
            _gameManger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            quad = GameObject.FindGameObjectWithTag("quad");
            quad.SetActive(false);
            currentGold = 10;
            currentHealth = maxHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Attacker")
            {
                throw new NotImplementedException();
            }
        }

        public void TakeDamage(float damage)
        {
            
            quad.SetActive(true);
            StartCoroutine(ouchies());
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

        IEnumerator ouchies()
        {
            yield return new WaitForSeconds(1);
            quad.SetActive(false);
            yield return null;
        }
    }
}
