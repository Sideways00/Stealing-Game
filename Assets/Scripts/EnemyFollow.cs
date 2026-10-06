using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Transform player;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player was not found! Make sure your player is tagged Player.");
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;

        // Don't make the enemy move up/down toward the player
        direction.y = 0;

        if (direction.magnitude > 1f)
        {
            direction.Normalize();

            characterController.Move(direction * speed * Time.deltaTime);

            // Make the enemy face the player
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
