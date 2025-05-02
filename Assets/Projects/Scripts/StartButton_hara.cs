using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton_hara : MonoBehaviour
{
    public void OnstartButtonClicked(){
        SceneManager.LoadScene("GameScene_hara");
    }
}
