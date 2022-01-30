using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 30.0f;

    public float speed = 4;
    public Vector3 direction;

    void FixedUpdate()
    {
        if (GameManager.Instance.inFuture)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction * -1000, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "player")
        {
            other.collider.gameObject.SendMessage("ApplyDamage", damage);
            GameManager.Instance.RemoveFutureObject(this.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.name != "monster")
        {
            Vector3.Reflect((transform.position - direction * -1000) * speed * Time.deltaTime, other.contacts[0].normal);
        }

    }
}
