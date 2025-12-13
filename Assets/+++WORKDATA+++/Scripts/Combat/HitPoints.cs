using UnityEngine;
using static UnityEditor.MaterialProperty;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine.Animations;
using UnityEngine.UI;



public class HitPoints : MonoBehaviour
{

    [SerializeField] private UIHitPoints uiHitpoints;
    [SerializeField] GameObject LostPanel;

    private Rigidbody2D rb;
    private EnemyMovement enemyController;

    [SerializeField] AudioClip hitSound;
    private AudioSource audioSource;

    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyMovement>();
        // spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {

        currentHealth = currentHealth - damage;

        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        }


        if (enemyController != null)
        {
            enemyController.BlockMovementFor(1f);
        }

        if (uiHitpoints != null)
        {
            uiHitpoints.UpdateHitpoints(currentHealth);
        }

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            LostPanel.SetActive(true);

        }


    }
}
