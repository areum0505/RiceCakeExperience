using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text upgradeDisplayer;

    public CanvasGroup canvasGroup;

    public string upgradeName;

    [HideInInspector]
    public int moneyByUpgrade;
    public int startMoneyByUpgrade;

    [HideInInspector]
    public int currentCost = 1;
    public int startCurrentCost = 1;

    [HideInInspector]
    public int level = 0;

    public float upgradePow = 1.2f;
    public float costPow = 1.7f;

    public bool isMax = false;

    void Start()
    {
        DataControl.Instance.LoadUpgradeButton(this);
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
        DataControl.Instance.Check();
    }

    public void PurchaseUpgrade()
    {
        if (DataControl.Instance.m_money >= currentCost && level < 20)
        {
            DataControl.Instance.m_money -= currentCost;
            level++;
            DataControl.Instance.m_moneyPerClick += moneyByUpgrade;

            UpdateUpgrade();
            UpdateUI();
            DataControl.Instance.SaveUpgradeButton(this);
        }
    }

    public void UpdateUpgrade()
    {
        int tempLevel = 1;
        if (!(level == 0))
            tempLevel = level;


        moneyByUpgrade = startMoneyByUpgrade * (int)Mathf.Pow(upgradePow, tempLevel);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, tempLevel);
    }

    public void UpdateUI()
    {
        string textLevel = level.ToString();
        if (level >= 20)
        {
            textLevel = "MAX";
            isMax = true;
        }

        upgradeDisplayer.text = upgradeName + "\n가격 : " + currentCost + " 레벨 : " + textLevel;

        if (currentCost <= DataControl.Instance.m_money || textLevel.Equals("MAX"))
        {
            canvasGroup.alpha = 1.0f;
        }
        else
        {
            canvasGroup.alpha = 0.5f;
        }

        if(level >= 1)
        {
            GameObject.Find("RiceCakePanel").transform.Find("image_" + upgradeName).gameObject.SetActive(true);
        }
    }
}
