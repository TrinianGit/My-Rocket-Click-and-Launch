using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAdd : MonoBehaviour
{
   public void ClickTheButton()
    {
        GlobalMoneyStore.MoneyCounter += 100000;
    }

}
