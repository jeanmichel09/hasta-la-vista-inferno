using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Alma : Interactable
{
    BoxCollider2D bc;
    private AIPath aIPath;
    private AIDestinationSetter destinationSetter;
    Animator myAnim;
    UI_Resource uiRes;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        aIPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        uiRes = FindObjectOfType<UI_Resource>();

    }
    private void Update()
    {

       
    }
    private void FaceVelocity()
    {


        aIPath.enableRotation = false;
        var direction = aIPath.desiredVelocity;
        //transform.up = direction;

        if (direction != Vector3.zero)
        {
            aIPath.rotation = Quaternion.LookRotation(Vector3.zero);
        }
        //myAnim.SetFloat("moveX", direction.x);
        //myAnim.SetFloat("moveY", direction.y);
        myAnim.SetFloat("speed", direction.magnitude);

    }
    public override string GetDescription()
    {
        myAnim.SetBool("PlayerNear", true);
        if(Player.fogoInfernal > 0)
        {
        return "Aperte <B>E</B> para usar <B>1</B> <color=red>Fogo Infernal</color>";

        }
        return "Você precisa de <color=red>Fogo Infernal</color> para interagir";
    }

    public override void Interact()
    {
        if(Player.fogoInfernal >= 1)
        {
            uiRes.DestroyLastInstance();
            Player.fogoInfernal--;
            FaceVelocity();
            destinationSetter.enabled = true;
            bc.enabled = false;
        }
        
    }


}
