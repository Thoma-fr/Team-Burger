using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.VFX;

public class pantagram : MonoBehaviour
{
    private Volume Volume;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private GameObject deerprefabs;
    [SerializeField] private float spawnRate;
    [SerializeField] private AudioClip AudioClip;

    [SerializeField] private List<GameObject> vfx;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Volume = GetComponent<Volume>();
        anim = GetComponent<Animator>();

        StartCoroutine(spawn());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("hey");
            DOTween.To(() => Volume.weight, x => Volume.weight = x, 1f, 1f);
            anim.SetTrigger("On");

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.To(() => Volume.weight, x => Volume.weight = x, 0f, 0.5f);
            
        }
    }

    IEnumerator spawn()
    {

        yield return new WaitForSeconds(spawnRate);
        foreach (GameObject obj in vfx)
            obj.GetComponent<VisualEffect>().SetBool("Active",true);
        yield return new WaitForSeconds(spawnRate);
        audioSource.PlayOneShot(AudioClip);
        yield return new WaitForSeconds(1f);
        Instantiate(deerprefabs, transform.position,Quaternion.Euler(0,0,0));
        foreach (GameObject obj in vfx)
            obj.GetComponent<VisualEffect>().SetBool("Active", false);
        StartCoroutine(spawn());
    }
}
