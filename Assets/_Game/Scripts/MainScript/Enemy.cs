using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;

    private IState currentState;
    private bool isRight = true;


    private Character target;
    public Character Target => target;

    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExcute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAtack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    protected override void OnDead()
    {
        ChangeState(null);
        base.OnDead();
    }


    public void ChangeState(IState newState)
    {
        //khi đổi sang state mới, check xem state cũ có = null ko
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("run");

        rb.velocity = transform.right * moveSpeed;

    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAtack();
        Invoke(nameof(DeActiveAtack), 0.5f);
    }

    public bool IsTargerInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up *180);
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargerInRange())
        {
            ChangeState(new AttackState());
        } else if(Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    private void ActiveAtack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAtack()
    {
        attackArea.SetActive(false);
    }
}
