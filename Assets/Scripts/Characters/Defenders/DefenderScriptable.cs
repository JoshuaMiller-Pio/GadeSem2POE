using UnityEngine;

namespace Characters.Defenders
{
    [CreateAssetMenu(fileName = "New Defender", menuName = "CreateDefender")]
    public class DefenderScriptable : ScriptableObject
    {
        public enum DefenderType
        {
            melee,
            ranged,
            tanks
        }
    
        public DefenderType defenderType;
        public string[] defenderNames = new string[3] { "Soldier", "Ranger", "Tank" };
        public string defenderName;
        public float maxHealth, damage, moveSpeed;
    }
}
