using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;



    
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            bullet.GetComponent<Bullet>().Weapon = this;
        }

    }
    
}
