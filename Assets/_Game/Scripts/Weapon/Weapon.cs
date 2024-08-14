using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] List<AttackWeapon> attackWeapons = new List<AttackWeapon>();
    [SerializeField] AttackWeapon AttackWeapon;
    public int CountAttackWeapon => attackWeapons.Count;
    public void OnInit(Character character, Vector3 bullet, float r, Vector3 attacktranform)
    {
        AttackWeapon attackWeapon = Instantiate(AttackWeapon,transform.position, Quaternion.LookRotation(bullet));//tao ra vien dan
        attackWeapon.Shoot(character, bullet, r, attacktranform,this);
        AddBullet(attackWeapon);
    }
    public void AddBullet(AttackWeapon attackWeapon)
    {
        attackWeapons.Add(attackWeapon);
    }
    public void Ondespawn()
    {
        ClearBullet();
    }
    public void ClearBullet() 
    {
        List<AttackWeapon> attackWeapons = this.attackWeapons;
        for (int i = attackWeapons.Count - 1; i >= 0; i--)
        {
            if (attackWeapons[i].gameObject) attackWeapons[i].OnDespawn();
            attackWeapons.RemoveAt(i);
        }
        attackWeapons.Clear();
    }
    public void RemoveBullet(AttackWeapon weapon)
    {
        attackWeapons.Remove(weapon);
    }
}
