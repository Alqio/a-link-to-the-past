using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool inFuture = true;

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

        var past = GameObject.FindGameObjectsWithTag("Past");
        var future = GameObject.FindGameObjectsWithTag("Future");
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TimeTravel"))
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
    }
}
