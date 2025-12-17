using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

   public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    
}
