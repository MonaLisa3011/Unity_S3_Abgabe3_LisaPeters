using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    public float health = 3;
    public int maxHealth;


    public bool movementBlocked;

    [SerializeField] GameObject LostPanel;
    [SerializeField] Button ReturnButton;

    [SerializeField] GameObject StartPanel;
    [SerializeField] Button StartButton;

    public Slider healthSlider;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("VogelMann").transform;

        StartButton.onClick.AddListener(StartGame);
        LostPanel.SetActive(false);
        ReturnButton.onClick.AddListener(RestartGame);

        health = maxHealth;
    }

    void StartGame()
    {

        StartPanel.SetActive(false);


    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowVerlorenPanel()
    {
        LostPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("Schaden");
        health -= damage;
        if (health <= 0)
        {

            Destroy(gameObject);
        }
        else
        {
            healthSlider.value = (float)health / (float)maxHealth;
        }
    }

    public void BlockMovementFor(float duration)
    {
        movementBlocked = true;

        StartCoroutine(ActivateMovementAfter(duration));
    }

    IEnumerator ActivateMovementAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        movementBlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {

            ShowVerlorenPanel();
        }
    }

   
}

