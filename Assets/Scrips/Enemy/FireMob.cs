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
        startwith();
    }
    public void startwith()
    {
        for(int j=0; j<3; j++)
        {
            GameObject flameEffect = Instantiate(flamePrefab);
            flameEffect.transform.SetParent(gameObject.transform, false);
            flameEffect.SetActive(false);
        }
        makeFire();
    }
    protected override void Update()
    {
        base.Update();
    }
    private void makeFire()
    {
        GameObject flame = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(spawnFire(flame));
    }
    private IEnumerator spawnFire(GameObject flame)
    {
        yield return new WaitForSeconds (0.6f);
        flame.SetActive(true);
        flame.transform.SetParent(null);
        i++;
        if(i<3)makeFire();
    }

}
