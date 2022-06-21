using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    float deathTimer = 1f;
    float hitTimer = 1f;
    float fade = 1f;
    [SerializeField] float hitAnimationSpeed = 1, deathSpeed = 1; //DeathSpeed também controla o tempo da animação
    bool isDissolving = false;
    bool isTouching = false;

    public SliderController healthBar;

    Material material;
    Light2D lightFade;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        material = GetComponent<SpriteRenderer>().material;
        if(isDissolving)
        {
            fade -= (Time.deltaTime*deathSpeed);
            if (fade <= 0)
            {
                fade = 0f;
            }
            var light = GameObject.FindWithTag("PlayerLight");
            lightFade = light.GetComponent<Light2D>();
            lightFade.intensity = fade;
            material.SetFloat("_Fade", fade);
        }
        if (currentHealth <= 0)
        {
            isDissolving = true;
            Player.isDead = true;
            deathTimer -= (Time.deltaTime*deathSpeed);
            if (deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        if (isTouching)
        {
            hitTimer -= (Time.deltaTime*hitAnimationSpeed);
            material.SetFloat("_HitAmount", hitTimer);
            if(hitTimer <= 0)
            {
                isTouching = false;
            }
        }else
        {
            hitTimer = 1;
        }
        //Debug.Log(hitTimer);
    }



    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        healthBar.SetHealth(currentHealth);
        isTouching = true;
    }
    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        healthBar.SetHealth(currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
