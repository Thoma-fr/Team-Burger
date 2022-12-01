using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; set; }

	public Queue<string> sentences;

	public GameObject dialogueHolder, dialogueText, nameText;
    public bool animFinish = true;
	public int pos;
    void Start()
	{
		Instance = this;
		animFinish = true;
		sentences = new Queue<string>();
	}

	public void StartDialogue(string dial)
	{
		dialogueHolder.SetActive(true);
		Sequence intro = DOTween.Sequence();
		intro.Append(dialogueHolder.transform.DOMoveY(pos, 0.5f).SetEase(Ease.Linear));
		StartCoroutine(Delay(0.5f));

		PlayerController.Instance.playerMode = PlayerController.PLAYER_MODE.DIALOGUE_MODE;
		

		// affiche le nom
		nameText.GetComponent<TextMeshPro>().text = PlayerController.Instance.npc.NPCname;

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
			animFinish = false;
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
		dialogueText.GetComponent<TextMeshPro>().text = "";
		// toCharArray sépare chaque caractère pour les mettre dans une array
		foreach (char letter in sentence.ToCharArray())
		{
			// ajoute la lette au texte
			dialogueText.GetComponent<TextMeshPro>().text += letter;
			// attend quelque frame 
			yield return null;
		}
	}

	void EndDialogue()
	{
		Sequence outro = DOTween.Sequence();
		outro.Append(dialogueHolder.transform.DOMoveY(pos - 210, 0.5f).SetEase(Ease.Linear));
		outro.OnComplete(() => { dialogueHolder.SetActive(false);});
		StartCoroutine(Delay(0.5f));
		PlayerController.Instance.npc = null;
		PlayerController.Instance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;
	}

	IEnumerator Delay(float t)
    {
		yield return new WaitForSeconds(t);
		animFinish = true;
	}
}
