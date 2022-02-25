using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class Fuels : MonoBehaviour
{
    public EventSystem eventSystem;
    public fuelButton[] fuelB;
    private Dictionary<string, Fuel> fuels = new Dictionary<string, Fuel>();
    private Dictionary<Button, string> buttonfuel = new Dictionary<Button, string>();

    [System.Serializable]
    public struct fuelButton
    {
        public Button button;
        public string name;
    }

    [System.Serializable]
    class Fuel
    {
        private Text ButtonLabel;
        private Text PriceLabel;
        private Text BuildLabel;
        public bool state;
        public ulong currentPrice;
        private int amount;
        private string FuelName;

        public Fuel(Text buttonLabel, Text priceLabel, Text buildLabel, string fuelName)
        {
            ButtonLabel = buttonLabel;
            PriceLabel = priceLabel;
            BuildLabel = buildLabel;
            FuelName = fuelName;
            state = false;

            amount = 0;
            currentPrice = MonetarySystemData.getPrice(fuelName);
            ButtonLabel.text = "Buy";
            PriceLabel.text = currentPrice.ToString();
            BuildLabel.text = BuildingSystemData.getBuildCost(fuelName).ToString();
        }

        public void ChangeState()
        {
            if (GlobalMoneyStore.MoneyCounter >= currentPrice)
            {
                GlobalMoneyStore.MoneyCounter -= currentPrice;
                amount += AmountSystemData.getAmount(FuelName);
                ButtonLabel.text = "Buy more" +
                    "\n" + "current amount: " + amount.ToString();
                MonetarySystemData.getPrice(FuelName);
                PriceLabel.text = currentPrice.ToString();
            }
        }
    }

    public void InitFuel(string agent, Text buttonL, Text priceL, Text buildL)
    {
        if (!fuels.ContainsKey(agent))
        {
            fuels[agent] = new Fuel(buttonL, priceL, buildL, agent);
        }
    }

    public void FuelClicked()
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
            for (int i = 0; i < fuelB.Length; i++)
            {
                buttonfuel[fuelB[i].button] = fuelB[i].name;
                InitFuel(fuelB[i].name, fuelB[i].button.transform.GetChild(0).GetComponent<Text>(), 
                    fuelB[i].button.transform.GetChild(1).GetChild(1).GetComponent<Text>(),
                    fuelB[i].button.transform.GetChild(1).GetChild(3).GetComponent<Text>());
            }
        }
    }
}
