using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    #region 변수 목록
    [Header("이동 속도")] [SerializeField] protected float speed = 10f;
    private Vector2 bulletScale = Vector2.one;
    protected GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private PlayerMove playerMove = null;
    private Animator animator = null;
    #endregion

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = FindObjectOfType<PlayerMove>();
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    { 
        checkAni();
        transform.position += Vector3.up * speed * Time.deltaTime;
        CheckLimit();
       
    }
    protected virtual void CheckLimit()
    {
        if (transform.position.y > GameManager.Instance.MaxPosition.y + 0.4f) Despawn();
        if (transform.position.y < GameManager.Instance.MinPosition.y - 2f) Despawn();
        if (transform.position.x > GameManager.Instance.MaxPosition.x + 2f) Despawn();
        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f) Despawn();
    }
    private void Despawn()
    {
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
        gameObject.SetActive(false);
    }
    private void checkAni()
    {
        if(playerMove.Thunder)
        {
         animator.Play("Thunder");
        }
        else if(playerMove.Stone)
        {
            animator.Play("StoneB");
        }
        else if(playerMove.Flame)
        {
            animator.Play("FlameBullet");
        }
        else if(playerMove.Harpy)
        {
            animator.Play("Harpy");
        }
    }
}
