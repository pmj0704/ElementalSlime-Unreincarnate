using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroll : EnemyMove
{
    private Animator animator = null;
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void Update()
    {
        if(cor == 0){
            StartCoroutine(Roll());
        cor =1;}
        base.Update();
    }
    public void Rolling()
    {
        if(isDead) return;
        if(!isDead && hp >= 0)StartCoroutine(Roll());
    }
    private IEnumerator Roll()
    {
        ownSpeed = speed;
        if(!isDead)animator.Play("StoneTrollWalk");
        else{yield return 0;}
        yield return new WaitForSeconds (1.8f);
        if(!isDead)animator.Play("Roll");
        else{yield return 0;}
        yield return new WaitForSeconds (0.45f);
        if(!isDead)animator.Play("RollAfter");
        else{yield return 0;}
        ownSpeed = 4f;
        yield return new WaitForSeconds (1.8f);
        Rolling();
    }
}
