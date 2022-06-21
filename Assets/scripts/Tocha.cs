using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tocha : Interactable
{
    //public Inventory inventory;
    Animator myAnim;
    BoxCollider2D bc;
    UI_Resource uiRes;
    private void Start()
    {

        bc = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        uiRes = FindObjectOfType<UI_Resource>();
    }
    public override string GetDescription()
    {
        return "Aperte <B>E</B> para coletar <color=red>Fogo Infernal</color>";
    }

    public override void Interact()
    {
        Player.fogoInfernal += 1;
        uiRes.InstantiateRect();
        myAnim.SetBool("Click", true);
        bc.enabled = false;
    }
}
