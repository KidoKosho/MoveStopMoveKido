using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private Character charater;
    public float bulletSpeed = 5f;
    public void OnInit(Character character, Vector3 bullet, float r,Vector3 attacktranform)// ban vien dan
    {
        rb.velocity = Vector3.zero;
        this.charater = character;
        transform.localScale = Vector3.one * character.Increaseswithlevel / 2;
        rb.velocity = bullet * bulletSpeed * character.Increaseswithlevel;
        float timespawn = r / (bulletSpeed * character.Increaseswithlevel);
        Invoke(nameof(OnDestroyBullet), timespawn);// se huy trong timespawn
    }
    public void OnDestroyBullet()
    {
        gameObject.SetActive(false);
        Invoke(nameof(CharacterEndAttack), 0.25f);  
    }
    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.Character(other);
        if (character == null) return;
        this.charater.RemoveCharacter(character);
        if (!character.IsDead&&this.charater != character)//check xem co bi ai giet truoc chia va check xem co phai đạn là do chính nó ném vào mình không
        {
            this.charater.ChangeLevelCharacter(character.LevelCharacter);
            character.OnDeath();
            if (IsInvoking(nameof(OnDestroyBullet))) { 
               CancelInvoke(nameof(OnDestroyBullet));
            }
            OnDestroyBullet();
        }
    }
    public void CharacterEndAttack()
    {
        charater.IsActiveWeap(true);
        charater.ChangeisAttack(false);
        charater.RemoveBullet(this);
        charater.ChangeIsAttacking(false);
        if (gameObject != null) Destroy(gameObject);
    }
    public void OnDespawn()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
