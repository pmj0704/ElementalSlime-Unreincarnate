using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BulletMove
{
    private EnemyBulletManager enemyBulletManager { get; set; }
    protected override void Start()
    {
        base.Start();
        enemyBulletManager = FindObjectOfType<EnemyBulletManager>();
    }

    protected override void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        CheckLimit();
    }
    protected override void CheckLimit()
    {
        if (transform.position.y > GameManager.Instance.MaxPosition.y + 1f) Despawn();
        if (transform.position.y < GameManager.Instance.MinPosition.y - 2f) Despawn();
        if (transform.position.x > GameManager.Instance.MaxPosition.x + 2f) Despawn();
        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f) Despawn();
    }
    private void Despawn()
    {
        transform.SetParent(GameManager.Instance.enemyBulletManager.transform, false);
        gameObject.SetActive(false);
    }
}
