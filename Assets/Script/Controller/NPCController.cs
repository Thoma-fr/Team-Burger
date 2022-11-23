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

public class NPCController : MonoBehaviour, Interactable
{
	public string NPCname;

	public string sentences;
	public string altSentences;

	public NPCState state;

	public List<Item> itemToGive = null;
	public List<GameObject> zoneToUnlock = null;


	public void Interact()
	{
		PlayerController.Instance.npc = this;
		

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
	public void nextSentence()
	{
		DialogueManager.Instance.DisplayNextSentence();
	}
}
