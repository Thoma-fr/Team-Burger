using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(waitodesactive());
    }
    IEnumerator waitodesactive()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
