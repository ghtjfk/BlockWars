using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcher : MonoBehaviour
{

    public enum Mode { Attack, Heal }
    public Mode curruntMode = Mode.Attack;

    public Button SwitchButton;
    void Start()
    {
        SwitchButton.onClick.AddListener(SwitchMode);
    }
    
    void SwitchMode()
    {
        if (curruntMode == Mode.Attack)
        {
            curruntMode = Mode.Heal;
            Debug.Log("Heal");
        }
        else
        {
            curruntMode = Mode.Attack;
            Debug.Log("Attack");
        }

    }


 
}
