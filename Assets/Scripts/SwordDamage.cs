using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword collided with: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Enemy detected but no EnemyHealth component found on: " + other.name);
            }
        }
    }
}
