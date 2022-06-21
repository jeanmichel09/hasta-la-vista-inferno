using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform uiBackground;
    private Animator myAnim;
    int inventoryCounter;
    private Player player;
    private RectTransform itemSlotRectTransform;

    private void Start()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        uiBackground = transform.Find("background");
        myAnim = GetComponent<Animator>();

    }
    private void Update()
    {
        ActivateInventory();
    }
    public void ActivateInventory()
    {
        KeyCode key = KeyCode.I;
        if (Input.GetKeyDown(key))
        {
            inventoryCounter++;
            if (inventoryCounter % 2 == 1)
            {
                uiBackground.gameObject.SetActive(true);
                itemSlotContainer.gameObject.SetActive(true);
                myAnim.SetBool("Open", true);
            }
            else
            {
                uiBackground.gameObject.SetActive(false);
                itemSlotContainer.gameObject.SetActive(false);
                myAnim.SetBool("Open", false);
            }
        }
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);

        }

        //foreach (Transform child in resourceContainer)
        //{
        //    if (child == resourceTemplate) continue;
        //    Destroy(child.gameObject);

        //}

        //foreach (Item item in inventory.GetItemList())
        //{

        //    if (item.itemType == Item.ItemType.FogoInfernal)
        //    {

        //        Debug.Log("Fogo Infernal: " + item.amount);

        //            RectTransform resourceRect = Instantiate(resourceTemplate, resourceContainer).GetComponent<RectTransform>();
        //            resourceRect.gameObject.SetActive(true);

        //            Image image = resourceRect.Find("resImage").GetComponent<Image>();

        //        //resourceRect.GetComponent<Button_UI>().ClickFunc = () =>
        //        //{
        //        //    // Use item
        //        //    inventory.UseItem(item);
        //        //};
        //    }

        //    else
        foreach (Item item in inventory.GetItemList())
        {
              
                itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {
                // Use item
                inventory.UseItem(item);
                };
                itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                // Drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                    inventory.RemoveItem(item);
                    ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                };

                Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                image.sprite = item.GetSprite();



                TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
                if (item.amount > 1)
                {
                    uiText.SetText(item.amount.ToString());
                }
                else
                {
                    uiText.SetText("");
                }

            }

            
       
            
        }

    }



        
        
    


