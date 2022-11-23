using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class pantagram : MonoBehaviour
{
    private Volume Volume;
    private Animator anim;
    private void Start()
    {
        Volume = GetComponent<Volume>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("hey");
            DOTween.To(() => Volume.weight, x => Volume.weight = x, 1f, 1f);
            anim.enabled = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.To(() => Volume.weight, x => Volume.weight = x, 0f, 0.5f);
            anim.enabled = false;
        }
    }
}
