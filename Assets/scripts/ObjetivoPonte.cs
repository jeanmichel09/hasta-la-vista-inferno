using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoPonte : Interactable
{
    private Animator myAnim;
    private BoxCollider2D bc;
    UI_Resource uiRes;
    Player player;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        bc = GetComponent<BoxCollider2D>();

        uiRes = FindObjectOfType<UI_Resource>();
    }

    private void Update()
    {
       
        


    }

    public override string GetDescription()
    {
        if (Player.fogoInfernal >= 1)
        {
            return "Aperte <B>E</B> para usar o <color=red>Fogo Infernal</color>";
        }
        return "Você precisa de <color=red>Fogo Infernal</color>";

    }




    public override void Interact()
    {
        if(Player.fogoInfernal >= 1)
        {
            myAnim.SetBool("OnClick", true);
            uiRes.DestroyLastInstance();
            Player.objetivo++;
            Player.fogoInfernal--;
            bc.enabled = false;

        }

    }
}
