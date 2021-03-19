using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject UpgradePanel;
    public GameObject ItemPanel;

    Vector3 upPosition;
    Vector3 downPosition;

    void Start()
    {
        upPosition = UpgradePanel.transform.position;
        downPosition = ItemPanel.transform.position;
    }

    public void UpgradeViewClick()
    {

        UpgradePanel.transform.position = upPosition;
        ItemPanel.transform.position = downPosition;
    }
    public void ItemViewClick()
    {
        ItemPanel.transform.position = upPosition;
        UpgradePanel.transform.position = downPosition;
    }
}
