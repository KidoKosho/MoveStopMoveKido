using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : CharacterSO
{
    [SerializeField] Transform characterTransform;
    [SerializeField] SightCharacter sightCharacter;
    [SerializeField] Transform weapontransform;
    [SerializeField] private Animator anim;
    [SerializeField] Transform HandCharacter;
    [SerializeField] Transform HeadCharacter;
    [SerializeField] Renderer pantrenderer;
    [SerializeField] private CanvasLevel canvasLevelPrefabs;
    [SerializeField] private ArrowUI arrow;
    private CanvasLevel canvasLevel;
    private bool isDead = false;
    private bool isAttacking = true;
    public Weapon currentWeapon;
    private GameObject currentHair;
    private string currentAnimName;
    private bool isAttack = false;
    private List<Character> charactersList = new List<Character>();
    private int levelCharacter = 1;
    private Character CharacterWeap;
    public int LevelCharacter => levelCharacter;
    public float Increaseswithlevel => (LevelCharacter <=15 ? LevelCharacter : 15) * 0.05f + 0.95f;
    public Transform CharacterTransform => characterTransform;
    public ArrowUI Arrow => arrow;
    public List<Character> CharacterList => charactersList;
    public SightCharacter SightCharacter => sightCharacter;
    public int t => CharacterList.Count;
    public bool IsAttack => isAttack;
    public bool IsDead => isDead;
    public Vector3 weaponposition => weapontransform.position;

    public virtual void OnInit()
    {
        ChangeisAttack(false);
        ChangeWeap(Weapon);
        ChangeDeath(false);
        ChangeColor(color);
        ChangeHair(hair);
        ChangePant(pant);
        isAttacking = false;
        if (canvasLevel == null) canvasLevel = Instantiate(canvasLevelPrefabs);
        canvasLevel.OnInit(this.transform,color,levelCharacter,nameShopSO.GetString(indexname));
        canvasLevel.gameObject.SetActive(true);
        OnLoadLevelCharacter();
        arrow.OnInit(color, levelCharacter);
    } 
    public void AddCharacter(Character character) // them nhan vat vao list
    {
        if(character != null) charactersList.Add(character);
    }
    public void RemoveCharacter(Character character) //xoa nhan vat trong list
    {
        for(int i  = charactersList.Count - 1; i >= 0; i--)
        {
            if(character == charactersList[i])
            {
                charactersList.RemoveAt(i);
                return;
            }
        }
    }
    public void ClearCharacter() // xoa het nhan vet
    {
        for (int i = charactersList.Count - 1; i >= 0; i--)
        {
            charactersList.RemoveAt(i);
        }
        charactersList.Clear();
    }
    public void ChangeLevelCharacter(int level) // nang level
    {
        if(this.levelCharacter >= level)
        {
            this.levelCharacter += 1;
        }
        else if( this.levelCharacter < level + 3)
        {
            this.levelCharacter += 2;
        }
        else
        {
            this.levelCharacter += 3;
        }
        OnLoadLevelCharacter();
    }
    public void OnLoadLevelCharacter() // load level
    {
        CharacterTransform.localScale = (Vector3.one* Increaseswithlevel);
        canvasLevel.LoadLevelUI(LevelCharacter);
    }
    public void ChangeAnim(string animName)
    {

        if (currentAnimName != animName)
        {
            if (currentAnimName != null) anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void ChangelevelStart(int level)
    {
        this.levelCharacter = level;
    }
    public void Attack() // tan cong
    {
        float distance = 0f;
        if (IsAttack||isAttacking||isDead) return;
        for (int i=charactersList.Count-1; i>=0;i--)
        {
            if (charactersList[i] == null|| (Vector3.Distance(charactersList[i].CharacterTransform.position, this.characterTransform.position) - charactersList[i].Increaseswithlevel * 0.5f > (SightCharacter.Radius + 0.1f)) || !charactersList[i].gameObject.activeSelf || charactersList[i].IsDead)
            {
                charactersList.RemoveAt(i);
                continue;
            }
            if (charactersList[i].gameObject.activeSelf && (distance > Vector3.Distance(charactersList[i].CharacterTransform.position, this.characterTransform.position) - charactersList[i].Increaseswithlevel * 0.5f || distance < 0.1f))
            {
                isAttack = true;
                CharacterWeap = charactersList[i];
                distance = Vector3.Distance(charactersList[i].CharacterTransform.position, this.characterTransform.position);
            }
        }
        if (IsAttack)
        {
            transform.LookAt(CharacterWeap.transform.position + (characterTransform.position.y - CharacterWeap.transform.position.y) * Vector3.up);
            ChangeAnim(PrefConst.Attack_Anim);
            Invoke(nameof(InsWeap), 0.4f);
        }
    }
    public void ChangeisAttack(bool check) // chuyen doi isAttack
    {
        isAttack = check;
    }
    public void CancelAttack() // huy Tancong
    {
        if (IsAttack)
        {
            CancelInvoke(nameof(InsWeap));
            ChangeisAttack(false);
        }
    }
    public void OnDeath()
    {
        if(!GameManger.Ins.Isplay) return;
        ChangeAnim(PrefConst.Dead_Anim);
        ChangeDeath(true);
        CancelAttack();
        arrow.gameObject.SetActive(false);
        LevelManager.Ins.RemoveListCharacter(this);
        LevelManager.Ins.CharacterDead(this);
        canvasLevel.gameObject.SetActive(false);
        Invoke(nameof(CharacterDeath), 0.75f);
    }
    public void ChangeDeath(bool isdead)
    {
        isDead = isdead;
    }
    public virtual void CharacterDeath()
    {
        //if (this is Enemy) SimplePool.Despawn(this);
        //else gameObject.SetActive(false);
    }
    public void InsWeap()//tao vien dan
    {
        IsActiveWeap(false);
        isAttacking = true;
        transform.LookAt(CharacterWeap.transform.position + (characterTransform.position.y - CharacterWeap.transform.position.y) * Vector3.up);
        Vector3 bullet = (CharacterWeap.transform.position- currentWeapon.transform.position + weaponposition.y * Vector3.up).normalized;//hướng ném mục tiêu 
        currentWeapon.OnInit(this,bullet, SightCharacter.Radius, weaponposition);
    }
    public void ChangeIsAttacking(bool check)// xem dang tan cong khong
    {
        isAttacking=check;
    }
    public void ChangeWeap(WeaponType weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponDataSO.GetWeapon(weapon),HandCharacter);
        currentWeapon.gameObject.SetActive(true);
    }
    public void ChangeHair(HairType hair)
    {
        if(currentHair != null)
        {
            Destroy(currentHair.gameObject);
        }
        currentHair = Instantiate(hairDataSO.GetHair(hair),HeadCharacter);
    }
    public void ChangePant(PantType pant)
    {
        this.pant = pant;
        if (pant != PantType.None) pantrenderer.material = pantDataSO.GetPant(pant);
        else pantrenderer.material = colorDataSO.GetMat(color);
    }
    public void IsActiveWeap(bool check) // bat tat vu khi dang cam
    {
        currentWeapon.gameObject.SetActive(check);  
    }
    public void ChangActiveUILevel()//bat tat UIlevel
    {
       if (canvasLevel.gameObject.activeSelf != GameManger.Ins.Isplay)
       {
          if (isDead) canvasLevel.gameObject.SetActive(false);
          else canvasLevel.gameObject.SetActive(GameManger.Ins.Isplay);
       }
    }
    public void OnDespawn()
    {
        CancelInvoke();
        canvasLevel.gameObject.SetActive(false);
        ChangeAnim(PrefConst.Idle_Anim);
        arrow.gameObject.SetActive(false);
    }
    public void Changearrow(ArrowUI arrow)
    {
        this.arrow = arrow;
    }
}
