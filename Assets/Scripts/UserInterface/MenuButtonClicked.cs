using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonClicked : MonoBehaviour
{
    public GameObject StaffPanel;
    public GameObject FuelPanel;
    public GameObject RocketPanel;
    public GameObject LocationPanel;
    public GameObject OthersPanel;
    public Button ClickPane;
    public void ButtonClicked(GameObject obj)
    {
        if (obj.activeSelf == false)
        {
            StaffPanel.SetActive(false);
            FuelPanel.SetActive(false);
            RocketPanel.SetActive(false);
            LocationPanel.SetActive(false);
            OthersPanel.SetActive(false);
            obj.SetActive(true);
            ClickPane.interactable = false;
        }
        else
        {
            obj.SetActive(false);
            ClickPane.interactable = true;
        }
    }
}
