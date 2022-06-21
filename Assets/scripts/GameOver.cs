using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator myAnim;

   
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        Player.isDead = false;
        Player.fogoInfernal = 0;
        Player.objetivo = 0;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void Activate()
    {
        myAnim.SetBool("isActive", true);
        gameObject.SetActive(true);
    }
}
