using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoingToTargetState : EnemyBaseState
{

    public override void EnterState(EnemyController enemy)
    {
        enemy.anim.SetBool("Run", true);
        enemy.run();
    }

    public override void Update(EnemyController enemy)
    {
        
    }

    public override void CustomUpdate(EnemyController enemy)
    {
        int mask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Obstacle"));
        Collider[] cols = Physics.OverlapSphere(enemy.transform.position, enemy.alertRatio, mask);
        
        if(cols.Length == 0 && (enemy.targetType == EnemyController.TargetType.player || enemy.targetType == EnemyController.TargetType.obstacle))
        {
            enemy.resumeTarget();
            return;
        }

        foreach (Collider col in cols)
        {
            if(col.CompareTag("Player"))
            {
                enemy.targetType = EnemyController.TargetType.player;
                enemy.changeTarget(col.gameObject.transform.position);
                break;
            }

            if (col.CompareTag("Defense"))
            {
                enemy.targetType = EnemyController.TargetType.obstacle;
                enemy.changeTarget(col.gameObject.transform.position);
                break;
            }
        }

        

        if (enemy.targetType != EnemyController.TargetType.objetive)
        {
            if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < enemy.attackRange)
            {
                switch (enemy.targetType)
                {
                    case EnemyController.TargetType.obstacle:
                    case EnemyController.TargetType.player:
                        enemy.anim.SetBool("Run", false);
                        enemy.transitionToState(enemy.enemyAttacktState);
                        break;
                    default:
                        Debug.Log("Cambiar");
                        enemy.nextTarget();
                        break;
                }
            }

        }
        else
        {
            if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < enemy.attackObjRange)
            {
                enemy.anim.SetBool("Run", false);
                enemy.transitionToState(enemy.enemyAttacktState);
            }
        }
    }
}
