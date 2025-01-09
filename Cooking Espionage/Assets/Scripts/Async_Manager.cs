using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Async_Manager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip menuMusic;





    private void Start()
    {
        SFXManager.instance.playSFXLoop(menuMusic, transform, 0.7f);
        loadingScreen.SetActive(false);
        mainMenu.SetActive(true);




    }

    public void LoadLevel(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    public void buttonClick()
    {
        SFXManager.instance.playSFX(buttonClickSound, transform, 1f);
    }
}
