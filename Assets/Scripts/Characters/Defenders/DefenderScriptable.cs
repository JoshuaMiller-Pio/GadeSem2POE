using UnityEngine;

namespace Characters.Defenders
{
    [CreateAssetMenu(fileName = "New Defender", menuName = "CreateDefender")]
    public class DefenderScriptable : ScriptableObject
    {
        public int Level;
        public enum DefenderType
        {
            Basic,
            MidBasic,
            LargeBasic,
            Debuff,
            MidDebuff,
            LargeDebuff,
            Aoe,
            MidAOE,
            LargeAOE
        }
    
        public DefenderType defenderType;
        public string[] defenderNames = new string[3] { "Turret", "Buff Tower", "Cannon",  };
        public string defenderName;

        public float damage, atkSpeed, cost;


    }
}
