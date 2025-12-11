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
    [SerializeField] AudioClip hitSound;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private CinemachineImpulseSource cinemachineImpulseSource;

    public int maxHealth;
    public int currentHealth;
    //public Slider healthSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {

        currentHealth = currentHealth - damage;  //VORHER hitpoints--; 

        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            //healthSlider.value = (float)currentHealth / (float)maxHealth;
        }


        if (playerMovement != null)
        {
            playerMovement.BlockMovementFor(1f);
        }

        if (uiHitpoints != null)
        {
            uiHitpoints.UpdateHitpoints(currentHealth);
        }

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.DOKill();
            spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo);
        }

        if (gameObject.CompareTag("Player"))
        {
            
            cinemachineImpulseSource.GenerateImpulse();
        }

        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                spriteRenderer.DOKill();
                Destroy(gameObject);
            }
        }
    }
}
