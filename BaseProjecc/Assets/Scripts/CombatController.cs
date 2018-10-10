using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public int Damage;

    Weapon wep;

	// Use this for initialization
	void Start ()
    {
        wep = GetComponentInChildren<Weapon>();
        wep.SetHitTag("Enemy");
        wep.SetDamage(Damage);
	}
	
}
