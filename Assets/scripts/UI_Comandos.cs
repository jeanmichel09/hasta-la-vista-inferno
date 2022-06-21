using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Comandos : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 15)
        {
            Destroy(gameObject);
        }
    }
}
