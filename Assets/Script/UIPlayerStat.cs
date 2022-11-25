using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIPlayerStat : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI statText;

    private void Start()
    {
        GameManager.instance.onPlayerStatChanged += UpdateLifeBar;
        GameManager.instance.onPlayerStatChanged += UpdateStat;

        slider.maxValue = GameManager.instance.GetPlayerData.maxHealth;
        slider.value = GameManager.instance.GetPlayerData.healthPoint;

        UpdateLifeBar();
        UpdateStat();
    }

    public void UpdateLifeBar()
    {
        if (GameManager.instance.GetPlayerData.healthPoint != slider.value)
            slider.DOValue(GameManager.instance.GetPlayerData.healthPoint, 1.8f);
    }

    private void UpdateStat()
    {
        statText.text = GameManager.instance.GetPlayerData.healthPoint.ToString();
    }

    private void OnDisable()
    {
        GameManager.instance.onPlayerStatChanged -= UpdateLifeBar;
        GameManager.instance.onPlayerStatChanged -= UpdateStat;
    }
}
