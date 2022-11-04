using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private GameObject UIBrowserPrefab;
    [SerializeField] private TextMeshProUGUI GUIText;
    [SerializeField] private int numberShow = 4;
    [SerializeField] private int stepPosition = 10;
    int pageID = 0;
    int lastPageID;
    int maxPage;
    int maxHeight;

    List<Object> browserList;
    List<GameObject> UIBox = new List<GameObject>();

    public void ShowBrowser<Tsource>(List<Tsource> source) where Tsource : Object
    {
        browserList = source.Cast<Object>().ToList();
        maxPage = (int)(source.Count / numberShow);
        maxHeight = - (stepPosition * numberShow / 2);
        pageID = 0;
        lastPageID = 0;
        DisplayPage();
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

        GUIText.text = "Page " + pageID + "/" + maxPage;

        if (pageID == lastPageID)
            return;
        else
            lastPageID = pageID;

        foreach (GameObject obj in UIBox)
            Destroy(obj);

        for (int id = 0; id < numberShow; id++)
        {
            if(pageID * numberShow + id < browserList.Count)
            {
                GameObject instance = Instantiate<GameObject>(UIBrowserPrefab, this.transform);
                instance.transform.localPosition = new Vector2(0 , maxHeight + (id * stepPosition));
                //instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetName();
                //instance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetContent();

                UIBox.Add(instance);
            }
        }
    }
}
