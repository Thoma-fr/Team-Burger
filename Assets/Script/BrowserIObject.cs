using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserIObject : MonoBehaviour
{
    public Object obj;

    public void OnActive()
    {
        BrowserManager.instance.DisplayDescription(obj);
    }
}
