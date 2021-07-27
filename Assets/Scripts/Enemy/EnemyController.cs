using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public float attackDelay = 1.0f;
    public NodesController nodesController;
    public Transform finalTarget;
    public float alertRatio;
    public float attackRange = 3.0f;
    public float attackObjRange = 5.0f;
    public bool canDamage = false;
    public Animator anim;

    public enum TargetType
    {
        node,
        objetive,
        player,
        obstacle
    }
    public TargetType targetType;

    private Vector3 _target;
    public Vector3 target
    {
        get => _target;
        private set => _target = value;
    }

    private List<Node> nodes;
    private int currentNode;
    private NavMeshAgent agent;
    private EnemyBaseState currentState;

    public readonly EnemyIdleState idleState = new EnemyIdleState();
    public readonly EnemyGoingToTargetState enemyGoingToTargetState = new EnemyGoingToTargetState();
    public readonly EnemyAttackState enemyAttacktState = new EnemyAttackState();

    private const float customUpdateInterval = 0.25f;

    public EnemyBaseState CurrentState
    {
        get { return currentState; }
    }

    private void Start()
    {
        finalTarget = GameObject.FindGameObjectWithTag("Objetive").transform;
        nodesController = FindObjectOfType<NodesController>();
        anim = GetComponent<Animator>();
        nodes = nodesController.nodes;
        agent = GetComponent<NavMeshAgent>();

        transitionToState(idleState);
        currentNode = 0;

        changeTarget(nodes[currentNode].getPoint());
        InvokeRepeating("customUpdate", customUpdateInterval, customUpdateInterval);
        
    }

    void Update()
    {
        currentState.Update(this);
    }
    
    public void customUpdate()
    {
        currentState.CustomUpdate(this);
    }

    public void transitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void changeTarget(Vector3 newTarget)
    {
        target = newTarget;
        agent.SetDestination(newTarget);
    }

    public void stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void run()
    {
        agent.isStopped = false;
    }

    public void nextTarget()
    {
        if(currentNode < nodes.Count - 1)
        {
            currentNode++;
            changeTarget(nodes[currentNode].getPoint());
            targetType = TargetType.node;
        }
        else
        {
            changeTarget(finalTarget.position);
            targetType = TargetType.objetive;
        }
    }

    public void resumeTarget()
    {
        if (currentNode < nodes.Count - 1)
        {
            changeTarget(nodes[currentNode].getPoint());
            targetType = TargetType.node;
        }
        else
        {
            changeTarget(finalTarget.position);
            targetType = TargetType.objetive;
        }
    }

    public void targetEliminate()
    {
        resumeTarget();
        transitionToState(enemyGoingToTargetState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, alertRatio);
    }
}
