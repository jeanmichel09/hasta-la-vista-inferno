using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    Animator myAnim;
    Ponte ponte;
    public static bool isCutsceneOn;

    private void Start()
    {
        ponte = FindObjectOfType<Ponte>();
        myAnim = GetComponent<Animator>();
    }
    private void Update()
    {

    }
    public void StartCutsceneOne()
    {
        isCutsceneOn = true;

        myAnim.SetBool("cutscene1", true);
        ponte.AtivarPonte();
        Invoke(nameof(StopCutsceneOne), 2f);
        
    }
    private void StopCutsceneOne()
    {
        isCutsceneOn = false;
        myAnim.SetBool("cutscene1", false);
    }


}
