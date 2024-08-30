using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public bool hasBeenTriggered;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        hasBeenTriggered = true;
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasBeenTriggered && !PauseManager.Instance.gamePaused)
        {
            TriggerDialogue();
        }
    }
}