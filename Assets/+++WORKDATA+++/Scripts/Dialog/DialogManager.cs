using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Ink.Runtime;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    private Story currentStory;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogManager instance;

    [SerializeField] private SpriteRenderer PictureSpriteRenderer;


    [SerializeField] private Sprite VogelMannPicture;
    [SerializeField] private Sprite VogelFrauPicture;
    [SerializeField] private Sprite FrauPicture;

    [SerializeField] private TextMeshProUGUI nameText;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the Scene");
        }
        instance = this;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (!dialogueIsPlaying)
            {
                return;
            }

           //if (PlayerInteraction.GetInstance())
            {
                ContinueStory();
                CharacterSprite();
            }
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        Debug.Log(currentStory.canContinue);
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }

    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given:" + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();

    }

    private void CharacterSprite()
    {
        foreach (string currentTag in currentStory.currentTags)
        {
            if (currentTag == "VogelMann")
            {
                SetPictureSprite(VogelMannPicture);

                nameText.text = "VogelMann";
            }
            else if (currentTag == "VogelFrau")
            {
                SetPictureSprite(VogelFrauPicture);
                nameText.text = "VogelFrau";
            }
            else if (currentTag == "Frau")
            {
                SetPictureSprite(FrauPicture);
                nameText.text = "Frau";
            }
        }
    }

    private void SetPictureSprite(Sprite newSprite)
    {
        if (PictureSpriteRenderer != null)
        {
            PictureSpriteRenderer.sprite = newSprite;
        }
    }
}
