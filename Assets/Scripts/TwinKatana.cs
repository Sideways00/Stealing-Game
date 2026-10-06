using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class KatanaAttack : MonoBehaviour
{
    [Header("Katanas")]
    [SerializeField] private Transform rightKatana;
    [SerializeField] private Transform leftKatana;

    [Header("Attack Settings")]
    [SerializeField] private float swingAmount = 90f;
    [SerializeField] private float swingSpeed = 0.15f;

    private bool rightAttack = true;
    private bool attacking = false;

    private Quaternion rightStartingRotation;
    private Quaternion leftStartingRotation;

    private void Start()
    {
        rightStartingRotation = rightKatana.localRotation;
        leftStartingRotation = leftKatana.localRotation;
    }

    private void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            !attacking)
        {
            if (rightAttack)
            {
                StartCoroutine(SwingRightKatana());
            }
            else
            {
                StartCoroutine(SwingLeftKatana());
            }

            // Switch which hand attacks next
            rightAttack = !rightAttack;
        }
    }

    private IEnumerator SwingRightKatana()
    {
        attacking = true;

        Quaternion attackRotation =
            rightStartingRotation *
            Quaternion.Euler(-swingAmount, 0, 0);

        float timer = 0;

        while (timer < swingSpeed)
        {
            timer += Time.deltaTime;

            rightKatana.localRotation =
                Quaternion.Slerp(
                    rightStartingRotation,
                    attackRotation,
                    timer / swingSpeed
                );

            yield return null;
        }

        timer = 0;

        while (timer < swingSpeed)
        {
            timer += Time.deltaTime;

            rightKatana.localRotation =
                Quaternion.Slerp(
                    attackRotation,
                    rightStartingRotation,
                    timer / swingSpeed
                );

            yield return null;
        }

        rightKatana.localRotation = rightStartingRotation;

        attacking = false;
    }

    private IEnumerator SwingLeftKatana()
    {
        attacking = true;

        Quaternion attackRotation =
            leftStartingRotation *
            Quaternion.Euler(swingAmount, 0, 0);

        float timer = 0;

        while (timer < swingSpeed)
        {
            timer += Time.deltaTime;

            leftKatana.localRotation =
                Quaternion.Slerp(
                    leftStartingRotation,
                    attackRotation,
                    timer / swingSpeed
                );

            yield return null;
        }

        timer = 0;

        while (timer < swingSpeed)
        {
            timer += Time.deltaTime;

            leftKatana.localRotation =
                Quaternion.Slerp(
                    attackRotation,
                    leftStartingRotation,
                    timer / swingSpeed
                );

            yield return null;
        }

        leftKatana.localRotation = leftStartingRotation;

        attacking = false;
    }
}
