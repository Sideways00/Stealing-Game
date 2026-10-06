using UnityEngine;
using UnityEngine.InputSystem;

public class ShurikenThrow : MonoBehaviour
{
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private Transform throwPoint;

    [SerializeField] private float throwSpeed = 20f;
    [SerializeField] private float spread = 0.1f;
    [SerializeField] private float cooldown = 5f;

    private float cooldownTimer = 0f;

    private void Update()
    {
        // Count down the cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Right mouse button
        if (Mouse.current != null &&
            Mouse.current.rightButton.wasPressedThisFrame &&
            cooldownTimer <= 0f)
        {
            ThrowShurikens();

            // Start the 5-second cooldown
            cooldownTimer = cooldown;
        }
    }

    private void ThrowShurikens()
    {
        // Throw 5 shurikens
        for (int i = 0; i < 5; i++)
        {
            // Direction the player is looking
            Vector3 direction = throwPoint.forward;

            // Add a small random spread
            direction += Random.insideUnitSphere * spread;
            direction.Normalize();

            // Correct the shuriken's orientation
            Quaternion rotation =
                Quaternion.LookRotation(direction) *
                Quaternion.Euler(-90f, 0f, 0f);

            // Create the shuriken
            GameObject shuriken = Instantiate(
                shurikenPrefab,
                throwPoint.position,
                rotation
            );

            // Give the shuriken velocity
            Rigidbody rb = shuriken.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = direction * throwSpeed;
            }
        }
    }
}
