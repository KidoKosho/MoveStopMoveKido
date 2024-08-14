using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 5f;

    [SerializeField] Transform rayGround;
    [SerializeField] LayerMask groundLayer;
    private void FixedUpdate()
    {
        ChangeActivesight();
        if (IsDead || !GameManger.Ins.Isplay)
        {
            if(IsDead) { rb.velocity = Vector3.zero; }
            return;
        }
        if (Joystick.Ins.Horizontal != 0 || Joystick.Ins.Vertical != 0)
        {

            Vector2 input = new Vector2(Joystick.Ins.Horizontal, Joystick.Ins.Vertical).normalized;
            if (!IsCheckPlatform())
            {
                rb.velocity = Vector3.zero;
            }
            else {
                rb.velocity = new UnityEngine.Vector3(input.x * speed, rb.velocity.y, input.y * speed);
            }
            ChangeAnim(PrefConst.Run_Anim);
            CancelAttack();
            transform.rotation = Quaternion.LookRotation(new UnityEngine.Vector3(Joystick.Ins.Horizontal * speed, 0, Joystick.Ins.Vertical * speed));
        }
        else
        {
            if (!IsAttack)
            {
                ChangeAnim(PrefConst.Idle_Anim);
            }
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            Attack();
        }
    }
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ChangeLevelCharacter(1);
        }
    }
    public override void OnInit()
    {
        Weapon = (WeaponType) Pref.WeaponID;
        hair = (HairType)Pref.HairID;
        pant = (PantType)Pref.PantID;
        indexname = 0;
        ChangelevelStart(1);
        base.OnInit();
    }
    public void ChangeActivesight()//hiện tầm bắn của Player
    {
        if (SightCharacter.gameObject.activeSelf != GameManger.Ins.Isplay)
        {
            SightCharacter.gameObject.SetActive(GameManger.Ins.Isplay);
        }
        ChangActiveUILevel();
    }
    public bool IsCheckPlatform()
    {
        return Physics.Raycast(rayGround.position, UnityEngine.Vector3.down, 5f, groundLayer);
    }

    public override void CharacterDeath()//TODO
    {
        base.CharacterDeath();
        gameObject.SetActive(false);
    }
}
    