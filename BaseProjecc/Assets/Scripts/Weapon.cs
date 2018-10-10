using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string hitTag { get; set; }
    public int Damage { get; set; }

    public void SetHitTag(string newHitTag)
    {
        hitTag = newHitTag;
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == hitTag)
        {
            other.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}
