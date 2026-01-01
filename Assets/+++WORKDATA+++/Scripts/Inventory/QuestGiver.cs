using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private SO_QuestData questData;
    [SerializeField] private GameObject highlight;

    private PlayerInteraction playerInteraction;

    [SerializeField] private TextMeshProUGUI textQuestState;
    private SO_QuestData.QuestState questStateLastFrame;
    [SerializeField] private QuestManager questManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        highlight.SetActive(false);
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();

        questStateLastFrame = questData.currentState;
        UpdateTextQuestState();
    }

    private void Update()
    {
        if (questData.currentState != questStateLastFrame)
        {
            UpdateTextQuestState();
            questStateLastFrame = questData.currentState;
        }

    }

    void UpdateTextQuestState()
    {
        if (questData.currentState == SO_QuestData.QuestState.open)
        {
            textQuestState.text = "?";
            textQuestState.transform.DOScale(1.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        }
        else if (questData.currentState == SO_QuestData.QuestState.active)
        {
            textQuestState.text = "...";
            textQuestState.transform.DOKill();
        }
        else if (questData.currentState == SO_QuestData.QuestState.completed)
        {
            textQuestState.text = "!";
            textQuestState.transform.DOScale(1.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        }
        else if (questData.currentState == SO_QuestData.QuestState.closed)
        {
            textQuestState.text = " ";
            textQuestState.transform.DOKill();
            // questManager.ShowGewonnenPanel();
            
        }

       // questManager.FinishQuest();
    }

    void OnPlayerinteraction()
    {
        if (questData.currentState == SO_QuestData.QuestState.open)
        {
            QuestManager.Instance.AssignQuest(questData);
        }
        else if (questData.currentState == SO_QuestData.QuestState.completed)
        {
            QuestManager.Instance.CloseQuest(questData);
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player is near" + gameObject.name);
        if (other.CompareTag("VogelMann"))
        {

            highlight.SetActive(true);

            playerInteraction.OnInteract.AddListener(OnPlayerinteraction);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player let area of " + gameObject.name);
        if (other.CompareTag("VogelMann"))
        {

            highlight.SetActive(false);

            playerInteraction.OnInteract.RemoveListener(OnPlayerinteraction);
        }
    }

    
}

