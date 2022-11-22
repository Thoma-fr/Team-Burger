using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class Volumehit : MonoBehaviour
{
    public Volume Volume;

    private void Start()
    {
        Volume.weight = 0;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.To(() => Volume.weight, x => Volume.weight = x, 1f, 1f));
        mySequence.Append(DOTween.To(() => Volume.weight, x => Volume.weight = x, 0f, 1f));


    }

}
