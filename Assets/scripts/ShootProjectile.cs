using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    void Start()
    {
        GetComponent<Player>().OnShoot += PlayerShootProjectile_OnShoot;
            
    }

    private void PlayerShootProjectile_OnShoot(object sender, Player.OnShootEventArgs e)
    {
        Transform bulletTransform =Instantiate(pfBullet, e.saida, Quaternion.identity);
        Vector3 shootDir = (e.shootPosition - e.saida).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
}
