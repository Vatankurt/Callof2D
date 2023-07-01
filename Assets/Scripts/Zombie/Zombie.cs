public class Zombie : Enemy
{
    private const int SCORE = 1;
    private const int ZOMBIE_DAMAGE = 2;
    private const int STARTING_LIVES = 1;

    protected override void Awake()
    {
        base.Awake();
        SetLives(STARTING_LIVES);
        Damage = ZOMBIE_DAMAGE;
        Score = SCORE;
    }
}