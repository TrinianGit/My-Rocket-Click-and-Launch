using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalMoneyStore : MonoBehaviour
{
    public static int MoneyCounter;
    public Text MoneyDisplay;
    public int InternalMoney;
    // Start is called before the first frame update
    void Start()
    {
        InternalMoney = MoneyCounter;
        MoneyDisplay.text = InternalMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        InternalMoney = MoneyCounter;
        MoneyDisplay.text = InternalMoney.ToString();
    }
}
