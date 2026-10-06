using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float Health = 100f;
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Enemy took " + damage + " damage");
        if (Health <= 0)
        {
            Debug.Log("Enemy was killed!");
            Destroy(gameObject);
        }
    }
}
