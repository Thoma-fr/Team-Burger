using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BrowserManager : MonoBehaviour
{
    public static BrowserManager instance { get; private set; }
    
    [Header("Browser")]
    [SerializeField] private GameObject UIBrowserPrefab;
    [SerializeField] private TextMeshProUGUI GUIText;
    [SerializeField] private int numberShow = 4;
    [SerializeField] private int stepPosition = 10;
    int pageID = 0;
    int lastPageID;
    int maxPage;
    float maxHeight;

    [Header("Description Browser")]
    [SerializeField] private GameObject DescriptionRoot;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private Object selectedObject;
    List<Object> browserList;
    List<GameObject> UIBox = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void ShowBrowser<Tsource>(List<Tsource> source) where Tsource : Object
    {
        Debug.Log("Bite");
        browserList = source.Cast<Object>().ToList();
        maxPage = (int)(source.Count / numberShow);
        maxHeight = (stepPosition * (numberShow / 2.0f)) - stepPosition / 2;
        pageID = 0;
        lastPageID = -1;
        DisplayPage();
    }

    public void CloseBrowser()
    {
        foreach (GameObject obj in UIBox)
            Destroy(obj);

        UIBox.Clear();
    }

    public void NextPage()
    {
        pageID++;
        DisplayPage();
    }

    public void PreviousPage()
    {
        pageID--;
        DisplayPage();
    }

    private void DisplayPage()
    {
        pageID = Mathf.Clamp(pageID, 0, maxPage);

        GUIText.text = "Page " + (pageID + 1) + "/" + (maxPage + 1);

        if (pageID == lastPageID)
            return;
        else
        {
            CloseBrowser();
            lastPageID = pageID;
        }

        for (int id = 0; id < numberShow; id++)
        {
            if(pageID * numberShow + id < browserList.Count)
            {
                GameObject instance = Instantiate<GameObject>(UIBrowserPrefab, this.transform);
                instance.transform.localPosition = new Vector2(0 , maxHeight - (id * stepPosition));
                instance.GetComponent<BrowserIObject>().obj = browserList[pageID * numberShow + id];
                //instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetName();
                //instance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetContent();

                UIBox.Add(instance);
            }
        }
    }

    public void DisplayDescription(Object what)
    {
        if (what == null)
            return;

        if (!DescriptionRoot.activeSelf)
            DescriptionRoot.SetActive(true);

        selectedObject = what;
        //descriptionImage.sprite = what.sprit;
        descriptionText.text = what.description;
    }

    public void UseObject()
    {
        if (selectedObject == null)
            return;

        Debug.Log("Using object !");
    }
}
