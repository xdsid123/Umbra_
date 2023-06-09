using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Weapon weaponParent;
    public float damage;
    public BoxCollider2D boxCollider;
    public float kbMagnitude;

    private Vector2 _rightOffset;

    private void Start() 
    {
        damage = weaponParent.damage;
        kbMagnitude = weaponParent.knockBack;
        _rightOffset = transform.localPosition;
    }

    public void AttackRight()
    {
        boxCollider.enabled = true;
        transform.localPosition = _rightOffset;
    }

    public void AttackLeft() 
    {
        boxCollider.enabled = true;
        transform.localPosition = new Vector2(-_rightOffset.x, _rightOffset.y);
    }

    public void StopAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Mob" || other.tag == "Player")
        {
            
            IDamageHandler mob = other.GetComponent<IDamageHandler>();

            if (mob != null)
            {
                Vector3 playerPos = gameObject.GetComponentInParent<Transform>().position;

                Vector2 direction = (Vector2) (other.transform.position - playerPos).normalized;
                Vector2 kb = direction * kbMagnitude;
            
                mob.TakeDamage(damage, kb);
            }
            else
                print("Other doesn't implement IMobDamageHandler");
        }
    }
}
