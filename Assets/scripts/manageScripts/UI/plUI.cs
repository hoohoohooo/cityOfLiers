using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class plUI : MonoBehaviour
{
    public Image staminaBar = null;
    public Image hungerBar = null;
    public playerData plData;
    // Start is called before the first frame update
    void Start()
    {
        plData = gameMng.instance.plCont.data;
    }

    // Update is called once per frame
    void Update()
    {
        if(staminaBar == null||hungerBar == null)
        {
            return;
        }
        else
        {
            staminaBar.fillAmount = plData.stamina / playerData.maxStam;
            hungerBar.fillAmount = plData.hunger / playerData.maxHunger;
        }
    }
}
