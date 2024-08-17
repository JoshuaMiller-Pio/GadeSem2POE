using UnityEngine;

namespace Characters.Attackers
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "CreateEnemy")]
    public class EnemyScriptable : ScriptableObject
    {
        public enum EnemyType
        {
            melee,
            ranged,
            tanks
        }
    
        public EnemyType enemyType;
        public string[] enemyNames = new string[3] { "Soldier", "Ranger", "Tank" };
        public string enemyName;
        public float maxHealth, damage, moveSpeed, attackSpeed;

    }
}
