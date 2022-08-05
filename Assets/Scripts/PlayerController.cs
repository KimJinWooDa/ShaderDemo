using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    MoveMent moveMent;
    [SerializeField] CameraController cameraController;
    PlayerAnimation playerAnimation;
    ShootController shootController;


    private float shootCoolTime = 0.3f;
    public bool canShot;
    public bool canSwing = true;
    [SerializeField] Transform[] weaponTR;
    private void Awake()
    {
        moveMent = GetComponent<MoveMent>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        moveMent.ToMove(new Vector3(dirX, 0, dirY));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveMent.ToJump();
        }

        float cameraX = Input.GetAxis("Mouse X");
        float cameraY = Input.GetAxis("Mouse Y");

        cameraController.ToRatate(cameraX, cameraY);
        playerAnimation.RunAnimation(new Vector3(dirX, 0, dirY));

        if (weaponTR[0].gameObject.activeSelf)
        {
            Debug.Log("0");
            if (Input.GetMouseButtonDown(1))
            {
                playerAnimation.ShootingAnimation(true);
                canShot = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                playerAnimation.ShootingAnimation(false);
                canShot = false;
            }
        }

        if (weaponTR[1].gameObject.activeSelf && canSwing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canSwing = false;
                playerAnimation.SwingBatAnimation();

                StartCoroutine(WeaponCoolTime());
            }
        }


       
        if (canShot)
        {
            if (weaponTR[0].gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    canShot = false;
                    shootController.Shooting();
                    //cameraController.Shake();
                    //총소리도 나면 좋을듯?

                    StartCoroutine(WeaponCoolTime());
                }
            }
        } 
    }

    IEnumerator WeaponCoolTime()
    {
        yield return new WaitForSeconds(0.3f);
        canShot = true;
        canSwing = true;
    }

  
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GUN"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                weaponTR[1].gameObject.SetActive(false);
                other.GetComponent<ShootController>().DestroyItem();
                weaponTR[0].gameObject.SetActive(true);
                shootController = weaponTR[0].GetComponent<ShootController>();
            }
        }
        if (other.CompareTag("BAT"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                weaponTR[0].gameObject.SetActive(false);
                //other.GetComponent<ShootController>().SetGunPos();
                weaponTR[1].gameObject.SetActive(true);
            }
        }
    }
}
