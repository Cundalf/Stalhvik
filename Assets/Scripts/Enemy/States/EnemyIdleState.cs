using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public override void EnterState(EnemyController enemy)
    {

    }

    public override void Update(EnemyController enemy)
    {
    }

    public override void CustomUpdate(EnemyController enemy)
    {
        Collider[] cols = Physics.OverlapSphere(enemy.transform.position, enemy.alertRatio);

        foreach (Collider col in cols)
        {
            if (col.CompareTag("Player"))
            {
                enemy.changeTarget(col.gameObject.transform.position);
            }
        }

        if (enemy.target != null)
        {
            enemy.transitionToState(enemy.enemyGoingToTargetState);
        }
    }
}
