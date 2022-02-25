using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class Locations : MonoBehaviour
{
    public EventSystem eventSystem;
    public locationButton[] locationB;
    private Dictionary<string, Location> locations = new Dictionary<string, Location>();
    private Dictionary<Button, string> buttonloc = new Dictionary<Button, string>();

    [System.Serializable]
    public struct locationButton
    {
        public Button button;
        public string name;
    }

    [System.Serializable]
    class Location
    {
        private Text ButtonLabel;
        private Text PriceLabel;
        public bool state;
        public ulong currentPrice;
        private string LocName;
        private Button button;

        public Location(Text buttonLabel, Text priceLabel, string locationName, Button b)
        {
            ButtonLabel = buttonLabel;
            PriceLabel = priceLabel;
            LocName = locationName;
            button = b;
            state = false;

            currentPrice = MonetarySystemData.getPrice(LocName);
            ButtonLabel.text = "Buy";
            PriceLabel.text = currentPrice.ToString();
        }

        public bool BuyPossible()
        {
            return GlobalMoneyStore.MoneyCounter >= currentPrice ? true : false;
        }
        public void ChangeState()
        {
            if (GlobalMoneyStore.MoneyCounter >= currentPrice && !state)
            {
                GlobalMoneyStore.MoneyCounter -= currentPrice;
                state = true;
                ButtonLabel.text = "Current";
                button.transform.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
                PriceLabel.enabled = false;
                button.enabled = false;
            }
        }
        public void TurnOff()
        {
            if (state)
            {
                ButtonLabel.text = "Previous";
            }
        }
    }

    public void InitLoc(string agent, Text buttonL, Text priceL, Button b)
    {
        if (!locations.ContainsKey(agent))
        {
            locations[agent] = new Location(buttonL, priceL, agent, b);
        }
    }

    public void LocationClicked()
    {
        if (locations[buttonloc[eventSystem.currentSelectedGameObject.GetComponent<Button>()]].BuyPossible())
        {
            GetRidOfPrev();
        }
        locations[buttonloc[eventSystem.currentSelectedGameObject.GetComponent<Button>()]].ChangeState();
    }

    public void GetRidOfPrev()
    {
        foreach (var item in locations)
        {
            item.Value.TurnOff();
        }
    }
    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {

        }
        else
        {
            for (int i = 0; i < locationB.Length; i++)
            {
                buttonloc[locationB[i].button] = locationB[i].name;
                InitLoc(locationB[i].name, locationB[i].button.transform.GetChild(0).GetComponent<Text>(), 
                    locationB[i].button.transform.GetChild(1).GetChild(1).GetComponent<Text>(),
                    locationB[i].button);
            }
            locations[locationB[0].name].ChangeState();
        }
    }
}
