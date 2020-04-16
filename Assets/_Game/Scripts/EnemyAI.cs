using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private float _distanceToTarget;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _isProvoked;
    private EnemyState _enemyState = EnemyState.Idle;
    [SerializeField] private float targetSenseRange = 10f;
    [SerializeField] private Transform target;

    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        ShotAt
    }

    // messages
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(transform.position, target.position); 
        
        switch (_enemyState)
        {
            case EnemyState.Idle:
                PerformIdleBehavior();
                break;
            case EnemyState.Move:
                PerformMoveBehavior();
                break;
            case EnemyState.Attack:
                PerformAttackBehavior();
                break;
            case EnemyState.ShotAt:
                PerformShotAtBehavior();
                break;
            default:
                Debug.Log(_enemyState + " state does not exist");
                throw new ArgumentOutOfRangeException();
        } 
    }

    // methods
    private void PerformIdleBehavior()
    {
        _animator.SetTrigger("IdleTrigger");
        if (_distanceToTarget < targetSenseRange) // detect if target within sensing range
        {
            _enemyState = EnemyState.Move;
            _animator.ResetTrigger("IdleTrigger");
        }
    }

    private void PerformMoveBehavior()
    {
        _animator.SetTrigger("MoveTrigger");
        if (_distanceToTarget > _navMeshAgent.stoppingDistance && _distanceToTarget < targetSenseRange) // if target not within stopping distance, move towards target
        {
            _navMeshAgent.SetDestination(target.position);
        }
        else if (_distanceToTarget <= _navMeshAgent.stoppingDistance) // if target within stopping distance attack
        {
            _enemyState = EnemyState.Attack;
            _animator.ResetTrigger("MoveTrigger");
        }
        else if (_distanceToTarget > targetSenseRange) // if target beyond sensing range, goto idle state
        {
            _enemyState = EnemyState.Idle;
            _animator.ResetTrigger("MoveTrigger");
        }
    }

    private void PerformAttackBehavior()
    {
        _animator.SetBool("AttackBool", true);
        var rotVec = Quaternion.LookRotation((target.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, 10 * Time.deltaTime);
        
        if (_distanceToTarget > _navMeshAgent.stoppingDistance) // is target no longer close enough?
        {
            _enemyState = EnemyState.Move;
            _animator.SetBool("AttackBool", false);
        }
        
    }

    private void PerformShotAtBehavior()
    {
        if (_distanceToTarget > _navMeshAgent.stoppingDistance) // if target not within stopping distance, move towards target
        {
            _navMeshAgent.SetDestination(target.position);
        }
        else
        {
            _enemyState = EnemyState.Attack;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetSenseRange);
    }
    
    // API
    public void GotHitByBullet()
    {
        if (_enemyState != EnemyState.Attack)
        {
            _enemyState = EnemyState.ShotAt;
        }
    }
}
