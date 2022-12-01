using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum NPCState
{
	NONE,
	GIVER,
	ZONER
};

[System.Serializable]
public class NPCController : MonoBehaviour, Interactable
{
	public string NPCname;

	public string sentences;
	public string altSentences;

	public NPCState state;

	public List<Item> itemToGive;
	public List<GameObject> zoneToUnlock;

	public List<AudioClip> sons;

    public void Interact()
	{
        if (DialogueManager.Instance.animFinish)
        {
			DialogueManager.Instance.animFinish = false;
			PlayerController.Instance.npc = this;
			GetComponent<AudioSource>().PlayOneShot(sons[Random.Range(0, sons.Count)]);

			switch (state)
			{
				case NPCState.GIVER:
					DialogueManager.Instance.StartDialogue(altSentences);
					foreach(Item it in itemToGive)
						GameManager.instance.GetPlayerData.AddItemInInventory(it, it.currentStack);
					itemToGive = null;
					state = NPCState.NONE;
					break;
			
				case NPCState.ZONER:
					DialogueManager.Instance.StartDialogue(altSentences);
					foreach(GameObject tmCol in zoneToUnlock)
						tmCol.SetActive(false);
					zoneToUnlock = null;
					state = NPCState.NONE;
					break;

				case NPCState.NONE:
					DialogueManager.Instance.StartDialogue(sentences);
					break;
			}
        }
	}
	public void nextSentence()
	{
		if (DialogueManager.Instance.animFinish)
		{
			DialogueManager.Instance.DisplayNextSentence();
		}
	}
}
