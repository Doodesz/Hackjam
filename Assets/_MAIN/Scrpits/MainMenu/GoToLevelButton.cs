using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevelButton : MonoBehaviour
{
    [SerializeField] string sceneDestName;
    [SerializeField] MainMenuManager mainMenuManager;

    private void Start()
    {
        mainMenuManager = GameObject.Find("Main Menu Manager").GetComponent<MainMenuManager>();
    }

    public void OnNewGameClick()
    {
        GoToScene("Level 1");
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

    void GoToScene(string sceneName)
    {
        mainMenuManager.GoToLevel(sceneDestName);
    }
}
