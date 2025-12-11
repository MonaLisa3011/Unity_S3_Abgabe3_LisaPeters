using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    private Rigidbody2D rb;
   // [SerializeField] private Transform turnPivot;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;

    private bool movementBlocked;

   // private Animator animator;




    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        //animator = GetComponent<Animator>();



    }

    void Update()
    {
       // animator.SetBool("Iswalking", false);

        //animator.SetBool("Iswalking", rb.linearVelocity.magnitude > 0);

        if (movementBlocked)
        {
            return;
        }
        //rb.linearVelocity = moveInput * (moveSpeed + InventoryManager.Instance.GetBonusSpeed());

        if (rb.linearVelocity.x > 0)
        {
           // animator.SetFloat("FacingRight", 1);
        }
        else if (rb.linearVelocity.x < 0)
        {
           // animator.SetFloat("FacingRight", -1);
        }


        if (moveInput.x != 0 || moveInput.y != 0)
        {
           // turnPivot.rotation = Quaternion.LookRotation(Vector3.forward, rb.linearVelocity);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

       // animator.SetBool("Iswalking", true);
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





}

