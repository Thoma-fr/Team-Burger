using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("decors"))
        {
            other.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("decors"))
        {
            other.gameObject.SetActive(true);
        }
    }
}
