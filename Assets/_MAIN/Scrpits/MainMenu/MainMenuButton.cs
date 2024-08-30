using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public GameObject mainMenuButton;
    public GameObject levelSelectButton;
    
    [SerializeField] string sceneDestName;
    [SerializeField] MainMenuManager mainMenuManager;

    private void Start()
    {
        mainMenuManager = GameObject.Find("Main Menu Manager").GetComponent<MainMenuManager>();
    }

    public void OnNewGameClick()
    {
        GoToScene("Level 1.1");
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

    public void OnReturnButtonClick()
    {
        mainMenuButton.SetActive(true);
        levelSelectButton.SetActive(false);
        //settingsButton.SetActive(false);
        //creditsButton.SetActive(false)
    }

    void GoToScene(string sceneName)
    {
        mainMenuManager.GoToLevel(sceneName);
    }
}
