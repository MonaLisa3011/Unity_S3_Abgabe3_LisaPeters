using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;
    private void OnCollisionEnter2D(Collision2D other)
    {

        HitPoints hitpoints = other.gameObject.GetComponent<HitPoints>();
        if (hitpoints != null && other.gameObject.CompareTag("Player"))
        {
            hitpoints.TakeDamage(damage, other.transform.position - transform.position, knockbackForce);
        }
    }
}
