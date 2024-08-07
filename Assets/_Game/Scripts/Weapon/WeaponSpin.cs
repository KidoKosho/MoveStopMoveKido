using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpin: AttackWeapon
{
    [SerializeField] private Transform m_transform;
    [SerializeField] private float SpeedSpin = 20f;
    private void Update()
    {
        m_transform.rotation *= Quaternion.Euler(0, 45 * Time.deltaTime * SpeedSpin, 0);
    }
}
