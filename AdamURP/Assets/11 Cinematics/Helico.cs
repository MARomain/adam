using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Helico : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float rangeBombs;
    public bool AIOn = false;
    public float hauteurHeli = 3f;
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    public float timeBetweenBombs = 0.5f;
    public int bombPerWave = 3;
    public float timeBetweenBombWaves = 3f;
    public int currentNumberOfWaves = 0;
    public int totalNumberOfWaves = 3;
    public bool doOnce = true;

    public int counter = 1;

    public Transform exitTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(AIOn == true)
        {
            ChasePlayer();
            BombPlayer();
        }

        if(currentNumberOfWaves == totalNumberOfWaves)
        {
            AIOn = false;
            ExitScreen();
        }


    }

    void ExitScreen()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, exitTransform.position, step);
    }

    void ChasePlayer()
    {
        float step = speed * Time.deltaTime;
        Vector3 heliVector = new Vector3(transform.position.x, hauteurHeli, transform.position.z);

        //ligne du dessous c de la bricole pour eviter que l'heli se dirige vers le joueurs
        Vector3 targetVector = new Vector3(target.transform.position.x, hauteurHeli, target.transform.position.z);

        transform.position = Vector3.MoveTowards(heliVector, targetVector, step);
    }


    void BombPlayer()
    {
        if (transform.position.x - target.transform.position.x < rangeBombs)
        {
            if(doOnce == true)
            {
                doOnce = false;
                StartCoroutine("SpawnBombs");
            }
            
        }

        

        
        
    }

    //Se lance qu'après une vague de bombe
    IEnumerator ResetBombing()
    {
        yield return new WaitForSeconds(timeBetweenBombWaves);
        counter = 1;
        doOnce = true;
        StopCoroutine("ResetBombing");

        //c'est pas là qu'il faut incrémenter ça se fait trop tard mais il est 0h18 fuck
        currentNumberOfWaves++;

    }


    //void BombPlayer()
    //{
    //    bool doOnce = false;
    //    if (transform.position.x - target.transform.position.x < rangeBombs)
    //    {
    //        doOnce = true;
    //        if(doOnce == true)
    //        {
    //            InvokeRepeating("LaunchBomb", 0f, timeBetweenBombs);
    //            doOnce = false;
    //        }
    //    }

    //    if(doOnce == false)
    //    {
    //        timeBetweenBombWavesTemp -= Time.deltaTime;
    //        if(timeBetweenBombWavesTemp <= 0f)
    //        {
    //            doOnce = true;
    //            timeBetweenBombWavesTemp = timeBetweenBombWaves;
    //        }
    //    }
    //}


    //Se lance à chaque spawn de bombe
    IEnumerator SpawnBombs()
    {
        //spawn une bombe 
        //attendre un petit moment
        //tant qu'on a pas laché X bomb repeat
        

        while (counter <= bombPerWave)
        {
            yield return new WaitForSeconds(timeBetweenBombs);
            Instantiate(bombPrefab, bombSpawnPoint);
            doOnce = true;
            StopCoroutine("SpawnBombs");
            counter++;

            if (counter >= bombPerWave)
            {
                StartCoroutine("ResetBombing");
            }
        }
    }

    void TurnAIOn()
    {
        AIOn = true;
    }
}
