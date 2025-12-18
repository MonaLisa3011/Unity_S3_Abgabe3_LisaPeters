using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class DialogTrigger : MonoBehaviour
{
   

    private bool playerInRange;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Awake()
    {
        playerInRange = false;
        
    }

    private void Update()
    {
        if (playerInRange && !DialogManager.GetInstance().dialogueIsPlaying)
        {

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("dialog startet");
                DialogManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
           
        }
    }

    public void StartDialog()
    {

        DialogManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "VogelMann")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "VogelMann")
        {
            playerInRange = false;
        }
    }

   


}
