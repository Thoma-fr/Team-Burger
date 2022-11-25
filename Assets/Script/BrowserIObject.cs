using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrowserIObject : MonoBehaviour
{
    public Object obj;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI txt;

    public void OnActive()
    {
        BrowserManager.instance.DisplayDescription(obj);

        image.sprite = obj.sprite;
        txt.text = obj.name;
    }
}
