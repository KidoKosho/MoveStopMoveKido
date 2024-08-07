using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] private List<Enemy> characterList = new List<Enemy>();
    public void Remove(Enemy character)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i] == character) { characterList.RemoveAt(i);break; }
        }
    }
    public void Add(Vector3 enemytransform,int i,int level,ArrowUI arrowPrefab, Canvas canvas)
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, enemytransform, Quaternion.identity);
        if (enemy.Arrow == null)
        {
            ArrowUI arrow = Instantiate(arrowPrefab, canvas.transform);
            arrow.gameObject.SetActive(false);
            enemy.Changearrow(arrow);
        }
        characterList.Add(enemy);
        LevelManager.Ins.Add(enemy);
        if (level != 1) level = (level + Random.Range(-3, 3));
        level = level >= 1 ? level : 1;
        if(i != 0) enemy.OnInt1(level);
        else enemy.OnInt2(level);
    }
    public void Clear()
    {
        for (int i = characterList.Count-1; i >=0; i--)
        {
            characterList[i].ChangeState(null);
            characterList.RemoveAt(i);

        }
    }
}
