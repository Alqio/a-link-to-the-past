using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDecorationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 0);
    }

}
