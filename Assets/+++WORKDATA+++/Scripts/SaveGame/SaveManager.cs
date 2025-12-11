using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public GlobalState SaveState;
    private string SavePath => Application.persistentDataPath + "/SaveData.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadGame();
    }



    public void SaveGame()
    {
        //umwandeln von savestate in einen text
        string json = JsonUtility.ToJson(SaveState);

        // speichern des texts auf der Festplatte
        System.IO.File.WriteAllText(SavePath, json);

        Debug.Log("Save complited");
    }

    public void LoadGame()
    {
        if (System.IO.File.Exists(SavePath))
        {
            // datei lesen von festplatte
            string json = System.IO.File.ReadAllText(SavePath);

            //text in globalstate objekt umwandeln
            SaveState = JsonUtility.FromJson<GlobalState>(json);

            Debug.Log("Save complited");
        }
        else
        {
            SaveState = new GlobalState();
            SaveGame();
        }

    }

    public void DeleteSaveFile()
    {
        System.IO.File.Delete(SavePath);
    }
}
