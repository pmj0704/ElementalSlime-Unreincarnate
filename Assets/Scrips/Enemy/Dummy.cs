using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Animator animator = null;
    [SerializeField] private GameObject bullet;
    private GameManager gameManager = null;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Heart;
    private PlayerMove playerMove = null;
    private bool isDead = false;
    private bool isDamaged = false;
    private TextTyping textTyping = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private GameObject t1;
   [SerializeField] private GameObject t2;
    private int hp = 1000000;
    private int time = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        
    }

    void Update()
    {
        if(textTyping == null)textTyping = FindObjectOfType<TextTyping>();
        if(hp <= 25 && time == 1)
        {
            t1.SetActive(true);
            textTyping.Idle();
            StartCoroutine(SmileOff());
            time++;
        }
        if(hp <=20  && time == 2)
        {
            textTyping.Eg();
            StartCoroutine(Fire());
            t2.SetActive(true);
            textTyping.Idle();
            StartCoroutine(SmileOff2());
            time++;
        }
    }
    private IEnumerator SmileOff()
    {
            yield return new WaitForSeconds (2f);
            t1.SetActive(false);
            textTyping.Smile();
    }
    private IEnumerator SmileOff2()
    {
            yield return new WaitForSeconds (2f);
            t2.SetActive(false);
            textTyping.Smile();
    }

    public void st()
    {
        animator.enabled = true;
        StartCoroutine(DDDJ());
    }
    public IEnumerator Fire()
    {
        yield return new WaitForSeconds (3f);
        Instantiate(bullet);
        player.transform.position = new Vector2(0.02f, -3.42f);
        playerMove.Stop();
        StartCoroutine(Freeze());
    }
    private IEnumerator Freeze()
    {
        yield return new WaitForSeconds (3f);
        playerMove.Resume();
    }
    private IEnumerator DDDJ()
        {
            animator.Play("DummyMove");
            yield return new WaitForSeconds (1f);
            animator.Play("Dummy");
            hp = 25;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.CompareTag("Bullet"))
        {
            if (isDamaged) return;



            DespawnB(collision.gameObject);
            StartCoroutine(Damaged());
            if (hp <= 0)
            {
                if (isDead) return;
                isDead = true; 
                gameManager.AddScore(100);
                StartCoroutine(Dead());
            }
        }
    }
    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        isDamaged = false;
    }
    private IEnumerator Dead()
    {
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        col.enabled = false;
        animator.enabled = true;
        textTyping.Ni();
        Heart.SetActive(true);
        Heart.transform.SetParent(null);
        yield return new WaitForSeconds(0.4f);
        Despawn(gameObject);
    }
   
         private void DespawnB(GameObject Object)
    {
        Object.transform.SetParent(gameManager.poolManager.transform, false);
        Object.SetActive(false);
    }
    public void Despawn(GameObject Object)
    {
        Object.transform.SetParent(gameManager.objectManager.transform, false);
        Object.SetActive(false);
    }
}
