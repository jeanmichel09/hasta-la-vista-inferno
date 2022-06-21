using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;
    public GameObject interactionHoldGO; // the ui parent to disable when not interacting
    public UnityEngine.UI.Image interactionHoldProgress; // the progress bar for hold interaction type
    Interactable interactable;
    public bool hit;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            }
        }
        if(!hit)
        interactionText.text = "";
        interactionHoldGO.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        interactable = collider.GetComponent<Interactable>();
        
        hit = true;

    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        interactable = collider.GetComponent<Interactable>();
        hit = false;
    }

    public void HandleInteraction(Interactable interactable)
    {
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();
                   
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key))
                {
                    // we are holding the key, increase the timer until we reach 1f
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f) {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                }
                else
                {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;

            // helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}

