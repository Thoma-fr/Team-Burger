using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Rumble : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, 5), 0.1f));
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, -5), 0.1f));
            mySequence.Append(transform.parent.DORotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.OutBounce));
        }
    }

}
