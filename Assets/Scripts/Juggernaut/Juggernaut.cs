public class Juggernaut : Enemy
{
    private const int SCORE = 5;
    private const int ZOMBIE_DAMAGE = 3;
    private const int STARTING_LIVES = 3;

    protected override void Awake()
    {
        base.Awake();
        SetLives(STARTING_LIVES);
        Damage = ZOMBIE_DAMAGE;
        Score = SCORE;
    }
}
