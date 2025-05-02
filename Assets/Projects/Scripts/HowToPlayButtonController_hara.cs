using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayButtonController_hara : MonoBehaviour
{
    public GameObject popupPanel;

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }
}
