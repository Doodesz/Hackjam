using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrativeScene : MonoBehaviour
{
    [SerializeField] string sceneTarget;
    DialogueTrigger dialogue;
    DialogueManager dialogueManager;
    bool isTransitioning;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<DialogueTrigger>();
        dialogueManager = GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(GetComponent<DialogueTrigger>().dialogue);
    }

    private void Update()
    {
        if (!dialogueManager.isDialogueActive && !isTransitioning)
        {
            isTransitioning = true;
            ScreenTransition.Instance.TransitionIn();
        }

        if (ScreenTransition.Instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank")
            && isTransitioning)
        {
            SceneManager.LoadScene(sceneTarget);
        }
    }
}
