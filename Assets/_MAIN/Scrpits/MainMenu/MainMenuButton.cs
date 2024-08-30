using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public GameObject mainMenuButton;
    public GameObject levelSelectButton;
    public GameObject creditsScreen;
    
    [SerializeField] string sceneDestName;
    [SerializeField] MainMenuManager mainMenuManager;

    private void Start()
    {
        mainMenuManager = GameObject.Find("Main Menu Manager").GetComponent<MainMenuManager>();
    }

    public void OnNewGameClick()
    {
        GoToScene("Intro");
    }

    public void OnSelectSceneClick()
    {
        GoToScene(sceneDestName);
    }

    public void OnContinueGameClick()
    {
        // get player prefs
        // load it
        // gotoscene()
    }

    public void OnSelectLevelClick()
    {
        mainMenuButton.SetActive(false);
        levelSelectButton.SetActive(true);
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }

    public void OnCreditsScreenClick()
    {
        mainMenuButton.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void OnReturnButtonClick()
    {
        mainMenuButton.SetActive(true);
        levelSelectButton.SetActive(false);
        creditsScreen.SetActive(false);
        //settingsButton.SetActive(false);
    }

    public void GoToScene(string sceneName)
    {
        mainMenuManager.GoToLevel(sceneName);
    }
}
