using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        long moneyPerClick = DataControl.Instance.m_moneyPerClick;
        DataControl.Instance.m_money += moneyPerClick;
    }
}
