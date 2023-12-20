using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    //update lien tuc
    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer > randomTime) //idle trong khoang tgian random sau do doi sang patrol
        {
            enemy.ChangeState(new PatrolState());
        }

    }

    public void OnExit(Enemy enemy)
    {
    }


}
