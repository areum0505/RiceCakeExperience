using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Text itemDisplayer;

    public CanvasGroup canvasGroup;

    public string itemName;

    public int level;

    [HideInInspector]
    public int currentCost;
    public int startCurrentCost = 1;

    [HideInInspector]
    public int moneyPerSec;
    public int startMoneyPerSec = 1;

    public float upgradePow = 1.5f;
    public float costPow = 1.7f;

    [HideInInspector]
    public bool isPurchased = false;

    public bool isMax = false;

    private void Start()
    {
        DataControl.Instance.LoadItemButton(this);
        StartCoroutine("AddGoldLoop");
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
        DataControl.Instance.Check();
    }

    public void Purchase()
    {
        if(DataControl.Instance.m_money >= currentCost && level < 20)
        {
            isPurchased = true;
            DataControl.Instance.m_money -= currentCost;
            level++;

            UpdateItem();
            UpdateUI();
            DataControl.Instance.SaveItemButton(this);
        }
    }

    IEnumerator AddGoldLoop()
    {
        while(true)
        {
            if (isPurchased)
            {
                DataControl.Instance.m_money += moneyPerSec;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateItem()
    {
        moneyPerSec += startMoneyPerSec * (int)Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, level);
    }
    public void UpdateUI()
    {
        string textLevel = level.ToString();
        if (level >= 20)
        {
            textLevel = "MAX";
            isMax = true;
        }
        itemDisplayer.text = itemName + "\n 가격 : " + currentCost + " 레벨 : " + textLevel;

        if(currentCost <= DataControl.Instance.m_money)
        {
            canvasGroup.alpha = 1.0f;
        } else
        {
            canvasGroup.alpha = 0.5f;
        }
    }
}
