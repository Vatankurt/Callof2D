using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float zombieSpeed = 3f;
    [SerializeField]
    private float rotateSpeed = 1f;
    [SerializeField]
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null)
            return;

        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, quaternion, rotateSpeed);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.up * zombieSpeed;
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}