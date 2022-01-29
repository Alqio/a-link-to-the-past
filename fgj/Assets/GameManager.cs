using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool inFuture = true;

    public PostProcessVolume pastPostProcessing;

    public const string FUTURE_TAG = "Future";
    public const string PAST_TAG = "Past";
    public float PAST_MAX_SECONDS = 3.0f;
    public float COOLDOWN_MAX_SECONDS = 10.0f;

    public List<GameObject> pastObjects;
    public List<GameObject> futureObjects;
    public static GameManager Instance { get; private set; }

    public PastTimer pastTimer = null;
    public CooldownTimer cooldownTimer = null;

    bool isTimeTravelOnCooldown = false;
    float postProcessingLerpSpeed = -2.0f;
    bool isPostProcessTransitioning = false;

    void Awake()
    {
        //Debug.LogError(SceneManager.GetActiveScene().name);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Warning: multiple " + this + " in scene!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pastObjects = new List<GameObject>();
        futureObjects = new List<GameObject>();

        var past = GameObject.FindGameObjectsWithTag(PAST_TAG);
        var future = GameObject.FindGameObjectsWithTag(FUTURE_TAG);
        foreach (var pastObject in past)
        {
            pastObjects.Add(pastObject);
            pastObject.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        foreach (var futureObject in future)
        {
            futureObjects.Add(futureObject);
        }

    }

    void AddObject(GameObject obj)
    {
        var tag = obj.tag;
        if (tag == FUTURE_TAG)
        {
            futureObjects.Add(obj);
        }
        else if (tag == PAST_TAG)
        {
            pastObjects.Add(obj);
        }
        else
        {
            Debug.Log("Tried to add object with unknown tag! Tag: " + tag);
        }
    }

    void AddFutureObject(GameObject obj)
    {
        obj.tag = FUTURE_TAG;
        futureObjects.Add(obj);
    }

    void AddPastObject(GameObject obj)
    {
        obj.tag = PAST_TAG;
        pastObjects.Add(obj);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TimeTravel"))
        {
            TimeTravel();
        }

        if (isPostProcessTransitioning)
        {
            var newWeightValue = pastPostProcessing.weight + Time.deltaTime * postProcessingLerpSpeed;
            if (newWeightValue > 1)
            {
                newWeightValue = 1;
                isPostProcessTransitioning = false;
            }

            if (newWeightValue < 0)
            {
                newWeightValue = 0;
                isPostProcessTransitioning = false;
            }

            pastPostProcessing.weight = newWeightValue;
        }
    }

    public void TimeTravel()
    {
        if (inFuture && isTimeTravelOnCooldown)
        {
            Debug.Log("Time travel on cooldown! Can't travel for now.");
            return;
        }


        inFuture = !inFuture;

        if (!inFuture)
        {
            OnEnterPast();
        }
        else
        {
            OnEnterFuture();
        }

        Debug.Log(inFuture);
    }

    // Start a Timer, after which the Player is again forced to travel to the future
    public void OnEnterPast()
    {

        Debug.Log("OnEnterPast");

        foreach (var futureObject in futureObjects)
        {
            futureObject.GetComponent<Collider2D>().enabled = false;
            futureObject.GetComponent<FadeScript>().fadingOut = true;
        }
        foreach (var pastObject in pastObjects)
        {
            pastObject.GetComponent<Collider2D>().enabled = true;
            pastObject.GetComponent<FadeScript>().fadingIn = true;
        }


        if (pastTimer == null)
        {
            pastTimer = this.gameObject.AddComponent<PastTimer>();
        }
        else
        {
            pastTimer.ResetTimer();
        }
        pastTimer.SetDuration(PAST_MAX_SECONDS);
        pastTimer.StartTimer();
        postProcessingLerpSpeed = -postProcessingLerpSpeed;
        isPostProcessTransitioning = true;
    }

    public void EndCooldown()
    {
        Debug.Log("Cooldown ended");
        isTimeTravelOnCooldown = false;
    }

    public void StartCooldown()
    {
        Debug.Log("Cooldown started");
        isTimeTravelOnCooldown = true;
    }

    // Start a cooldown Timer. The player can't travel back in time until the Timer has finished.
    public void OnEnterFuture()
    {
        Debug.Log("OnEnterFuture");
        foreach (var pastObject in pastObjects)
        {
            pastObject.GetComponent<Collider2D>().enabled = false;
            pastObject.GetComponent<FadeScript>().fadingOut = true;
        }
        foreach (var futureObject in futureObjects)
        {
            futureObject.GetComponent<Collider2D>().enabled = true;
            futureObject.GetComponent<FadeScript>().fadingIn = true;
        }


        StartCooldown();
        if (cooldownTimer == null)
        {
            cooldownTimer = this.gameObject.AddComponent<CooldownTimer>();
        }
        else
        {
            cooldownTimer.ResetTimer();
        }
        cooldownTimer.SetDuration(COOLDOWN_MAX_SECONDS);
        cooldownTimer.StartTimer();

        postProcessingLerpSpeed = -postProcessingLerpSpeed;
        isPostProcessTransitioning = true;
    }

    public void Win()
    {
        Debug.Log("OU jea, voitto!");
    }

}
