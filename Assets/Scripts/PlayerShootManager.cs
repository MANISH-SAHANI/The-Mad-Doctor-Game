using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour {

    [SerializeField] private GameObject bulletsprefab;
    [SerializeField] private Transform bulletSpawnPos;

    public void Shoot(float facingDirection) {

        GameObject newBullet = Instantiate(bulletsprefab, bulletSpawnPos.position, Quaternion.identity);

        if(facingDirection < 0) {
            newBullet.GetComponent<Bullets>().SetNeagativeSpeed();

        }

        SoundManager.instance.PlayShootSound();

    }

}
