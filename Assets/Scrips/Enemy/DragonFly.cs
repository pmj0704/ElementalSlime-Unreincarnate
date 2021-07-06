using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : EnemyMove
{
    protected override void Start(){base.Start();}
    protected override void Update()
    {
        if(isDead) return;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.y > gameManager.MaxPosition.y + 2f) Despawn(gameObject);
        if(transform.position.y < gameManager.MinPosition.y - 2f) Despawn(gameObject);
        if(transform.position.x > gameManager.MaxPosition.y + 2f) Despawn(gameObject);
        if(transform.position.x < gameManager.MinPosition.y - 2f) Despawn(gameObject);
    }
}
