using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour, IAIController {

    public int startHealth;

    GameObject player;

    bool inCombat = false;

    void EnableCombat()
    {
        //player.GetComponent<PlayerMovement>().ToggleInCombat();
        //player.GetComponent<PlayerMovement>().SetTarget(gameObject);
        inCombat = true;

        Debug.Log("Enabled");
    }


    public void ToggleCombat()
    {
        inCombat = !inCombat;
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        Health health = GetComponent<Health>();
        health.Initialise(startHealth, this);
	}
	
	// Update is called once per frame
	void Update ()
    {

        float dist = Vector3.Distance(player.transform.position, transform.position);
		if(dist < 10 && !inCombat)
        {
            EnableCombat();
            player.GetComponent<PlayerMovement>().enemies.Add(this.gameObject);
        }else if(dist > 10 && inCombat)
        {
            //player.GetComponent<PlayerMovement>().TakeFromEnemies(gameObject);
        }
	}

    void OnDestroy()
    {
        if (player != null) { player.GetComponent<PlayerMovement>().BreakFromCombat(); } 
    }

    public void OnDeath()
    {
        player.GetComponent<PlayerMovement>().TakeFromEnemies(gameObject);
        Destroy(this.gameObject);
    }
}
