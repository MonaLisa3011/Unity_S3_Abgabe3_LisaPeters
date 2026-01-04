using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { private set; get; }
    
    public GameObject GewonnenPanel;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private List<SO_QuestData> activeQuests = new List<SO_QuestData>();

    [SerializeField] UIQuestManager uiQuestManager;

    [Header("Dfor load and save")]
    [SerializeField] private SO_QuestData[] allQuestData;
    private Dictionary<string, SO_QuestData> allQuests = new Dictionary<string, SO_QuestData>();

    private void Start()
    {
        foreach (SO_QuestData data in allQuestData)
        {
            allQuests.Add(data.questID, data);
        }

        foreach (SaveQuestState saveQuestState in SaveManager.Instance.SaveState.saveQuestState)
        {
            SO_QuestData newQuestData = allQuests[saveQuestState.questID];
            newQuestData.currentState = (SO_QuestData.QuestState)saveQuestState.currentState;
            newQuestData.currentAmount = saveQuestState.currentProgress;

            if (saveQuestState.currentState == (int)SO_QuestData.QuestState.active ||
                saveQuestState.currentState == (int)SO_QuestData.QuestState.completed)
            {
                activeQuests.Add(newQuestData);
            }
        }
        uiQuestManager.UpdateAllQuestEnteries(activeQuests);
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            foreach (SO_QuestData data in allQuestData)
            {
                data.currentState = SO_QuestData.QuestState.open;
            }

            SaveManager.Instance.DeleteSaveFile();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    public void AssignQuest(SO_QuestData newQuestData)
    {
        if (newQuestData.currentState == SO_QuestData.QuestState.open)
        {
            newQuestData.currentState = SO_QuestData.QuestState.active;
            newQuestData.currentAmount = 0;
            activeQuests.Add(newQuestData);

            OnItemCollected();
        }
    }

    public void OnItemCollected()
    {
        foreach (SO_QuestData questData in activeQuests)
        {
            questData.currentAmount = InventoryManager.Instance.ItemCountInInventory(questData.requiredItem);
            if (questData.isCompleted)
            {
                CompleteQuest(questData);
            }
            else
            {
                // quest UI
            }
        }
        uiQuestManager.UpdateAllQuestEnteries(activeQuests);
        SaveActiveQuests();
    }


    void CompleteQuest(SO_QuestData completedQuest)
    {
        completedQuest.currentState = SO_QuestData.QuestState.completed;
        uiQuestManager.UpdateAllQuestEnteries(activeQuests);

    }

    public void CloseQuest(SO_QuestData finishedQuest)
    {
        finishedQuest.currentState = SO_QuestData.QuestState.closed;

        
        for (int i = 0; i < finishedQuest.requiredAmount; i++)
        {
            InventoryManager.Instance.RemoveItem(finishedQuest.requiredItem);

        }

        activeQuests.Remove(finishedQuest);

        uiQuestManager.UpdateAllQuestEnteries(activeQuests);
        SaveActiveQuests();

        // Das muss hier stehen, damit “CheckAllQuest” auch hier kontrolliert und ausgeführt wird.
        CheckAllQuests();
    }

    void SaveActiveQuests()
    {
        SaveManager.Instance.SaveState.saveQuestState = new SaveQuestState[activeQuests.Count];
        List<SaveQuestState> temporaryQuestList = new List<SaveQuestState>();

        for (int i = 0; i < allQuestData.Length; i++)
        {
            if (allQuestData[i].currentState != SO_QuestData.QuestState.open)
            {

                SaveQuestState newSaveQuestState = new SaveQuestState();
                newSaveQuestState.questID = allQuestData[i].questID;
                newSaveQuestState.currentState = (int)allQuestData[i].currentState;
                newSaveQuestState.currentProgress = allQuestData[i].currentAmount;

                
                temporaryQuestList.Add(newSaveQuestState);
            }

        }

        SaveManager.Instance.SaveState.saveQuestState = temporaryQuestList.ToArray();
        SaveManager.Instance.SaveGame();
    }


    

    void CheckAllQuests()
    {
        // man nutzt das um zu schauen wie der zustand unserer Quest´s sind
        // finished bedeutet das alles fertig ist und den bool nehmen wir um true or false zu benutzen
        bool finished = true;
        // wir nutzen eine foreach schleife um alle unsere Quest´s zu durchlaufen
        // wie eine liste 
        foreach (SO_QuestData data in allQuestData)
        {
            // hier sagen wir das er kontrollieren soll wann die Quest´s abgeschlossen also "closed" sind
            // wir nutzen das if um verschiedene Möglichkeiten zu geben 
            if (data.currentState != SO_QuestData.QuestState.closed)
            {
                // wenn noch nicht alle Quest´s fertig sind soll er...
                finished = false;
                //... es nicht zeigen und nicht ausführen. (Aber auch nicht löschen weswegen dort nur "break" steht.)
                break;
            }
        }
        // wenn alle fertig und aubgeschlossen sind soll...
        if (finished)
        {
            //... das gewonnen Panel gezeigt werden
            ShowGewonnenPanel();
        }
    }

    public void ShowGewonnenPanel()
    {
        GewonnenPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}

