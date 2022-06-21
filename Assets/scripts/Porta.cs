using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : Interactable
{
    private int interagiu = 0;
    UI_Resource uiRes;
    private Animator myAnim;
    BoxCollider2D[] myColliders;

    private void Start()
    {
        uiRes = FindObjectOfType<UI_Resource>();
        myAnim = GetComponent<Animator>();
        myColliders = GetComponents<BoxCollider2D>();
    }
    private void PortaAberta()
    {
        myAnim.SetBool("aberta", true);
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
        interagiu++;
        if (Player.fogoInfernal >= 1)
        {
            if (interagiu == 1 && Player.objetivo == 0)
            {
                Player.fogoInfernal--;
                Player.objetivo++;
                uiRes.DestroyLastInstance();
                myAnim.SetBool("umFogo", true);
            }else if(interagiu == 2 && Player.objetivo == 1)
            {
                Player.fogoInfernal--;
                Player.objetivo++;
                uiRes.DestroyLastInstance();
                myAnim.SetBool("doisFogos", true);
            }else if (interagiu == 3 && Player.objetivo == 2)
            {
                Player.fogoInfernal--;
                Player.objetivo++;
                uiRes.DestroyLastInstance();
                myAnim.SetBool("tresFogos", true);
                Invoke(nameof(PortaAberta), 1f);
                foreach (BoxCollider2D bc in myColliders) bc.enabled = false;
            }
        }
        Debug.Log("Interagiu " + interagiu + " Vezes");
    }
}
