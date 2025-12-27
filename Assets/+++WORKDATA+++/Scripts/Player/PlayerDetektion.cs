using UnityEngine;

public class PlayerDetektion : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement.movementBlocked = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("VogelMann"))
        {
            enemyMovement.movementBlocked = false;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("VogelMann"))
        {
            enemyMovement.movementBlocked = true;
        }
    }
}
