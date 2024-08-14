using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] Rigidbody rb;
    private IState currentState;
    [SerializeField] private NavMeshAgent agent;
    private UnityEngine.Vector3 destination;
    public bool IsDestionation => Vector3.Distance(transform.position, destination + (transform.position.y - destination.y) * Vector3.up) < 0.1f;//TODO transform.positsition
    public void OnInt1(int level)
    {
        ChangelevelStart(level);
        ChangeState(new FindEnemy());
        OnInit();
    }
    public void OnInt2(int level)
    {
        ChangelevelStart(level);
        ChangeState(new FindEnemy2());
        OnInit();
    }
    public override void OnInit()
    {
        RandomName();
        RandomMaterial();
        RandomWeapon();
        RandomHair();
        RandomPant();
        base.OnInit();
    }
    private void FixedUpdate()
    {
        ChangActiveUILevel();
        if (IsDead||!GameManger.Ins.Isplay)
        {
            SetDestination(CharacterTransform.position);
            if(!GameManger.Ins.Isplay) ChangeAnim(PrefConst.Idle_Anim);
            return;
        }
        if (currentState == null) return;
        currentState.OnExecute(this);
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void SetDestination(UnityEngine.Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }
    public void SaveSetDestination()
    {

    }

    public override void CharacterDeath()
    {
        base.CharacterDeath();
        SimplePool.Despawn(this);
    }
}
