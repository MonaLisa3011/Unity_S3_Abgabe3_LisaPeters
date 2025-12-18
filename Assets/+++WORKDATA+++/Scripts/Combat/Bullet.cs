using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerAttack Weapon;
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {

            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }


            Destroy(gameObject);
        }

    }

    

    
}
