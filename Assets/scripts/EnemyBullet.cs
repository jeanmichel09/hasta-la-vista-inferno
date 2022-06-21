using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 shootDir;
    [SerializeField] float moveSpeed;
    [SerializeField] int damageToGiveToPlayer;
    HealthManager hm;

    private void Start()
    {
        hm = FindObjectOfType<Player>().GetComponent<HealthManager>();
    }
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * shootDir;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            hm.HurtPlayer(damageToGiveToPlayer);
            Destroy(gameObject);
        }
    }
}
