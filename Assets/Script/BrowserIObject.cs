using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserIObject : MonoBehaviour
{
    public Object obj;

    public void OnClick()
    {
        BrowserManager.SetObjectSelected = this.obj;
    }
}
