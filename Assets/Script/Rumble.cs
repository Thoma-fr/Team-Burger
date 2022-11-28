using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Rumble : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] private AudioClip shake;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.PlayOneShot(shake);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, 5), 0.1f));
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, -5), 0.1f));
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.OutBounce));
        }
    }

}
