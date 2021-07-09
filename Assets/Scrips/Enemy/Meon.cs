using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private GameManager gameManager = null;
    public EnemyBulletManager enemyBulletManager {get; private set;}
    private bool dead =false;
     private bool spin =false;
    [SerializeField] private float rot_Speed;
    [SerializeField] private Transform pos;
     

    void Start()
    {
        enemyBulletManager = FindObjectOfType<EnemyBulletManager>();
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log(enemyBulletManager.transform.childCount );
        StartCoroutine(Shoot());

    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(0,0), 1 * Time.deltaTime);
        // if(spin){
        // transform.Rotate(Vector3.forward * rot_Speed*100 * Time.deltaTime);
        // GameObject temp = Instantiate(bullet);
        // Destroy(temp, 2f);
        // temp.transform.position = transform.position;
        // temp.transform.rotation = transform.rotation;
        // }
    }
    private IEnumerator Shoot()
    {
            for (int j = 0; j < 360; j += 13){
            shot(j);
            }
            yield return new WaitForSeconds(3f);
            for (int j = 0; j < 360; j += 13){
            shot(j);
            }
            yield return new WaitForSeconds(3f);
             for (int j = 0; j < 360; j += 13){
            shot(j);
            }
            yield return new WaitForSeconds(3f);
             for (int j = 0; j < 360; j += 13){
            shot(j);
            }
            yield return new WaitForSeconds(3f);
        }
    
private GameObject shot(int i)
    {
            GameObject result = null;
            if(enemyBulletManager.transform.childCount > 0) {
            result = gameManager.enemyBulletManager.transform.GetChild(0).gameObject;
            Debug.Log(result);
            result.SetActive(true);
            result.transform.position = Vector2.zero;
            result.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            else
            {
            GameObject temp = Instantiate(bullet);
            temp.transform.position = Vector2.zero;
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
            result = temp;
            }
            return result;
    }
    private IEnumerator DespawnSec(GameObject temp)
    {
        yield return new WaitForSeconds (3f);
            Despawn(temp);

    }
        
    private void Despawn(GameObject GO)
    {
        dead = true;
        GO.transform.SetParent(gameManager.enemyBulletManager.transform, false);
        GO.SetActive(false);
    }
}

