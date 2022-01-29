using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollision : MonoBehaviour
{
    float damage = 30.0f;
    const string damageEventName = "GetStuck";

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "monster")
        {
            float deepness = gameObject.transform.localScale.x;
            var options = new object[3];

            options[0] = damage;
            options[1] = deepness;
            options[2] = gameObject;

            other.collider.gameObject.SendMessage(damageEventName, options);
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "monster")
        {
            other.collider.gameObject.SendMessage("UnStuck", gameObject);
        }

    }
}
