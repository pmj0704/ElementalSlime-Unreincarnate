using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeonBulley : MonoBehaviour
{
    private GameManager gameManager = null;
     [SerializeField]private float speed = 10f;
     private bool dead =false;
    void Start()
    {
         gameManager = FindObjectOfType<GameManager>();
         //StartCoroutine(AutoDesapwn());
    }

    void Update()
    {
        if(dead)
        {
        // StartCoroutine(AutoDesapwn());
        dead = false;
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        
        if (transform.position.y > gameManager.MaxPosition.y + 0.4f) Despawn(gameObject);
        if (transform.position.y < gameManager.MinPosition.y - 2f) Despawn(gameObject);
        if (transform.position.x > gameManager.MaxPosition.x + 2f) Despawn(gameObject);
        if (transform.position.x < gameManager.MinPosition.x - 2f) Despawn(gameObject);
    }
    public IEnumerator AutoDesapwn()
    {
        yield return new WaitForSeconds (4f);
        Despawn(gameObject);
    }
    public void Despawn(GameObject GO)
    {
        dead = true;
        GO.transform.SetParent(gameManager.enemyBulletManager.transform);
        GO.transform.position = new Vector2(0,0);
        GO.SetActive(false);
    }
}
