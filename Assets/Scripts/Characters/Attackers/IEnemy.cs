namespace Characters.Attackers
{
   public interface IEnemy 
   {
   
      void Attack();
      void Move();
      void TakeDamage();
      void Die();
   }
}
