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


   // public Slider healthSlider;
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyMovement>();


        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {

        currentHealth = currentHealth - damage;

        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            //healthSlider.value = (float)currentHealth / (float)maxHealth;

        }


        if (enemyController != null)
        {
            enemyController.BlockMovementFor(1f);
        }

        if (uiHitpoints != null)
        {
            uiHitpoints.UpdateHitpoints(currentHealth);
        }



        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            LostPanel.SetActive(true);

        }
    }
}
