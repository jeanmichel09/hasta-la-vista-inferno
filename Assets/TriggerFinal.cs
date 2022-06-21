using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinal : MonoBehaviour
{
    public UI_Final final;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {

        final.Activate();
        }
    }
}
