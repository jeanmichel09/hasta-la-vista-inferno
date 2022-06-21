using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;

    private SpriteRenderer enemySprite;
    float deathTimer = 1f;
    float hitTimer = 1f;
    float fade = 1f;
    [SerializeField] float hitAnimationSpeed = 1, deathSpeed = 1; //DeathSpeed também controla o tempo da animação
    bool isDissolving = false;
    bool isTouching = false;

    Material material;
    Light2D lightFade;

    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material = GetComponent<SpriteRenderer>().material;
        if (isDissolving)
        {
            fade -= (Time.deltaTime * deathSpeed);
            if (fade <= 0)
            {
                fade = 0f;
            }
            var enemyLight = GameObject.FindGameObjectWithTag("LuzInimigo");
            lightFade = enemyLight.GetComponent<Light2D>();
            lightFade.intensity = fade;

            material.SetFloat("_Fade", fade);
        }
        if (currentHealth <= 0)
        {
            isDissolving = true;
            deathTimer -= (Time.deltaTime * deathSpeed);
            if (deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        if (isTouching)
        {
            hitTimer -= (Time.deltaTime * hitAnimationSpeed);
            material.SetFloat("_HitAmount", hitTimer);
            if (hitTimer <= 0)
            {
                isTouching = false;
            }
        }
        else
        {
            hitTimer = 1;
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;
        isTouching = true;
    }
}
