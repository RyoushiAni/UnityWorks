using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
        public Transform bulletSpawnPoint;
        public GameObject BulletPrefab;
        public float BulletSpeed = 10;
        void FixedUpdate(){
            if(Input.GetButtonDown("Fire1") && Time.timeScale == 1){
                 var bullet = Instantiate(BulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                 bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * BulletSpeed;
            }
        }
}
