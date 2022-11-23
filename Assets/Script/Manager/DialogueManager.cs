using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	public Queue<string> sentences;

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public GameObject dialogueHolder;

	void Start()
	{
		Instance = this;
		sentences = new Queue<string>();
	}

	public void StartDialogue(string dial)
	{
		dialogueHolder.SetActive(true);
		PlayerController.Instance.playerMode = PlayerController.PLAYER_MODE.DIALOGUE_MODE;
		

		// affiche le nom
		nameText.text = PlayerController.Instance.npc.NPCname;

		// efface les anciennes phrases
		sentences.Clear();

		// récupère les phrase présent dans l'array pour les mettre dans la queux

		foreach (string sentence in dial.Split("$*$"))
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		// si il n'y a plus de phrase à afficher
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		// passe à la phrase suivante
		string sentence = sentences.Dequeue();

		// si le jr appuis sur continuer quand la 1er animation ce joue, cette dernière sera stopé
		StopAllCoroutines();

		// affiche les phrase
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		// toCharArray sépare chaque caractère pour les mettre dans une array
		foreach (char letter in sentence.ToCharArray())
		{
			// ajoute la lette au texte
			dialogueText.text += letter;
			// attend quelque frame 
			yield return null;
		}
	}

	void EndDialogue()
	{
		dialogueHolder.SetActive(false);
		PlayerController.Instance.npc = null;
		PlayerController.Instance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;
	}
}
