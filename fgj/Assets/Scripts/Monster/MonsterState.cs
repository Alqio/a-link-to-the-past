using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Stuck,
    Dead,
    Cold,
    Normal
}

public class MonsterState : MonoBehaviour
{

    public State state = State.Normal;
    MonsterHealthComponent hpComponent;

    // Start is called before the first frame update
    void Start()
    {
        hpComponent = GetComponent<MonsterHealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpComponent.currentHealth <= 0)
        {
            state = State.Dead;
            GameManager.Instance.Win();
            Destroy(gameObject);
        }
        if (state == State.Stuck)
        {
            Debug.Log("Monster is stuck");
        }
        else if (state == State.Dead)
        {
            Debug.Log("win");
        }
    }

    void GetStuck(object[] opts)
    {
        float damage = (float)opts[0];
        float deepness = (float)opts[1];

        state = State.Stuck;

        hpComponent.ApplyDamage(damage);

    }

    public void UnStuck()
    {
        state = State.Normal;
        //TODO mihin stateen sen pitää mennä?
    }

}
