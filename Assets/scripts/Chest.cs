using System;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public bool isClosed = true;
    private Animator myAnim;
    BoxCollider2D bc;
    [SerializeField] private Item item;
    static ItemWorld itemWorld;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        
    }


    private void UpdateChest()
    {
        if (!isClosed)
        {
            myAnim.SetBool("Click", true);
            bc.enabled = false;
            isClosed = false;
            itemWorld = ItemWorld.SpawnItemWorld((transform.position + new Vector3(0,1,0)), item);
            itemWorld.GetComponent<Rigidbody2D>().AddForce((-transform.up) * 15f, ForceMode2D.Impulse);
        }
    }


    public override string GetDescription()
    {
        if (isClosed)
        {
            return "E";
        }
        else
        {
            return "";
        }
    }

    public override void Interact()
    {
        isClosed = !isClosed;
        UpdateChest();
        Debug.Log("E key was pressed.");
    }
}
