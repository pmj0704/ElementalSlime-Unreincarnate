using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMove : EnemyMove
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        if(isDead) return;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.y > GameManager.Instance.MaxPosition.y ) Despawn(gameObject);
        if(transform.position.y < GameManager.Instance.MinPosition.y ) Despawn(gameObject);
        if(transform.position.x > GameManager.Instance.MaxPosition.y + 2f) Despawn(gameObject);
        if(transform.position.x < GameManager.Instance.MinPosition.y - 2f) Despawn(gameObject);
        base.Update();
    }
}
