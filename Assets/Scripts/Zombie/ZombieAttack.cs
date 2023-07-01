using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private const float ATTACK_RATE = 1f;

    private float attackTime;
    private float nextAttackTime;
    private ZombieMovement zombieMovement;
    private bool canAttack;
    private Enemy enemy;
    private PlaySound playSound;

    private void Awake()
    {
        zombieMovement = GetComponentInChildren<ZombieMovement>(true);
        enemy= GetComponentInChildren<Enemy>(true);
        playSound = GetComponentInChildren<PlaySound>(true);
    }

    private void Update()
    {
        if (UIManager.IsPaused)
            return;

        attackTime += Time.deltaTime;
        if (attackTime > nextAttackTime)
        {
            canAttack = true;
        }
    }

    private void ResetAttackingTimer()
    {
        nextAttackTime = attackTime + ATTACK_RATE;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (canAttack)
        {
            DoDamage(other);
            canAttack = false;
            ResetAttackingTimer();
        }
    }

    private void DoDamage(Collision2D other)
    {
        Player player = other.gameObject.GetComponentInChildren<Player>();
        if (player != null)
        {
            player.TakeDamage(enemy.Damage);

            if (zombieMovement != null)
                zombieMovement.SetTarget(null);
        }
    }
}