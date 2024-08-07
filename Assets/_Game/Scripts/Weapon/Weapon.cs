using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] AttackWeapon AttackWeapon;
    public void OnInit(Character character, Vector3 bullet, float r, Vector3 attacktranform)
    {
        AttackWeapon attackWeapon = Instantiate(AttackWeapon, attacktranform, Quaternion.LookRotation(bullet));//tao ra vien dan
        attackWeapon.OnInit(character, bullet, r, attacktranform);
        character.AddBullet(attackWeapon);
    }
}
