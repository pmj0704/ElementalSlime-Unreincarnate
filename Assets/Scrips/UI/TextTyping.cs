using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTyping : MonoBehaviour
{
   [SerializeField] private UnityEngine.UI.Text tx;
    [SerializeField]private UnityEngine.UI.Text txName;
    [SerializeField]private GameObject point;
    [SerializeField]private GameObject nameType;
    [SerializeField]private GameObject YesOrNo;
    private Camera cam;
    private Name nm = null;
   private string t_text;
   public int textNum = 0;
   private int next = 0;
   private bool wait = true;
   private bool start = true;
   [SerializeField]private GameObject Muon;
   [SerializeField]private Camera mainCamera;
   [SerializeField]private GameObject MCanvas;
   [SerializeField]private Canvas mainCanvas;
   [SerializeField] private GameObject textBox;
   private bool end = false;
   private Dummy dummy = null;

    void Start()
    {
        if(end){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");}
        StartCoroutine(Fst());
        nm = FindObjectOfType<Name>();
    }
    void Update()
    {
        if(dummy == null){
        dummy = FindObjectOfType<Dummy>();
        }
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
            case 4:
            StartCoroutine(Sixth());
            break;
            case 5:
            StartCoroutine(Seventh());
            break;
            case 6:
            Last();
            break;
        }
        }
    }
    private IEnumerator Fst()
    {
        yield return new WaitForSeconds (12f);
        t_text = "???.. ?????????..!";
        txName.text = string.Format("???"); 
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds(2f);
        wait = false;
    }
    private IEnumerator Second()
    {
        start = false;
        txName.text = string.Format("???");
        t_text = "??? ?????????...";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        nameType.SetActive(true);
    }
    public void St(string NM)
    {
        StartCoroutine(nextT(NM));
    }
    private IEnumerator nextT(string NM)
    {
        yield return new WaitForSeconds (0.3f);
        txName.text = string.Format("???");
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "???! \n" + NM + "\n????????? ?????????!" ;
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "??? ????????????!";
        StartCoroutine(typing(t_text));
        txName.text = string.Format("??????");
        yield return new WaitForSeconds (2f);
        wait = false;
    }
    
    private IEnumerator Third()
    {
        start = false;
        Muon.GetComponent<Animator>().Play("Idle");
        t_text = "(?????? ?????? ??? ??????????)";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "???.. ?????????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (0.8f);
        wait = false;
    }
    private IEnumerator Forth()
    {   
        start = false;
        t_text = "?????? ??? ?????? ?????????????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1.8f);
        t_text = "";
        StartCoroutine(typing(t_text));
        YesOrNo.SetActive(true);
    }
    private IEnumerator Fifth()
    {
        YesOrNo.SetActive(false);
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "????????????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1.8f);
        t_text = "?????? ?????? ?????? ??? ??????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "?????????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        wait = false;
    }

    private IEnumerator Sixth()
    {
        start = false;
        Muon.GetComponent<Animator>().Play("Follow");
        yield return new WaitForSeconds (1.6f);
        tx.transform.SetParent(null);
        tx.transform.SetParent(mainCanvas.transform);
        t_text = "";
        StartCoroutine(typing(t_text));
        textBox.GetComponent<Animator>().Play("FadeOut");
        point.GetComponent<Animator>().Play("FadeOutP");
        yield return new WaitForSeconds (1.2f);
        textBox.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Tutorial");
        yield return new WaitForSeconds (3f);
        Muon.GetComponent<Animator>().Play("ComeIn");
        yield return new WaitForSeconds (1.3f);
        Muon.GetComponent<Animator>().Play("Smile");
        tx.transform.localPosition = new Vector2(-411f, -400f);
        tx.fontSize = 90;
        tx.alignment = TextAnchor.MiddleRight;
        yield return new WaitForSeconds (1.3f);
        t_text = "????????? ?????????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        t_text = "?????? ???????????? ????????????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        Muon.GetComponent<Animator>().Play("Idle");
        t_text = "????????? ????????? ?????? ????????????\n???????????? ??????????????? ?????? ??????!!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (4f);
        t_text = "????????? ???????????? \n?????? ????????? ?????????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        t_text = "????????? ?????? \n????????? ??? ??????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        wait = false;
    }
    private IEnumerator Seventh()
    {
        start = false;
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "?????????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        t_text = "?????? ??? ?????? ????????? \n???????????? ????????? ???!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        dummy.st();
        yield return new WaitForSeconds (3f);
    }
    public void Eg()
    {
        StartCoroutine(Eighth());
    }
    private IEnumerator Eighth()
    {
        t_text = "?????? ??????..";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        t_text = "?????? ????????? ????????? \n???????????? ????????? ??????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2.3f);
        t_text = "";
        StartCoroutine(typing(t_text));
        Muon.GetComponent<Animator>().Play("Life");
        yield return new WaitForSeconds (3f);
        Muon.GetComponent<Animator>().Play("Smile");
        t_text = "????????? ????????? ????????? ????????? ??????!";
        StartCoroutine(typing(t_text));
    }
    public void Ni()
    {
        StartCoroutine(Nine());
    }
    private IEnumerator Nine()
    {
        t_text = "???..!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        t_text = "?????? ??????!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        t_text = "????????? ?????? ?????? \n???????????? ???????????? ?????????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2.5f);
        t_text = "???????????? ????????? \n????????? ?????? ????????? ????????? ???!";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2.5f);
        StartCoroutine(YesEnding());
    }
    public void No()
    {
        StartCoroutine(Ending());
    }
    private IEnumerator Ending()
    {
        YesOrNo.SetActive(false);
        txName.text = string.Format("??????");
        t_text = "????????????..";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        t_text = "????????? ?????????..";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (1f);
        Muon.GetComponent<Animator>().Play("Disap");
        yield return new WaitForSeconds (2.5f);
        txName.text = string.Format("???");
        t_text = "?? ????????? ????????????.";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "?????? ?????? ??? ???????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (3f);
        next = 5;
        wait = false;
    }
    private IEnumerator YesEnding()
     {
        t_text = "???! ????????? ?????????..";
        StartCoroutine(typing(t_text));
        Muon.GetComponent<Animator>().Play("Disap");
        yield return new WaitForSeconds (2.5f);
        tx.transform.localPosition = new Vector2(-500, -700f);
        t_text = "?? ????????? ????????????.";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        t_text = "?????? ????????? ???????";
        StartCoroutine(typing(t_text));
        yield return new WaitForSeconds (2f);
        next = 5;
        wait = false;
    }

    public void Last()
    {
        start = false;
        Destroy(MCanvas);
        Destroy(Muon);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }


    public void Smile()
    {
        Muon.GetComponent<Animator>().Play("Smile");
    }
    public void Idle()
    {
        Muon.GetComponent<Animator>().Play("Idle");
    }
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

