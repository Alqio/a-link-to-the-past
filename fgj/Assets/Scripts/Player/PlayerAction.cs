using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public GameObject holePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.inFuture && Input.GetButtonDown("HoleAction"))
        {
            var newObject = Instantiate(holePrefab, gameObject.transform.position + new Vector3(0, 0, 690), gameObject.transform.rotation);
            Debug.Log("Hole in one!");
        }

    }
}
