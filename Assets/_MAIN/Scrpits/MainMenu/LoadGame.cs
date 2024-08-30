using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    [SerializeField] string lastLevel;
    TextMeshProUGUI text;

    private void Awake()
    {
        lastLevel = PlayerPrefs.GetString("LastLevel");
    }

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        text.text = "Continue " + lastLevel;

        if (lastLevel == null || lastLevel == string.Empty || lastLevel == "Main Menu")
        {
            GetComponent<Button>().interactable = false;
            text.text = "Continue Unavailable";
        }
    }

    public void ContinueGame()
    {
        MainMenuButton button = GetComponent<MainMenuButton>();

        button.GoToScene(lastLevel);
    }
}
