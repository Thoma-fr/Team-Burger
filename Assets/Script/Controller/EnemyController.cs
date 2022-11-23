using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyController : BaseController , IShootable<PlayerData>
{
    [SerializeField] private int iDEnemy;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Transform canvasParent;

    public EnemyData m_data { get; private set; }

    private TextMeshProUGUI pvText;
    private TextMeshProUGUI nameText;
    private Slider slider;

    private bool isSetSliderValue = false;
    private bool isInCombat = true;

    private void Awake()
    {
        nameText = canvasParent.GetChild(0).GetComponent<TextMeshProUGUI>();
        slider = canvasParent.GetChild(1).GetComponent<Slider>();
        pvText = slider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        canvasParent.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(iDEnemy < GameManager.instance.GetDefaultEnemiesData.allEnemiesData.Count)
            m_data = new EnemyData(GameManager.instance.GetDefaultEnemiesData.allEnemiesData[iDEnemy]);

        nameText.text = m_data.name;
        slider.maxValue = m_data.maxHealth;
        slider.value = m_data.healthPoint;
        pvText.text = ((int)slider.value).ToString();
    }

    public string EnemyAttacking(out int damage)
    {
        Attack current;
        if (m_data.attacks.Count >= 1)
            current = m_data.attacks[Random.Range(0, m_data.attacks.Count)];
        else
        {
            current = new Attack("Coups de boule", 20);
            Debug.Log("Attack par défault");
        }

        damage = current.damage;
        return current.attName;
    }

    private void Update()
    {
        if (!isInCombat)
            return;

        canvasParent.rotation = Quaternion.LookRotation(canvasParent.transform.position - Camera.main.transform.position, Vector3.up);

        if (isSetSliderValue)
            pvText.text = ((int)slider.value).ToString();
    }

    /*public bool Subscribing(out EnemyController ec, out EnemyData data)
    {
        try
        {


            ec = this;
            data = m_data;
            return true;
        }
        catch
        {
            ec = null;
            data = null;
            return false;
        }
    }

    public bool UnSubscribing()
    {
        return true;
    }*/

    public void InitCombat()
    {
        canvasParent.gameObject.SetActive(true);
    }

    public bool TakeDamage(int damage)
    {
        isSetSliderValue = true;
        m_data.healthPoint -= damage;

        Sequence takeDamageSequence = DOTween.Sequence();
        takeDamageSequence.Append(slider.DOValue(m_data.healthPoint, 1.8f));
        takeDamageSequence.AppendCallback(() => isSetSliderValue = false);

        if (m_data.healthPoint <= 0)
            return true;
        else
            return false;
    }

    public void OnInteraction(PlayerData actuator)
    { 
    }
}
