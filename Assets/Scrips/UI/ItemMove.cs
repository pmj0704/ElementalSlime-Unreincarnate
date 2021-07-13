using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
        [SerializeField] private float speed = 1f;
        private GameManager gameManager = null;
        public string Name;
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    void Update()
    {
        transform.localScale = new Vector2 (1f, 1f);
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.x > GameManager.Instance.MaxPosition.x) gameObject.SetActive(false);
        if (transform.position.y < GameManager.Instance.MinPosition.y) gameObject.SetActive(false);
        if (transform.position.x < GameManager.Instance.MinPosition.x) gameObject.SetActive(false);
    }
}
