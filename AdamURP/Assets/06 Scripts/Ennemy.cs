using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public Animator animator;
    public float health = 3f;



    public int weapontype = 1;
    public float movementSpeed = 5f;
    public float detectionRange = 5f;
    public float attackRange = 3f;
    public float rateOfAttack;
    public bool canAttack = true;

    //glorykill
    public float glorykilllife = 1f;
    public float livegivedback = 10f;
    public bool opennedtoglorykill = false;
    public float timetodieafterstun = 3;
    private float timer;

    public GameObject projectile;
    public GameObject target;
    public Transform canonTransform;
    public enum EnnemyType { range, melee };
    public EnnemyType ennemyType;


    private void Start()
    {
        FindTarget();
        timer = timetodieafterstun;
    }
    private void Update()
    {
        if (!opennedtoglorykill)
        {
            IsInDetectionRange();
            ChasePlayer();
            AttackPlayer();
            AimLeftRight();
        }
        else
        {
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
               
                print("too long,did not survive!");

            }
        }






  
    }

    //V1 
    //quand player rentre en detection range 
    //chase jusqu'à être en attaque range
    //si ennemy type = melee alors fais le truc de melee
    //sinon fais le truc de range

    // ****************V2************
    //quand player rentre en detection range 
    //chase jusqu'à être en attaque range
    //tirer un projectile et s'arrêter un petit temps
    //reprendre le chase

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            animator.SetTrigger("die");
           // Die(); Die est maintenant dans l'animator
            
        }
        else if (health <= glorykilllife)
        {
            opennedtoglorykill = true;
            animator.SetTrigger("stun");
        }
        else
        {
            int rnganim =1;
            rnganim = Random.Range(1, 3);
            switch (rnganim)
            {
                case 1:
                    animator.SetInteger("hitnum", 1);
                    break;
                case 2:
                    animator.SetInteger("hitnum", 2);
                    break;
                case 3:
                    animator.SetInteger("hitnum", 3);
                    break;
            }
            animator.SetTrigger("hit");



        }
    }

    public void Die()
    {

        //death particles
        //death sound effects
        Destroy(this.gameObject);
    }

    public void FindTarget()
    {
        target = GameObject.FindObjectOfType<Player>().gameObject;
    }

    public void AimLeftRight()
    {
        if(target.transform.position.x - this.transform.position.x <= 0)
        {
            canonTransform.eulerAngles = new Vector3(0f,180f,0f);
            canonTransform.localPosition = new Vector3(-1f, 0.3f, 0f);
        }
        else
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(1f, 0.3f, 0f);
        }
    }

    public bool IsInDetectionRange()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, this.transform.position);

            if (dist <= detectionRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }

    public void ChasePlayer()
    {
        if(IsInDetectionRange() && IsInAttackRange() == false)
        {
            Vector3 direction = (target.transform.position - this.transform.position).normalized;
            direction.y = 0f;
            transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
    }

    public void AttackPlayer()
    {
        if(IsInAttackRange())
        {
            if(ennemyType == EnnemyType.melee)
            {

            }

            else if(ennemyType == EnnemyType.range && canAttack == true)
            {
                StopCoroutine("RangeAttack");
                StartCoroutine("RangeAttack");
            }
        }
    }

    IEnumerator RangeAttack()
    {
        canAttack = false;
        GameObject go = Instantiate(projectile, canonTransform);
        go.transform.SetParent(null);
        Destroy(go, 6f);
        yield return new WaitForSeconds(rateOfAttack);
        canAttack = true;
    }

    public bool IsInAttackRange()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, this.transform.position);

            if (dist <= attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
      
    }

    private void OnDrawGizmos()
    {
        DrawCircleGizmo(Color.white, detectionRange);
        DrawCircleGizmo(Color.red, attackRange);
    }

    void DrawCircleGizmo(Color gizmoColor, float radius)
    {
        Gizmos.color = gizmoColor;
        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, y, 0);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
   public void Glorykill()
    {
        //TO DO teleporter le joueur sur la postion de l'ennemie ->lancer les animations ->depop ennemie ->change item 
        Debug.Log("Glorykill");
        GameObject[] playerGO;
        playerGO = GameObject.FindGameObjectsWithTag("Player");
        Player player;
       player = playerGO[0].GetComponent<Player>();

        player.Heal(livegivedback);
        player.weapontype = weapontype;
    


    }

}
