using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, IEntity, IHealth 
{
    public float health {get; set; }
    public float remainingHealth {get; set;}
    public Queue<StatusEffect> statusEffects { get; set; }

    public float movementSpeed;
    public float attackSpeed;
    public float attackRange;
    public float damage;

    void Start(){
        remainingHealth = health;
        isSelected = false;
    }

    [HideInInspector]
    public bool isSelected;

    public void Die()
    {
        if(remainingHealth == 0)
        {
            gameObject.SetActive(false);
            Debug.Log("Hero has died");
        }
    }

    public void TakeDamage(float damage)
    {
        remainingHealth -= damage;
    }

    public void Move(Vector3 Point){}
    public void Reposition(){}
    public void Patrol(Vector3 PointA, Vector3 PointB){}
    public void Attack(Vector3 Point){
        //STATES
    }
    public void MeleeAttack(){}
    public void Shoot(){}
    public void Aim(){}
    public void Heal(){}

}
