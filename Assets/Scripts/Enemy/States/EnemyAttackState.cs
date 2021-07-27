using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    float attackCounter;

    public override void EnterState(EnemyController enemy)
    {
        Debug.Log("Attack State");
        enemy.stop();
        enemy.gameObject.transform.LookAt(enemy.target);
    }

    public override void Update(EnemyController enemy)
    {
        //if (enemy.targetType == EnemyController.TargetType.player && !enemy.isAttacking)
        if (enemy.targetType == EnemyController.TargetType.node)
            return;

        attackCounter += Time.deltaTime;
        if (attackCounter >= enemy.attackDelay)
        {
            enemy.anim.SetTrigger("Attack1");
            enemy.canDamage = true;
            attackCounter = 0f;
        }

        /*if(enemy.targetType != EnemyController.TargetType.node)
        {
            attackCounter += Time.deltaTime;
            if(attackCounter >= enemy.attackDelay)
            {
                enemy.anim.SetTrigger("Attack1");
                enemy.canDamage = true;
                attackCounter = 0f;
            }
        }*/
    }

    public override void CustomUpdate(EnemyController enemy)
    {
        int mask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Obstacle"));
        Collider[] cols = Physics.OverlapSphere(enemy.transform.position, enemy.alertRatio, mask);

        if (cols.Length == 0 && (enemy.targetType == EnemyController.TargetType.player || enemy.targetType == EnemyController.TargetType.obstacle))
        {
            enemy.resumeTarget();
            enemy.transitionToState(enemy.enemyGoingToTargetState);
            return;
        }

        foreach (Collider col in cols)
        {
            if (col.CompareTag("Player") && enemy.targetType == EnemyController.TargetType.player)
            {
                if (Vector3.Distance(enemy.transform.position, col.gameObject.transform.position) > enemy.attackRange)
                {
                    enemy.transitionToState(enemy.enemyGoingToTargetState);
                    return;
                }
            }
        }
    }
}
