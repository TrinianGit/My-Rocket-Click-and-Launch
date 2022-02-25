using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class AgentsAndWorkers : MonoBehaviour
{
    public EventSystem eventSystem;
    public agentButton[] agentB;
    private Dictionary<string, Agent> agents = new Dictionary<string, Agent>();
    private Dictionary<Button, string> buttonagent = new Dictionary<Button, string>();

    [System.Serializable]
    public struct agentButton
    {
        public Button button;
        public string name;
    }

    [System.Serializable]
    class Agent
    {
        private Text ButtonLabel;
        private Text PriceLabel;
        public bool state;
        public ulong currentPrice;
        public int level;
        private string AgentName;
        
        public Agent(Text buttonLabel, Text priceLabel, string agentName)
        {
            ButtonLabel = buttonLabel;
            PriceLabel = priceLabel;
            AgentName = agentName;
            state = false;
            level = 0;
            currentPrice = MonetarySystemData.getPrice(AgentName, level);
            ButtonLabel.text = "Buy";
            PriceLabel.text = currentPrice.ToString();
        }

        public void ChangeState()
        {
            if (GlobalMoneyStore.MoneyCounter >= currentPrice)
            {
                GlobalMoneyStore.MoneyCounter -= currentPrice;
                level++;
                ButtonLabel.text = "Upgrade" +
                    "\n" + "current lvl: " + level.ToString();
                MonetarySystemData.getPrice(AgentName, level);
                PriceLabel.text = currentPrice.ToString();
            }
        }
    }

    public void InitAgent(string agent, Text buttonL, Text priceL)
    {
        if (!agents.ContainsKey(agent))
        {
            agents[agent] = new Agent(buttonL, priceL, agent);
        }
    }

    public void AgentClicked()
    {
        agents[buttonagent[eventSystem.currentSelectedGameObject.GetComponent<Button>()]].ChangeState();
    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {

        }
        else
        {
            for (int i = 0; i < agentB.Length; i++)
            {
                buttonagent[agentB[i].button] = agentB[i].name;
                InitAgent(agentB[i].name, agentB[i].button.transform.GetChild(0).GetComponent<Text>(), agentB[i].button.transform.GetChild(1).GetChild(1).GetComponent<Text>());
            }
        }
    }
}
