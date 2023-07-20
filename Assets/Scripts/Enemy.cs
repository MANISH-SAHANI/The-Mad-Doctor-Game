using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform playerTarget;

    [SerializeField] private float moveSpeed = 2f;

    private Vector3 tempScale;

    [SerializeField] private float stoppingDistance;

    private PlayerAnimation enemyAnimation;

    [SerializeField] private float attackWaitTime = 2.5f;
    private float attackTimer;

    [SerializeField] private float attackFinishedWaitTime = 0.5f;
    private float attackFinishedTimer;

    [SerializeField] private EmemyDamageArea ememyDamageArea;

    private bool enemyDied;

    [SerializeField] private float deathAniTime;

    [SerializeField] RectTransform healthBarTransform;
    private Vector3 healthBarTempScale;

    private void Awake() {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;

        enemyAnimation = GetComponent<PlayerAnimation>();   
    }

    private void Update() {

        if (enemyDied) {
            return;
        }
        SearchForPlayer();
    }

    void SearchForPlayer() {
        if (!playerTarget) {
            return;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance) {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);

            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);

            HandleFacingDirection();
        }
        else {
            CheckIfAttackFinished();
            Attack();
        }
      
    }

    void HandleFacingDirection() {

        tempScale = transform.localScale;

        if (transform.position.x > playerTarget.position.x) {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;

        //Health Bar Direction Facing
        healthBarTempScale = healthBarTransform.localScale;

        if(transform.localScale.x > 0f) {
            healthBarTempScale.x = Mathf.Abs(healthBarTempScale.x);
        }
        else {
            healthBarTempScale.x = -Mathf.Abs(healthBarTempScale.x);
        }

        healthBarTransform.localScale = healthBarTempScale;
    }

    void CheckIfAttackFinished() {
        if(Time.time > attackFinishedTimer) {
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

    void Attack() {

        if(Time.time > attackTimer) {

            attackFinishedTimer = Time.time + attackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);
        }
    }

    void EnemyAttack() {
        ememyDamageArea.gameObject.SetActive(true);
        ememyDamageArea.ResetDeactivateTimer();
    }

    public void EnemyDied() {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay",deathAniTime);
    }

    void DestroyEnemyAfterDelay() {
        Destroy(gameObject);
    }

}
