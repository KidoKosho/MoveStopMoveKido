using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    //khoi tao Pool moi
    public static void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.Log("PREFAB IS EMPTY!!");
            return;
        }
        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(prefab, amount, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }
    //lay phan tu ra
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!!");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    // tra phan tu vao
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + "IS NOT PRELOAD!!!");
            return;
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }
    //thu thap phan tu poolType
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!!");
            return;
        }
        poolInstance[poolType].Collect();
    }
    //thu thap tat ca
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }
    // destroy 1 pool
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!!");
            return;
        }
        poolInstance[poolType].Release();
    }
    //destroy tat ca pool
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }

}
public class Pool
{
    Transform parent;
    GameUnit prefab;
    //chua cac Unit dang duoc su dung o trong Pool
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    //List chua cac unit dang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();
    //khoi tao Pool
    public void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;
        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab,parent));
        }
    }
    //Lay phan tu tu pool
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);//loaddd
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }
    //tra phan tu vao trong pool
    public void Despawn(GameUnit unit)
    {
        if (unit != null&&unit.gameObject.activeSelf)
        {
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
        actives.Remove(unit);
    }
    //thu thap cac phan tu dang dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }
    // destroy tat ca phan tu
    public void Release()
    {
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}
