using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAdd : MonoBehaviour
{
   public void ClickTheButton()
    {
        GlobalStageOfRocket.CurrentBuild += 1;
    }

}
