using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool inFuture = true;

    public const string FUTURE_TAG = "Future";
    public const string PAST_TAG = "Past";

    public List<GameObject> pastObjects;
    public List<GameObject> futureObjects;
    public static GameManager Instance { get; private set; }
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
        foreach (var pastObject in pastObjects)
        {
            pastObject.gameObject.SetActive(inFuture);
        }

        foreach (var futureObject in futureObjects)
        {
            futureObject.gameObject.SetActive(!inFuture);
        }
        inFuture = !inFuture;
        Debug.Log(inFuture);
    }

    public void Win()
    {
        Debug.Log("OU jea, voitto!");
    }

}
