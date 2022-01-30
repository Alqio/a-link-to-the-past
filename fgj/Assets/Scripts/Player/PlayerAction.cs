using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public AudioSource source;

    public List<AudioClip> holeSounds;

    public GameObject holePrefab;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.inFuture && Input.GetButtonDown("HoleAction"))
        {
            var clipIndex = Random.Range(0, holeSounds.Count);
            //source.clip = holeSounds[clipIndex];
            source.PlayOneShot(holeSounds[clipIndex]);

            var newObject = Instantiate(holePrefab, gameObject.transform.position + new Vector3(0, 0, 600), gameObject.transform.rotation);
            Debug.Log("Hole in one!");
        }

    }
}
