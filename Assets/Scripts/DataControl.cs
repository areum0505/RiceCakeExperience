using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataControl : MonoBehaviour
{
    private static DataControl instance;
    public static DataControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataControl>();
                if (instance == null)
                {
                    GameObject container = new GameObject("DataControl");
                    container.AddComponent<DataControl>();
                }
            }
            return instance;
        }
    }

    public long m_money
    {
        get
        {
            if(!PlayerPrefs.HasKey("Money"))
            {
                return 0;
            }

            string money = PlayerPrefs.GetString("Money");
            return long.Parse(money);
        }
        set
        {
            PlayerPrefs.SetString("Money", value.ToString());
        }
    }

    public long m_moneyPerClick
    {
        get
        {
            if (!PlayerPrefs.HasKey("MoneyPerClick"))
            {
                return 1;
            }

            string mpc = PlayerPrefs.GetString("MoneyPerClick", "1");
            return long.Parse(mpc);
        }
        set
        {
            PlayerPrefs.SetString("MoneyPerClick", value.ToString());
        }
    }

    private ItemButton[] itemButtons;
    private UpgradeButton[] upgradeButtons;

    void Awake()
    {
        itemButtons = FindObjectsOfType<ItemButton>();
        upgradeButtons = FindObjectsOfType<UpgradeButton>();
    }

    public void LoadUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        upgradeButton.level = PlayerPrefs.GetInt(key + "_level", 0);
        upgradeButton.moneyByUpgrade = PlayerPrefs.GetInt(key + "_moneyByUpgrade", upgradeButton.startMoneyByUpgrade);
        upgradeButton.currentCost = PlayerPrefs.GetInt(key + "_cost", upgradeButton.startCurrentCost);
    }
    public void SaveUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        PlayerPrefs.SetInt(key + "_level", upgradeButton.level);
        PlayerPrefs.SetInt(key + "_moneyByUpgrade", upgradeButton.moneyByUpgrade);
        PlayerPrefs.SetInt(key + "_cost", upgradeButton.currentCost);
    }

    public void LoadItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        itemButton.level = PlayerPrefs.GetInt(key + "_level", 0);
        itemButton.currentCost = PlayerPrefs.GetInt(key + "_cost", itemButton.startCurrentCost);
        itemButton.moneyPerSec = PlayerPrefs.GetInt(key + "_moneyPerSec");

        if(PlayerPrefs.GetInt(key + "_isPurchased") == 1)
        {
            itemButton.isPurchased = true;
        } else
        {
            itemButton.isPurchased = false;
        }
    }
    public void SaveItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key + "_level", itemButton.level);
        PlayerPrefs.SetInt(key + "_cost", itemButton.currentCost);
        PlayerPrefs.SetInt(key + "_moneyPerSec", itemButton.moneyPerSec);

        if (itemButton.isPurchased)
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 0);
        }
    }

    public int GetMoneyPerSec()
    {
        int moneyPerSec = 0;
        for(int i = 0; i < itemButtons.Length; i++)
        {
            moneyPerSec += itemButtons[i].moneyPerSec;
        }
        return moneyPerSec;
    }

    public bool Check()
    {
        foreach (ItemButton ib in itemButtons)
        {
            if (!ib.isMax)
            {
                return false;
            }
        }
        foreach (UpgradeButton ub in upgradeButtons)
        {
            if (!ub.isMax)
            {
                return false;
            }
        }
        StartCoroutine("goEnding");
        return true;
    }

    IEnumerator goEnding()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Ending");
    }

}
