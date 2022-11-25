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

    
}
