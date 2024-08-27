using UnityEngine;

namespace Characters.Defenders
{
    [CreateAssetMenu(fileName = "New Defender", menuName = "CreateDefender")]
    public class DefenderScriptable : ScriptableObject
    {
        public enum DefenderType
        {
            Basic,
            Debuff,
            Aoe
        }
    
        public DefenderType defenderType;
        public string[] defenderNames = new string[3] { "Turret", "Glue Shooter", "Cannon" };
        public string defenderName;

        public float damage, atkSpeed, cost;


    }
}
