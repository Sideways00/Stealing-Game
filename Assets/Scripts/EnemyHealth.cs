using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float Health = 100f;
    [SerializeField] private float flashTime = 0.1f;

    private Renderer[] enemyRenderers;
    private Color[] originalColors;

    private void Start()
    {
        enemyRenderers = GetComponentsInChildren<Renderer>();

        originalColors = new Color[enemyRenderers.Length];

        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            originalColors[i] = enemyRenderers[i].material.color;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        Debug.Log("Enemy took " + damage + " damage!");

        // Flash red
        StartCoroutine(DamageFlash());

        if (Health <= 0)
        {
            Debug.Log("Enemy was killed!");
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageFlash()
    {
        // Turn enemy red
        foreach (Renderer renderer in enemyRenderers)
        {
            renderer.material.color = Color.red;
        }

        // Wait briefly
        yield return new WaitForSeconds(flashTime);

        // Return to original color
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material.color = originalColors[i];
        }
    }
}
