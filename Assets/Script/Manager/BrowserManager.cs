using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private GameObject UIBrowserPrefab;
    [SerializeField] private int numberShow = 4;
    [SerializeField] private int stepPosition = 10;
    int pageID = 0;
    int maxPage;

    List<IBrowsable> browserList = new List<IBrowsable>();

    public void ShowBrowser<Tsource>(List<Tsource> source) where Tsource : IBrowsable
    {
        maxPage = (int)(source.Count / numberShow);
        pageID = 0;
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

        for (int id = 0; id < numberShow; id++)
        {
            if(pageID * numberShow + id < browserList.Count)
            {
                GameObject instance = Instantiate<GameObject>(UIBrowserPrefab, this.transform);
                instance.transform.localPosition = new Vector2(0 , (id - 2) * stepPosition);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetName();
                instance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = browserList[pageID * numberShow + id].GetContent();
            }
        }
    }
}
