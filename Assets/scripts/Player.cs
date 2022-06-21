using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 saida;
        public Vector3 shootPosition;
    }
    public static Player Instance { get; private set; }
    public static int objetivo = 0;
    public static int fogoInfernal = 0;
    public static bool isDead = false;

    private SceneManager sceneManager;
    private Rigidbody2D myRB;
    private Animator myAnim;
    private Transform aimTransform;
    private Transform aimSaidaTransform;
    public Inventory inventory;
    private HealthManager hm;
    private Transform lightTransform;
    private GameObject ponteBlock2;
    public GameOver gameOver;
    private StartCutscene startCutscene;

    //private Vector2 posDif;
    
    
    private float speed = 200f;
    [SerializeField] private int objetivosACumprir;
    [SerializeField] private float thrust = 1f;
    [SerializeField] private UI_Inventory uiInventory;

    void Start()
    {
        GameObject go = GameObject.Find("PlayerLight");
        lightTransform = go.transform;

        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        aimTransform = transform.Find("Aim");
        aimSaidaTransform = aimTransform.Find("Saida");

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        ponteBlock2 = GameObject.Find("PathBlock_Ponte2");

        startCutscene = FindObjectOfType<StartCutscene>();

        
    }
    private void Update()
    {
        //    movement.x = Input.GetAxisRaw("Horizontal");
        //if (movement.x == 0)
        //{
        //    movement.y = Input.GetAxisRaw("Vertical");
        //    movement.x = 0;
        //}
        //else
        //{
        //    movement.y = 0;
        //}
 
        HandleAiming();
        HandleShooting();
        LightDir();

        Debug.Log("Fogo Infernal: " + fogoInfernal);
        Debug.Log("Objetivos: " + objetivo);

        if (SceneManager.GetActiveScene().buildIndex == 1) { 
            if (objetivo == objetivosACumprir)
        {
                startCutscene.StartCutsceneOne();
                Destroy(ponteBlock2);
                objetivosACumprir = 0;
                objetivo = 0;  
        }
             }

        if (isDead)
        {
            gameOver.Activate();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (objetivo == objetivosACumprir)
            {
                Debug.Log("Top");
                objetivo = 0;
            }

        }

    }

    void FixedUpdate()
    {
        if(!StartCutscene.isCutsceneOn)
        { 
        myRB.velocity = speed * Time.deltaTime * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //myRB.velocity = speed * Time.deltaTime * new Vector2(movement.x, movement.y);
        if (Input.GetButton("Run"))
        {
            speed = 300f;
            myAnim.speed = 1.5f;

        }
        else
        {
            speed = 200f;
            myAnim.speed = 1f;
        }
        if (Input.GetButton("Jump"))
        {
            
            myRB.AddForce(aimTransform.right * thrust, ForceMode2D.Impulse);
        }

        //Vector2 mousePos = UtilsClass.GetMouseWorldPosition();
        //posDif = mousePos - myRB.position;

        myAnim.SetFloat("moveX", myRB.velocity.x);
        //myAnim.SetFloat("moveX", posDif.x);
        myAnim.SetFloat("moveY", myRB.velocity.y);
        //myAnim.SetFloat("moveY", posDif.y);
        myAnim.SetFloat("speed", myRB.velocity.magnitude);
       
        


        if(!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0.0f) || !Mathf.Approximately(Input.GetAxisRaw("Vertical"),0.0f))
        {
            myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            //myAnim.SetFloat("lastMoveX", posDif.x);
            myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            //myAnim.SetFloat("lastMoveY", posDif.y);
        }
        }
        
        //Debug.Log(myRB.velocity);
    }

  
    private void LightDir()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        //Vector3 aimDirection = (mousePosition - lightTransform.position).normalized;
        //float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //lightTransform.eulerAngles = new Vector3(0, 0, angle);


        Vector3 lightDir = mousePosition - transform.position;
        lightTransform.up = lightDir;
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = mousePosition - transform.position;
        aimTransform.right = aimDirection;
        //Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        //float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //aimTransform.eulerAngles = new Vector3(0, 0, angle);
        //Debug.Log(aimDirection);
    }
    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1")) {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            OnShoot?.Invoke(this, new OnShootEventArgs{
                saida = aimSaidaTransform.position,
                shootPosition = mousePosition,
            });

        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

        if(itemWorld != null)
        {
            //Touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();

        }

    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                //To Do: a��o de dar vida
                hm = gameObject.GetComponent<HealthManager>();
                int maxH = hm.maxHealth;
                int currentH = hm.currentHealth;
                if (currentH < maxH)
                {

                hm.GiveHealth(10);
                }
                else
                {
                    hm.GiveHealth(0);
                }
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                
                break;

        }
    }
}
