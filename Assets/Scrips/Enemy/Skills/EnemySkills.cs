using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    [Header("비활성화 될 때까지의 시간")][SerializeField] float time;
    void Start()
    {
        StartCoroutine(WaitUntilDestroy(time));
    }
    private IEnumerator WaitUntilDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
