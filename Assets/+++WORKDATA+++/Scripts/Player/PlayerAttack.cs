using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRadius;
    [SerializeField] private float knockbackForce;

    [SerializeField] private GameObject attackVisualizer;
    [SerializeField] private Transform turnPivot;

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(turnPivot.position + turnPivot.up * attackRange, attackRadius);

            attackVisualizer.SetActive(true);
            StartCoroutine(DeactivateAttackVisualizer(0.2f));

            foreach (Collider2D hit in hits)
            {
                HitPoints hitpoints = hit.GetComponent<HitPoints>();
                if (hitpoints != null && hitpoints.gameObject != gameObject)
                {
                    Debug.Log("Dealing damage to: " + hit.name);
                    hitpoints.TakeDamage(damage, hit.transform.position - transform.position, knockbackForce);
                }


            }
        }
    }

    IEnumerator DeactivateAttackVisualizer(float delay)
    {
        yield return new WaitForSeconds(delay);

        attackVisualizer.SetActive(false);
    }
}
