using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameplayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
