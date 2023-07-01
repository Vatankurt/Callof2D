using UnityEngine;

public class ZombieAwareness : MonoBehaviour
{
    public bool IsAwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float distanceAwareness;
    
    private Transform playerTransform;

    private void Awake()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
            playerTransform = playerMovement.transform;
    }

    private void Update()
    {
        if (UIManager.IsPaused)
            return;

        CheckPlayerAwareness();
    }

    private void CheckPlayerAwareness()
    {
        if (playerTransform == null)
            return;

        Vector2 enemyToPlayerVector = playerTransform.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= distanceAwareness)
        {
            IsAwareOfPlayer = true;
        }
        else
        {
            IsAwareOfPlayer = false;
        }
    }
}
