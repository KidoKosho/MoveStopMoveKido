using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControll : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;
    private void Awake()
    {
        //GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/");
        //Debug.Log(gameUnits.Length);
        //for (int i = 0; i < gameUnits.Length; i++)
        //{
        //    SimplePool.PreLoad(gameUnits[i], 0, new GameObject(gameUnits[i].name).transform);
        //}
        for (int i = 0; i < poolAmounts.Length; ++i)
        {
            SimplePool.PreLoad(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }
}
[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
public enum PoolType
{
    None = 0,
    Enemy = 1,
}
