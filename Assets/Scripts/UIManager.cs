using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text moneyDisplayer;
    public Text moneyPerClickDisplayer;
    public Text moneyPerSecondDisplayer;

    void Update()
    {
        moneyDisplayer.text = DataControl.Instance.m_money + "원";
        moneyPerClickDisplayer.text = DataControl.Instance.m_moneyPerClick + "원/클릭";
        moneyPerSecondDisplayer.text = DataControl.Instance.GetMoneyPerSec() + "원/초";
    }
}
