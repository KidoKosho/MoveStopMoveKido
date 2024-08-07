using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : IState
{
    public void OnEnter(Enemy enemy)
    {
    }
    public void OnExecute(Enemy enemy)
    {
        if(!enemy.IsAttack) enemy.ChangeAnim(PrefConst.Idle_Anim);
        if (enemy.CharacterList.Count < 1&&!enemy.IsAttack)
        {
            enemy.ChangeState(new FindEnemy());
        }
        else if(!enemy.IsAttack)
        {
            enemy.Attack();
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}