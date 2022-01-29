using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterStateEnumNameSpace;

namespace MonsterStateEnumNameSpace
{
    public enum MonsterStateEnum
    {
        Stuck,
        Dead,
        Normal,
        Shoot
    }
}
public class MonsterState : MonoBehaviour
{

    public MonsterStateEnum state = MonsterStateEnum.Normal;
    MonsterHealthComponent hpComponent;
    MonsterMovement moveComponent;

    float lastSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveComponent = GetComponent<MonsterMovement>();
        hpComponent = GetComponent<MonsterHealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpComponent.currentHealth <= 0)
        {
            state = MonsterStateEnum.Dead;
            GameManager.Instance.Win();
            Destroy(gameObject);
        }
        if (state == MonsterStateEnum.Stuck)
        {
            Debug.Log("Monster is stuck");
        }
        else if (state == MonsterStateEnum.Dead)
        {
            Debug.Log("win");
        }
    }

    void GetStuck(object[] opts)
    {
        float damage = (float)opts[0];
        float deepness = (float)opts[1];

        state = MonsterStateEnum.Stuck;

        hpComponent.ApplyDamage(damage);

        lastSpeed = moveComponent.speed;
        moveComponent.speed = moveComponent.stuckSpeed;

    }
    public void UnStuck(GameObject hole)
    {
        Destroy(hole);
        state = MonsterStateEnum.Normal;
        moveComponent.speed = lastSpeed;

    }



}
