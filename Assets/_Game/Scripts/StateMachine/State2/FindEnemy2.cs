using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindEnemy2 : IState
{
    float time = 5f,timeout = 0f;
    public void OnEnter(Enemy enemy)
    {
        timeout = 0f;
        enemy.ChangeAnim(PrefConst.Run_Anim);
        float x = Random.Range(-LevelManager.Ins.length/2, LevelManager.Ins.length/2);
        float z = Random.Range(-LevelManager.Ins.width / 2, LevelManager.Ins.width/2);
        time = Random.Range(3f,6f);
        enemy.SetDestination(new Vector3(x, 0, z));
    }
    public void OnExecute(Enemy enemy)
    {
        if(!GameManger.Ins.Isplay) return;  
        if ((enemy.IsDestionation||timeout >= time)&&!enemy.IsAttack)
        {
            if (timeout >= time)
            {
                timeout = 0f;
                enemy.SetDestination(enemy.CharacterTransform.position);
            }
            enemy.ChangeState(new AttackEnemy2());
        }
        timeout += Time.deltaTime;
    }
    public void OnExit(Enemy enemy)
    {
    }
}
