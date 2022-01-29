using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool inFuture = true;

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
            pastObject.gameObject.SetActive(false);
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
    }

    public void TimeTravel()
    {
        if (inFuture && isTimeTravelOnCooldown) {
            Debug.Log("Time travel on cooldown! Can't travel for now.");
            return;
        }

        foreach (var pastObject in pastObjects)
        {
            pastObject.gameObject.SetActive(inFuture);
        }

        foreach (var futureObject in futureObjects)
        {
            futureObject.gameObject.SetActive(!inFuture);
        }
        inFuture = !inFuture;

        if (!inFuture) {
            OnEnterPast();
        } else {
            OnEnterFuture();
        }

        Debug.Log(inFuture);
    }

    // Start a Timer, after which the Player is again forced to travel to the future
    public void OnEnterPast() {
        Debug.Log("OnEnterPast");
        if (pastTimer == null) {
            pastTimer = this.gameObject.AddComponent<PastTimer>();
        } else {
            pastTimer.ResetTimer();
        }
        pastTimer.SetDuration(PAST_MAX_SECONDS);
        pastTimer.StartTimer();
    }

    public void EndCooldown() {
        Debug.Log("Cooldown ended");
        isTimeTravelOnCooldown = false;
    }

    public void StartCooldown() {
        Debug.Log("Cooldown started");
        isTimeTravelOnCooldown = true;
    }

    // Start a cooldown Timer. The player can't travel back in time until the Timer has finished.
    public void OnEnterFuture() {
        Debug.Log("OnEnterFuture");
        StartCooldown();
        if (cooldownTimer == null) {
            cooldownTimer = this.gameObject.AddComponent<CooldownTimer>();
        } else {
            cooldownTimer.ResetTimer();
        }
        cooldownTimer.SetDuration(COOLDOWN_MAX_SECONDS);
        cooldownTimer.StartTimer();
    }

    public void Win()
    {
        Debug.Log("OU jea, voitto!");
    }

}
