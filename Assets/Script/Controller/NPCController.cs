using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    public string name;
    public List<string> sentences;

    public void Interact()
    {
        PlayerController.Instance.npc = this;
        DialogueManager.Instance.StartDialogue(this);
    }
    public void nextSentence()
    {
        DialogueManager.Instance.DisplayNextSentence();
    }
}
