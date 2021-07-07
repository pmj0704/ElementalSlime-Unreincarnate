using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceM : MonoBehaviour
{
    [SerializeField] private GameObject Muon1;
    [SerializeField] private GameObject TxtBox;
    private TextTyping textTyping;
    private float waitingTime = 0;
    void Start()
    {   
        textTyping = FindObjectOfType<TextTyping>();
        StartCoroutine(FirstAni());
    }
    private IEnumerator FirstAni()
    {
        yield return new WaitForSeconds (0.8f);
        Muon1.SetActive(true);
        yield return new WaitForSeconds (10f);
        TxtBox.SetActive(true);
    }
}
