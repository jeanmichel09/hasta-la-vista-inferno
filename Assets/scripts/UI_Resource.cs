using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Resource : MonoBehaviour
{
    private Transform resourceTemplate;
    private Transform resourceContainer;
    void Start()
    {
        resourceContainer = transform.Find("resourceContainer");
        resourceTemplate = resourceContainer.transform.Find("resourceTemplate");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void InstantiateRect()
    {
        RectTransform resourceRect = Instantiate(resourceTemplate, resourceContainer).GetComponent<RectTransform>();
        resourceRect.gameObject.SetActive(true);

        Image image = resourceRect.Find("resImage").GetComponent<Image>();
    }

    public void DestroyLastInstance()
    {
        int numChildren = resourceContainer.childCount;

        if(numChildren > 0)
                Destroy(resourceContainer.GetChild(numChildren - 1).gameObject);
        }
    

}
