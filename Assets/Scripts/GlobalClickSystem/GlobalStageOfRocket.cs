using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GlobalStageOfRocket : MonoBehaviour
{
    public Text Progress;
    public Slider ProgressBar;
    public static int RocketBody = 35;
    public static int RocketHead = 35;
    public static int RocketFuel = 30;
    public static int CurrentBuild;
    public float CurrentB;
    public float AllRP;

    // Start is called before the first frame update
    void Start()
    {
        CurrentB = 0;
        AllRP = RocketBody + RocketHead + RocketFuel;
        Progress.text = (int)CurrentB + "\\" + (int)AllRP;
        ProgressBar.value = CurrentB / AllRP;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBuild < RocketBody + RocketHead + RocketFuel)
        {
            CurrentB = CurrentBuild;
        }
        else
        {
            CurrentB = RocketBody + RocketHead + RocketFuel;
        }
        AllRP = RocketBody + RocketHead + RocketFuel;
        Progress.text = (int)CurrentB + "\\" + (int)AllRP;
        ProgressBar.value = CurrentB / AllRP;
    }
}
