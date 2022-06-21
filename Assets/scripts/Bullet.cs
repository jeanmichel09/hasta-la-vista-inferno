using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    [SerializeField] float moveSpeed = 100f;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0,UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * shootDir;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy_controller enemy = collider.GetComponent<Enemy_controller>();
            EnemyHealth enemyHealth;
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.HurtEnemy(10);
            Destroy(gameObject);
        }
    }
}
