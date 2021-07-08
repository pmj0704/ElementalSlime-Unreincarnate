using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMob : EnemyMove
{
    [SerializeField] private Transform CurrentPos;
    [SerializeField] private GameObject flamePrefab;
    private int i = 0;
        protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if(cor == 0)
        {
        StartCoroutine(spawnFire());
        cor = 1;
        }
    }
    private IEnumerator spawnFire()
    {
        yield return new WaitForSeconds (0.6f);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
        yield return new WaitForSeconds (0.6f);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.transform.SetParent(null);
        yield return new WaitForSeconds (0.6f);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.transform.SetParent(null);
    }

}
