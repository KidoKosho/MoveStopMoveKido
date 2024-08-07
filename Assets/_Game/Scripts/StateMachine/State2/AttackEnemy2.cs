using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy2 : IState
{
     float time = 3f,time_spawn =0f;
    public void OnEnter(Enemy enemy)
    {
        time = Random.Range(1.5f, 3f);
        time_spawn = 0f;
    }
    public void OnExecute(Enemy enemy)
    {
        time_spawn += Time.deltaTime;
        if(!enemy.IsAttack)
        {
            enemy.ChangeAnim(PrefConst.Idle_Anim);
        }
        if (!enemy.IsAttack && (time_spawn > time || enemy.CharacterList.Count >=3))
        {
            enemy.ChangeState(new FindEnemy2());
        }
        else if (!enemy.IsAttack && enemy.CharacterList.Count > 0)
        {
            enemy.Attack();
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
