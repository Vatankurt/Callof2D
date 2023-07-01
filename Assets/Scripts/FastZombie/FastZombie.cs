public class FastZombie : Enemy
{
    private const int SCORE = 3;
    private const int ZOMBIE_DAMAGE = 1;
    private const int STARTING_LIVES = 2;

    protected override void Awake()
    {
        base.Awake();
        SetLives(STARTING_LIVES);
        Damage = ZOMBIE_DAMAGE;
        Score = SCORE;
    }
}