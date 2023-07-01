using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private int damage;

    private void Awake()
    {
        DestroyBullet(5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Character character = other.gameObject.GetComponentInChildren<Character>();
        if (character != null && GameManager.Instance != null && GameManager.Instance.IsMultiplayerGame)
        {
            character.TakeDamage(damage);
            DestroyBullet(0f);
        }

        Enemy enemy = other.gameObject.GetComponentInChildren<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            DestroyBullet(0f);
        }

        if (other.gameObject.CompareTag("environment"))
        {
            DestroyBullet(0f);
        }
    }

    public void Fire(int gunDamage, Transform direction, float force)
    {
        damage = gunDamage;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
            rigidbody.AddForce(direction.up * force, ForceMode2D.Impulse);
    }

    private void DestroyBullet(float time)
    {
        Destroy(gameObject, time);
    }
}