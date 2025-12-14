using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    [SerializeField] private Transform turnPivot;
   
    private Vector2 moveInput;

    private bool movementBlocked;

    [SerializeField] GameObject LostPanel;
    [SerializeField] GameObject WinPanel;

    void Start()
    {
      
            rb = GetComponent<Rigidbody2D>();
        LostPanel.SetActive(false);
        WinPanel.SetActive(false);


    }

    void Update()
    {
      
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

       
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
        LostPanel.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (DialogManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
    }

    
        


    
}


