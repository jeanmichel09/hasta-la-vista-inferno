using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform pfEnemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Enemy_controller>().EnemyOnShoot += EnemyShootProjectile_EnemyOnShoot;
    }

    private void EnemyShootProjectile_EnemyOnShoot(object sender, Enemy_controller.EnemyOnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfEnemyBullet, e.saida, Quaternion.identity);
        Vector3 shootDir = (e.shootPosition - e.saida).normalized;
        bulletTransform.GetComponent<EnemyBullet>().Setup(shootDir);
    }

}
