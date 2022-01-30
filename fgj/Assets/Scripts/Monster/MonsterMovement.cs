using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterStateEnumNameSpace;

public class MonsterMovement : MonoBehaviour
{
    Vector2 currentTarget;
    int targetIndex = 0;

    Quaternion lookHere;
    public float rotateSpeed;
    public float speed;
    public float chargeSpeed;
    public float stuckSpeed;
    List<GameObject> targetPoints = new List<GameObject>();
    System.Random rnd = new System.Random();

    public GameObject room;

    public int amountOfTargetPoints;
    public Sprite targetSprite;
    public GameObject player;
    public LayerMask mask;
    public GameObject projectilePrefab;

    private AudioSource audioSource;

    public AudioClip walkAudio;
    public AudioClip shootAudio;

    public enum MonsterBehavior // your custom enumeration
    {
        Random,
        Pattern,
        Follow
    };

    public MonsterBehavior movementBehavior;
    MonsterBehavior prevBehavior = MonsterBehavior.Random;

    public bool showTargets = true;
    public GameObject sprite;

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
        speed = (float)System.Math.Pow(2, rnd.Next(1, 4));

        //lookHere = Quaternion.LookRotation(currentTarget - (Vector2)transform.position);


    }

    void RotateTowards(Vector3 target)
    {
        Vector3 vectorToTarget = target - sprite.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, q, Time.deltaTime * rotateSpeed);
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().direction = Vector3.Normalize(transform.position - player.transform.position);
        GameManager.Instance.AddFutureObject(projectile);
    }

    IEnumerator ShootRoutine()
    {

        MonsterState monsterStateScript = GetComponent<MonsterState>();

        RaycastHit2D hit = Physics2D.Raycast(sprite.transform.position, sprite.transform.up * -10, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.name == "player")
        {
            if (rnd.Next(10) < 7)
            {
                Debug.Log("shooty tooty");
                Shoot();
                PickNewTarget();
            }
            else
            {
                Debug.Log("CHAAAARGE!!!");
                currentTarget = player.transform.position;
                speed = chargeSpeed;
            }
        }
        else
        {
            RotateTowards(player.transform.position);
        }

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MonsterState>().state = MonsterStateEnum.Normal;
        audioSource = GetComponent<AudioSource>();

        ResetTargets();
        currentTarget = GetNextTarget();
    }

    public void StopSounds()
    {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;



        if (prevBehavior != movementBehavior)
        {
            prevBehavior = movementBehavior;
            currentTarget = GetNextTarget();
        }
        else if (Vector2.Distance(transform.position, currentTarget) < 0.1)
        {

            StartCoroutine(ShootRoutine());
        }
        else
        {
            if (GameManager.Instance.inFuture)
            {

                if (movementBehavior == MonsterBehavior.Follow)
                {
                    currentTarget = player.transform.position;
                    transform.LookAt(player.transform.position, Vector3.down);
                }
                transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
                RotateTowards(currentTarget);
                if (step > 0.02)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(walkAudio, 0.4f);
                    }
                }

            }
            else
            {
                if (audioSource.isPlaying)
                {
                    StopSounds();
                }

            }
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
