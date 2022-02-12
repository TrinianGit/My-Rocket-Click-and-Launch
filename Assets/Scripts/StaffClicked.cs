using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffClicked : MonoBehaviour
{
    public GameObject StaffPanel;
    public Button ClickPane;
    public void ButtonClicked()
    {
        if (StaffPanel.activeSelf == false)
        {
            StaffPanel.SetActive(true);
            ClickPane.interactable = false;
        }
        else
        {
            StaffPanel.SetActive(false);
            ClickPane.interactable = true;
        }
    }
}
