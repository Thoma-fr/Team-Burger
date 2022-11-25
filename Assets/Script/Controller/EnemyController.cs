using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

public class EnemyController : BaseController , IShootable<PlayerData>
{
    [SerializeField] private int iDEnemy;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Transform canvasParent;

    public EnemyData m_data { get; private set; }

    private TextMeshProUGUI pvText;
    private TextMeshProUGUI nameText;
    private Slider slider;
    private Image fill;

    private bool isSetSliderValue = false;
    private bool isInCombat = true;

    private void Awake()
    {
        nameText = canvasParent.GetChild(0).GetComponent<TextMeshProUGUI>();
        slider = canvasParent.GetChild(1).GetComponent<Slider>();
        pvText = slider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        fill = slider.transform.GetChild(1).GetComponent<Image>();

        canvasParent.gameObject.SetActive(false);
        canvasParent.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void Start()
    {
        if(iDEnemy < GameManager.instance.GetDefaultEnemiesData.allEnemiesData.Count)
            m_data = new EnemyData(GameManager.instance.GetDefaultEnemiesData.allEnemiesData[iDEnemy]);

        canvasParent.GetComponent<Canvas>().worldCamera = Camera.main;

        nameText.text = m_data.name;
        slider.maxValue = m_data.maxHealth;
        slider.value = m_data.healthPoint;
        pvText.text = ((int)slider.value).ToString();
        fill.color = gradient.Evaluate(1.0f);
    }

    public int EnemyAttacking(out string attName)
    {
        Attack current;
        if (m_data.attacks.Count >= 1)
            current = m_data.attacks[Random.Range(0, m_data.attacks.Count)];
        else
        {
            current = new Attack("Coups de boule", 20);
            Debug.Log("Attack par défault");
        }

        attName = current.attName;
        return current.damage;
    }

    private void Update()
    {
        if (!isInCombat)
            return;

        canvasParent.rotation = Quaternion.LookRotation(canvasParent.transform.position - GameManager.instance.mainCam.position, -Vector3.forward);

        if (isSetSliderValue)
        {
            pvText.text = ((int)slider.value).ToString();
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    public void InitCombat()
    {
        canvasParent.gameObject.SetActive(true);
        canvasParent.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
    }

    public void TakeDamage(int damage)
    {
        isSetSliderValue = true;
        
        m_data.healthPoint -= damage;

        Sequence takeDamageSequence = DOTween.Sequence();
        takeDamageSequence.Append(slider.DOValue(m_data.healthPoint, 1.8f));
        takeDamageSequence.AppendCallback(() => isSetSliderValue = false);
    }

    public void OnInteraction(PlayerData actuator)
    { 
    }
}
