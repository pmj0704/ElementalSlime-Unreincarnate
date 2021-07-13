using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : EnemyMove
{
    protected override void Start(){base.Start();}
    protected override void Update()
    {
         if(isFire && fire == 0)
        {StartCoroutine(startFire());}
        if(GameManager.Instance.stage != 1 && stage == 1)Destroy(gameObject);
        if (GameManager.Instance.stage != 2 && stage == 2)Destroy(gameObject);
        if (GameManager.Instance.stage != 3 && stage == 3)Destroy(gameObject);
        if (GameManager.Instance.stage != 4 && stage == 4)Destroy(gameObject);
        if(isDead) return;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.y > GameManager.Instance.MaxPosition.y ) Despawn(gameObject);
        if(transform.position.y < GameManager.Instance.MinPosition.y ) Despawn(gameObject);
        if(transform.position.x > GameManager.Instance.MaxPosition.y + 2f) Despawn(gameObject);
        if(transform.position.x < GameManager.Instance.MinPosition.y - 2f) Despawn(gameObject);
    }
}
