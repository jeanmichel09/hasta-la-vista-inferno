using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    GameObject tutorial1;
    public GameObject tutorial2;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        tutorial1 = GameObject.Find("Tutorial1");
        
        
    }


    public void Proximo()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);

    }
    public void Fechar()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        
    }

}
