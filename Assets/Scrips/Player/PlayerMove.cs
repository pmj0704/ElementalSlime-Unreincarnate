using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    #region 변수 목록
    private GameManager gameManager = null;
    private Animator animator = null;
    private AudioSource audioSource = null;
    private bool isDamaged = false;
    private SpriteRenderer spriteRenderer = null;
    public bool Thunder = false;
    public bool Stone = false;
    public bool Flame = false;
    public bool Harpy = false;
    private float nspeed;
    #endregion

    #region 발사
    private Vector2 targetPosition = Vector2.zero;
    [Header("이동속도")] [SerializeField] private float speed = 5f;
    [Header("총알 발사 위치")] [SerializeField] private Transform bulletPosition = null;
    [Header("총알 프리팹")] [SerializeField] private GameObject bulletPrefab = null;
    [Header("총알 발사간격")] [SerializeField] private float bulletDelay = 0.5f;
    [Header("총알 스프라이트")] [SerializeField] private Sprite OriginalBulletSprite;

    #endregion


    void Start()
    {
        bulletPrefab.GetComponent<SpriteRenderer>().sprite = OriginalBulletSprite;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, GameManager.Instance.MinPosition.x, GameManager.Instance.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, GameManager.Instance.MinPosition.y, GameManager.Instance.MaxPosition.y);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }

    }
    #region 총알 발사
    private IEnumerator Fire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(bulletDelay);
        }
    }
    public void Stop()
    {
        nspeed = speed;
        speed = 0;
    }
    public void Resume()
    {
        speed = nspeed;
    }
   private GameObject InstantiateOrPool()
    {
        GameObject result = null;
        if (GameManager.Instance.poolManager.transform.childCount > 0)
        {
            audioSource.Play();
            result = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
            result.transform.position = bulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);

        }
        else
        {
            audioSource.Play();
            GameObject Bullet = Instantiate(bulletPrefab, bulletPosition);
            Bullet.transform.position = bulletPosition.position;
            Bullet.transform.SetParent(null);
            result = Bullet;
        }
        result.transform.localScale = bulletPosition.lossyScale;
        return result;
    }
    #endregion

    #region 플레이어가 먹거나 맞을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) 
        {
            if (isDamaged) return;
            ItemMove item = collision.gameObject.GetComponent<ItemMove>();
            Destroy(collision.gameObject);
            switch(item.Name)
            {
                case "Thunder":
                Harpy = false;
                Stone = false;
                Flame = false;
                StartCoroutine(Blip(2));
                animator.Play("Thunder");
                GameManager.Instance.getLife(3);
                bulletDelay = 0.5f;
                Thunder = true;
                break;

                case "Stone":
                Thunder = false;
                Harpy = false;
                Flame = false;
                StartCoroutine(Blip(2));
                GameManager.Instance.getLife(6);
                animator.Play("Stone");
                Stone = true;
                break;

                case "Flame":
                Thunder = false;
                Stone = false;
                Harpy = false;
                StartCoroutine(Blip(2));
                GameManager.Instance.getLife(4);
                animator.Play("Fire");
                bulletDelay = 0.3f;
                Flame = true;
                break;
                case "Harpy":
                Thunder = false;
                Stone = false;
                Flame = false;
                StartCoroutine(Blip(2));
                GameManager.Instance.getLife(4);
                animator.Play("Harp");
                bulletDelay = 0.2f;
                Harpy = true;
                break;
                
                case "Heart":
                GameManager.Instance.getLife(4);
                break;
            } 
        }
        else if(collision.CompareTag("Debuff"))
        {
            if (isDamaged) return;
            DeBuffs deBuffs = collision.gameObject.GetComponent<DeBuffs>();
            switch(deBuffs.Debuff)
            {
                case "Stoned":
                StartCoroutine(Stoned());
                break;
                case "Freeze":
                StartCoroutine(Freeze());
                break;
                case "Harp":
                StartCoroutine(Harp());
                break;
            }
        }
        else if(!(collision.CompareTag("Dummy"))){
        if (isDamaged) return;
        Dead();
        }
    }
    private IEnumerator Stoned()
    {
        speed = 0;
        spriteRenderer.material.SetColor("_Color", new Color(0.3f,0.3f,0.3f,1f));
        yield return new WaitForSeconds (2f);
        spriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,1f));
        speed = 5;
    }
    private IEnumerator Freeze()
    {
        speed = 1;
        spriteRenderer.material.SetColor("_Color", new Color(0.3f,0.9680373f,1f,1f));
        yield return new WaitForSeconds (4f);
        spriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,1f));
        speed = 5;
    }
    private IEnumerator Harp()
    {
        speed = 3;
        for(float i = 0; i < 2f; i += Time.deltaTime)transform.position += Vector3.up * speed * Time.deltaTime;
        spriteRenderer.material.SetColor("_Color", new Color(0.9811321f,0.4304022f,0.5f,1f));
        yield return new WaitForSeconds (3f);
        spriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,1f));
        speed = 5;
    }
    private void Dead()
    {
        GameManager.Instance.Dead();
        StartCoroutine(Blip(5));
    }
    public void SetDelay(float delay)
    {
        bulletDelay = delay; 
    }
    private IEnumerator Blip(int num)
    {
        for (int i = 0; i < num; i++)
        {
                isDamaged = true;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
                isDamaged = false;
    }
    #endregion
    public void Hack()
    {
        bulletDelay = 0.01f;
    }
}

