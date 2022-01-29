using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{

    float damage = 30.0f;
    const string damageEventName = "ApplyDamage";

    void OnCollisionEnter2D(Collision2D other)
    {
        other.collider.gameObject.SendMessage(damageEventName, damage);
    }
}
