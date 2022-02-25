using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class RocketParts : MonoBehaviour
{
    public EventSystem eventSystem;
    public rocketButton[] rocketB;
    private Dictionary<string, Part> fuels = new Dictionary<string, Part>();
    private Dictionary<Button, string> buttonfuel = new Dictionary<Button, string>();

    [System.Serializable]
    public struct rocketButton
    {
        public Button button;
        public string name;
    }

    [System.Serializable]
    class Part
    {
        private Text ButtonLabel;
        private Text PriceLabel;
        private Text BuildLabel;
        public bool state;
        public ulong currentPrice;
        private int level;
        private string PartName;

        public Part(Text buttonLabel, Text priceLabel, Text buildLabel, string partName)
        {
            ButtonLabel = buttonLabel;
            PriceLabel = priceLabel;
            BuildLabel = buildLabel;
            PartName = partName;
            state = false;

            level = 0;
            currentPrice = MonetarySystemData.getPrice(PartName);
            ButtonLabel.text = "Buy";
            PriceLabel.text = currentPrice.ToString();
            BuildLabel.text = BuildingSystemData.getBuildCost(PartName).ToString();
        }

        public void ChangeState()
        {
            if (GlobalMoneyStore.MoneyCounter >= currentPrice)
            {
                GlobalMoneyStore.MoneyCounter -= currentPrice;
                level++;
                ButtonLabel.text = "Upgrade" +
                    "\n" + "current level: " + level.ToString();
                MonetarySystemData.getPrice(PartName);
                PriceLabel.text = currentPrice.ToString();
            }
        }
    }

    public void InitParts(string agent, Text buttonL, Text priceL, Text buildL)
    {
        if (!fuels.ContainsKey(agent))
        {
            fuels[agent] = new Part(buttonL, priceL, buildL, agent);
        }
    }

    public void RocPartClicked()
    {
        fuels[buttonfuel[eventSystem.currentSelectedGameObject.GetComponent<Button>()]].ChangeState();
    }

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {

        }
        else
        {
            for (int i = 0; i < rocketB.Length; i++)
            {
                buttonfuel[rocketB[i].button] = rocketB[i].name;
                InitParts(rocketB[i].name, rocketB[i].button.transform.GetChild(0).GetComponent<Text>(),
                    rocketB[i].button.transform.GetChild(1).GetChild(1).GetComponent<Text>(),
                    rocketB[i].button.transform.GetChild(1).GetChild(3).GetComponent<Text>());
            }
        }
    }
}
