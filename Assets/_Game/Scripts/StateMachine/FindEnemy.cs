using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FindEnemy : IState
{
    Vector3 dis;
    public void OnEnter(Enemy enemy)
    {
        Find(enemy);
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.ChangeAnim(PrefConst.Run_Anim);
        if (enemy.CharacterList.Count > 0&&!enemy.IsAttack)
        {
            enemy.SetDestination(enemy.transform.position);
            enemy.ChangeState(new AttackEnemy());
        }
        else if(enemy.IsDestionation&&!enemy.IsAttack) 
        {
            Find(enemy);
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
    public void Find(Enemy enemy)
    {
        dis = LevelManager.Ins.FindCharacter(enemy);
        enemy.SetDestination(dis);
    }
}
