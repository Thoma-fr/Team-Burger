using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bush : MonoBehaviour
{
    private BoxCollider collider;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer otherSPR;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bestiole")
        {
            //animator.SetTrigger("Anim");
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DORotate(new Vector3(0, 0, 17), 0.2f));
            mySequence.Append(transform.DORotate(new Vector3(0, 0, -35), 0.2f));
            mySequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBounce));

            Debug.Log("enter");
            Debug.Log(other.gameObject.name);
            otherSPR = other.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSPR.sortingOrder + 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bestiole")
        {
            Debug.Log("exit");
            spriteRenderer.sortingOrder = otherSPR.sortingOrder - 1;
        }

    }
}
