namespace Characters.Defenders
{
    public interface IDefender
    {
        void Move();
        void Attack();
        void TakeDamage();
        void Death();
    }
}
