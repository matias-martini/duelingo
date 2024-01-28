using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController0 : MonoBehaviour
{
    public GameObject Sword, Axe, Spear;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }
    }
    public void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }

    IEnumerator ResetAttackCooldown()
    { 
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}

