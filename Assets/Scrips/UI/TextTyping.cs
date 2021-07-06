using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyping : MonoBehaviour
{
   [SerializeField] private UnityEngine.UI.Text tx;
    [SerializeField]private UnityEngine.UI.Text txName;
    [SerializeField]private GameObject nameType;
    [SerializeField]private GameObject YesOrNo;

    private Name nm = null;
   private string t_text;
   public int textNum = 0;
   private int next = 0;
   private bool wait = true;
   private bool start = true;
   [SerializeField]private GameObject Muon;
   [SerializeField]private Camera mainCamera;
   [SerializeField]private Canvas mainCanvas;

    void Start()
    {
        StartCoroutine(Fst());
        nm = FindObjectOfType<Name>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) next++;
        if(Input.GetKeyDown(KeyCode.F2)) next--;
        if(Input.GetMouseButton(0) && !wait){
            next++;
            start = true;
            wait = true;
            }
        if(start){
        switch(next)
        {
            case 1: 
            StartCoroutine(Second());
            break;
            case 2:
            StartCoroutine(Third());
            break;
            case 3:
            StartCoroutine(Forth());
            break;
        }
        }
    }
    private IEnumerator Fst()
    {
        yield return new WaitForSeconds (12f);
        t_text = "넌.. 누구야..!";
        txName.text = string.Format("???"); 
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds(2f);
        wait = false;
    }
    private IEnumerator Second()
    {
        start = false;
        txName.text = string.Format("나");
        t_text = "내 이름은...";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        nameType.SetActive(true);
    }
    public void St(string NM)
    {
        StartCoroutine(nextT(NM));
    }
    private IEnumerator nextT(string NM)
    {
        yield return new WaitForSeconds (1.5f);
        txName.text = string.Format("???");
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "오! \n" + NM + "\n만나서 반가워!" ;
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        t_text = "난 뮤온이야!";
        StartCoroutine(typing(t_text));
        txName.text = string.Format("뮤온");
        yield return new WaitForSeconds (2f);
        wait = false;
    }
    
    private IEnumerator Third()
    {
        start = false;
        Muon.GetComponent<Animator>().Play("Idle");
        t_text = "(혹시 예언 속 슬라임?)";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "앗.. 아니야";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (0.8f);
        wait = false;
    }
    private IEnumerator Forth()
    {   
        start = false;
        t_text = "그럼 이 곳이 처음이야?";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        t_text = "";
        StartCoroutine(typing(t_text));
        YesOrNo.SetActive(true);
    }
    private IEnumerator Fifth()
    {
        float i = 0f;
        YesOrNo.SetActive(false);
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "그렇구나!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1.8f);
        t_text = "그럼 내가 안내 해 줄게!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "따라와!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        Muon.GetComponent<Animator>().Play("Follow");
        yield return new WaitForSeconds (3f);
        mainCamera.transform.rotation = Quaternion.Euler(0f,180f,0f);
        mainCanvas.enabled = false;
        yield return new WaitForSeconds (2f);
        if((i >= 10f)){
            i++;
        mainCamera.GetComponent<Camera>().orthographicSize = i;
        yield return new WaitForSeconds (0.8f);
        }
    }
    /*private IEnumerator Ending()
    {
        txName.text = string.Format("뮤온");
        t_text = "자! 그러면 모험을 시작하자!";
        StartCoroutine(typing(t_text));
        txName.text = string.Format("나");

    }*/
    private IEnumerator typing(string _text)
    {
        for(int i=0; i <= _text.Length; i++)
            {
                tx.text = _text.Substring(0, i);
                yield return new WaitForSeconds(0.08f);
            }   
    }
    public void Yes()
    {
        StartCoroutine(Fifth());
    }
}

