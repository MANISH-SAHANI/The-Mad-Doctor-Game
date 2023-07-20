using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyDamageArea : MonoBehaviour {


    [SerializeField]
    private float deactivateWaitTime = 0.1f;
    private float deactivateTimer;

    [SerializeField]
    private LayerMask playerLayer;

    private bool canDealDamage;

    [SerializeField] private float damageAmount = 5f;

    //Player Health
    private PlayerHealth playerHealth;

    private void Awake() {
        gameObject.SetActive(false);  
        playerHealth = GameObject.FindWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerHealth>();
    }

    private void Update() {
        if (Physics2D.OverlapCircle(transform.position,1f,playerLayer)) {
            if(canDealDamage) {
                canDealDamage = false;
                // Deal damage tto playerr
                playerHealth.TakeDamage(damageAmount);
            }
        }
        DeactivateDamageArea();
    }
    
    void DeactivateDamageArea() {
        if(Time.time > deactivateTimer) {
            gameObject.SetActive (false);
        }
    }

    public void ResetDeactivateTimer() {
        canDealDamage = true;
        deactivateTimer = Time.time + deactivateWaitTime;
    }

}
