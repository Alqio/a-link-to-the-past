using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterStateEnumNameSpace;

public class MonsterMovement : MonoBehaviour
{
    Vector2 currentTarget;
    int targetIndex = 0;


    public float speed;
    public float stuckSpeed;
    List<GameObject> targetPoints = new List<GameObject>();
    System.Random rnd = new System.Random();

    public GameObject room;

    public int amountOfTargetPoints;
    public Sprite targetSprite;
    public GameObject player;

    public GameObject projectilePrefab;

    public enum MonsterBehavior // your custom enumeration
    {
        Random,
        Pattern,
        Follow
    };

    public MonsterBehavior movementBehavior;
    MonsterBehavior prevBehavior = MonsterBehavior.Random;

    public bool showTargets = true;

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void ResetTargets()
    {
        foreach (var target in targetPoints)
        {
            Destroy(target);
        }
        targetPoints.Clear();
        for (int i = 0; i < amountOfTargetPoints; i++)
        {
            GameObject tmp = new GameObject();
            tmp.transform.position = RandomPointInBounds(room.GetComponent<Collider2D>().bounds);
            tmp.AddComponent<SpriteRenderer>().sprite = targetSprite;
            targetPoints.Add(tmp);
        }
    }

    public bool shooted = false;
    Vector2 GetNextTarget()
    {
        if (movementBehavior == MonsterBehavior.Pattern)
        {
            if (targetIndex < amountOfTargetPoints - 1)
            {
                targetIndex += 1;
            }
            else
            {
                targetIndex = 0;
            }
            return targetPoints[targetIndex].transform.position;
        }
        else if (movementBehavior == MonsterBehavior.Random)
        {
            return targetPoints[rnd.Next(targetPoints.Count)].transform.position;
        }
        else
        {
            return player.transform.position;
        }
    }

    void PickNewTarget()
    {
        currentTarget = GetNextTarget();
        var q = Quaternion.LookRotation(currentTarget - (Vector2)transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 10 * Time.deltaTime);
        speed = (float)System.Math.Pow(2, rnd.Next(1, 4));

    }

    IEnumerator Shoot()
    {
        Debug.Log("shoot tooty");
        var q = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 10 * Time.deltaTime);

        GameObject projectile = Instantiate(projectilePrefab, transform);
        //projectile.GetComponent<Projectile>().target = player.transform.position;

        GetComponent<MonsterState>().state = MonsterStateEnum.Normal;
        PickNewTarget();
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MonsterState>().state = MonsterStateEnum.Normal;

        ResetTargets();
        currentTarget = GetNextTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<MonsterState>().state);

        float step = speed * Time.deltaTime;

        if (prevBehavior != movementBehavior)
        {
            prevBehavior = movementBehavior;
            currentTarget = GetNextTarget();
        }
        else if (Vector2.Distance(transform.position, currentTarget) < 0.1)
        {
            MonsterState monsterStateScript = GetComponent<MonsterState>();
            /* if (monsterStateScript.state != MonsterStateEnum.Shoot)
             {
                 monsterStateScript.state = MonsterStateEnum.Shoot;


                 //GetComponent<MonsterState>().state = MonsterStateEnum.Normal;
                 //StartCoroutine(Shoot());
             }*/
            shooted = true;
            var q = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 10 * Time.deltaTime);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().direction = Vector3.Normalize(transform.position - player.transform.position);

            PickNewTarget();

        }
        else
        {
            if (movementBehavior == MonsterBehavior.Follow)
            {
                currentTarget = player.transform.position;
                transform.LookAt(player.transform.position, Vector3.down);
            }
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
        }

        if (Input.GetButtonDown("ResetMonsterTargets"))
        {
            ResetTargets();
            currentTarget = GetNextTarget();

        }

        foreach (var item in targetPoints)
        {
            item.GetComponent<SpriteRenderer>().enabled = showTargets;
        }
    }
}
