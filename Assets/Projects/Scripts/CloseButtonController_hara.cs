using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonController_hara : MonoBehaviour
{
    public GameObject popupPanel;
     public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
