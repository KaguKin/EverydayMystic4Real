using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public int Damage;

    Weapon wep;

    PlayerMovement PM;

    bool canAttack = true;

    bool inCombat = false;

    Animator myAnim;

    public List<GameObject> enemies;

    float timeSinceAttack;

    // Use this for initialization
    void Start ()
    {
        wep = GetComponentInChildren<Weapon>();
        wep.SetHitTag("Enemy");
        wep.SetDamage(Damage);

        PM = GetComponent<PlayerMovement>();
        myAnim = GetComponent<Animator>();
	}

    private void Update()
    {
        if (inCombat)
        {

            AnimatorClipInfo[] clipInfo = myAnim.GetCurrentAnimatorClipInfo(0);
            //Check for a case to break away from being in combat state.
            if (CheckForBreakFromCombat()) { BreakFromCombat(); }

            if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
            {
                myAnim.SetTrigger("attack");
                canAttack = false;
                timeSinceAttack = 0;
            }
            if(Input.GetKeyDown(KeyCode.X) && canAttack)
            {
                myAnim.Play("Special_AOE");
            }
        }
        CheckForNewTarget();

        timeSinceAttack += Time.deltaTime;
        myAnim.SetFloat("timeSinceAttack", timeSinceAttack);
    }

    public void SetInCombat(bool value)
    {
        inCombat = value;
    }

    public bool InCombat()
    {
        return inCombat;
    }

    public void ToggleInCombat()
    {
        inCombat = !inCombat;
    }


    bool CheckForBreakFromCombat()
    {
        if (PM.GetTargetEnemy() == null) { return true; }
        float dist = Vector3.Distance(transform.position, PM.GetTargetEnemy().transform.position);
        if (dist > 10) { return true; }
        else { return false; }
    }

    void CheckForNewTarget()
    {
        GameObject currTarget = null;
        float minDist = 0;

        if (enemies.Count <= 0)
        {
            inCombat = false;
        }
        else
        {
            inCombat = true;
            for (int i = 0; i < enemies.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (dist < minDist || minDist == 0)
                {
                    currTarget = enemies[i];
                    minDist = dist;
                }
            }
            PM.SetTargetEnemy(currTarget);
        }
    }

    public void BreakFromCombat()
    {
        inCombat = false;

        if (PM.GetTargetEnemy() == null) { return; }
        PM.GetTargetEnemy().GetComponent<CrabController>().ToggleCombat();
        PM.SetTargetEnemy(null);
    }

    public void ResetCanAttack()
    {
        canAttack = true;
    }

    public void AddToEnemies(GameObject newEnemy)
    {
        if (!enemies.Contains(newEnemy))
        {
            enemies.Add(newEnemy);
        }
    }

    public void TakeFromEnemies(GameObject enemyToRemove)
    {
        if (enemies.Contains(enemyToRemove))
        {
            enemies.Remove(enemyToRemove);
        }

    }
}
