using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Final : MonoBehaviour
{
    public Animator myAnim;

   
    public void RestartButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitButton()
    {
        Application.Quit();
    }


    public void Activate()
    {
        myAnim.SetBool("isActive", true);
        gameObject.SetActive(true);
    }
}
