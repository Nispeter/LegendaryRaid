public interface IHealth {
    float health {get; set;}
    float remainingHealth {get; set;}
    void TakeDamage(float damage);
    void Die();
}