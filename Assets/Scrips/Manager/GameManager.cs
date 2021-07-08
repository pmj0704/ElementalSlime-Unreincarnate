using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region UI
    [Header("최고 점수")][SerializeField] private UnityEngine.UI.Text textScore = null;
    [Header("점수")] [SerializeField] private UnityEngine.UI.Text textHighScore = null;
    [Header("목숨UI")] [SerializeField] private SpriteRenderer lifeSpriteRen = null;
    [Header("종료 버튼")] [SerializeField] GameObject Quit;
    [Header("카운트 다운")] [SerializeField] private UnityEngine.UI.Text Countdown;
    [Header("카운트 다운 옵젝")] [SerializeField] private GameObject CountdownOj;
    [Header("광고 카운트")] [SerializeField] private UnityEngine.UI.Text AdCount;
    [Header("광고 카운트 옵젝")] [SerializeField] private GameObject AdCountOj;
    [Header("광고 창")] [SerializeField] private GameObject DeadScean;
    [Header("메뉴 화면으로")] [SerializeField] private GameObject closeBt;
    [Header("바탕")] [SerializeField] private GameObject BackGround;
    [Header("스테이지 패널")] [SerializeField] private GameObject StagePanel;
    [Header("용암 스테이지")][SerializeField] private Texture2D LavaTexture;
    [Header("바다 스테이지")][SerializeField] private Texture2D seaTexture;
    [Header("안개")][SerializeField] private GameObject Fog;
    [Header("스테이지 이름")][SerializeField] private UnityEngine.UI.Text StageText;
    private PlayFabManager playFabManager = null;
    #endregion 
    #region 카메라
    [SerializeField]private Camera mainCamera;
    Vector3 cameraPos;


    #endregion
    #region 적 프리팹
    [Header("바람 프리팹")] [SerializeField] private GameObject windPrefab = null;
    [Header("바람 생성 시간")] [SerializeField] private float windDealy = 0f;
    [Header("돌 프리팹")] [SerializeField] protected GameObject enemyStone = null;
    [Header("픽시 프리팹")] [SerializeField] private GameObject enemyPixi = null;
    [Header("트롤 프리팹")] [SerializeField] private GameObject enemyTroll = null;
    [Header("뱀 프리팹")] [SerializeField] private GameObject enemySnake = null;
    [Header("잠자리 프리팹")] [SerializeField] GameObject enemyDragonFly = null;
    [Header("파스 프리팹")] [SerializeField] GameObject enemyFlameMob = null;
    [Header("아이템이 있는 적")] [SerializeField] GameObject[] enemiesWithItem = null;
    [Header("보스 프리팹")] [SerializeField] private GameObject bossGolem = null;
    [Header("보스 본체")] [SerializeField] private GameObject mainBody;
    #endregion

    #region 플레이어 정보
    [Header("플레이어 스프라이트")] [SerializeField] private SpriteRenderer playerSprite = null;
    [Header("목숨")] [SerializeField] private Sprite[] Life = null;
    #endregion

    #region 변수 목록
    public int Lhigh = 100000;
    public int stage =1;
    private PlayerMove playerMove = null;
    private SpriteRenderer spriteRenderer = null;
    public Vector2 FogPosition { get; private set; }
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }
    public PoolManager poolManager { get; private set; }
    public ObjectManager objectManager { get; private set; }
    public WindManager windManager { get; private set;}
    public EnemyBulletManager enemyBulletManager { get; private set; }
    private EnemyMove enemyMove;
    private int score = 0;
    private int life = 4;
    private int highScore = 0;
    private bool bossActivate = true;
    private bool BackMsgOn = false;
    private bool counting = false;
    private bool wind =true;
    private BackGroundMove backGroundMove = null;
    [SerializeField] private bool tuto = false;
    #endregion

    #region 시작, 업데이트
    void Start()
    {
        cameraPos = mainCamera.transform.position;
        backGroundMove = FindObjectOfType<BackGroundMove>();
        enemyMove = FindObjectOfType<EnemyMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        poolManager = FindObjectOfType<PoolManager>();
        objectManager = FindObjectOfType<ObjectManager>();
        windManager = FindObjectOfType<WindManager>();
        enemyBulletManager = FindObjectOfType<EnemyBulletManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        MinPosition = new Vector2(-2f, -4.3f);
        MaxPosition = new Vector2(2f, 4.3f);
        FogPosition = new Vector2(-0.08f, 10.57f);
        
        #region 적 소환
        if(!tuto){
        if(stage == 1){
        spawnHelpEnemy(enemyStone, 231233f, 6f);
        spawnHelpEnemy(enemySnake, 3123123f, 6f);
        spawnHelpEnemy(enemyTroll, 3123123f, 6f);
        }
        if(stage == 2)
        {
        spawnHelpEnemy(enemyFlameMob, 231233f, 6f);
        }
        StartCoroutine(Wait());
        if(wind)StartCoroutine(SpawnWind());
        StartCoroutine(randomItemEnemySpawn());
        }
        #endregion

        #region UI
        highScore = PlayerPrefs.GetInt("HIGHSCORE");
        UpdateUI();
        StartCoroutine(FadeStage());
        #endregion
    }
    void Update()
    {
        if(playFabManager == null){
        playFabManager = FindObjectOfType<PlayFabManager>();}

        if(Input.GetKeyDown(KeyCode.F9))Hack();
        if(Input.GetKeyDown(KeyCode.F10))AddScore(1000);
        if(Input.GetKeyDown(KeyCode.F11))AddScore(500);
        if(Input.GetKeyDown(KeyCode.F12))Time.timeScale +=1f;
        if(!counting && !BackMsgOn && Input.GetKeyDown(KeyCode.Escape))GoBack();
        else if(!counting && BackMsgOn && Input.GetKeyDown(KeyCode.Escape))
        {
            Quit.SetActive(false);
            StartCoroutine(WaitFSec());
        }
    if(score >= (1000 + highScore/8) && bossActivate) 
    {
        for(int i = 0; i < 20; i++)shake();
        bossGolem.SetActive(true);
        bossActivate = false;
    }
    if(score == (Lhigh + 1000))
    {
        SeaStage();
    }
    if(life < 5){
        lifeSpriteRen.sprite = Life[life];
    }
    else 
        lifeSpriteRen.sprite = Life[4];
        Shrink();
    }

    #region UI
    private void UpdateUI()
    {
        textScore.text = string.Format("SCORE: {0}", score);
        textHighScore.text = string.Format("HIGHSCORE: {0}", highScore);
    }
    public void GoBack()
    {
        Quit.SetActive(true);
        BackMsgOn = true;
        Time.timeScale = 0f;
    }
    public void Yes()
    {
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1f;
    }
    public void No()
    {
        Quit.SetActive(false);
        StartCoroutine(WaitFSec());
    }
    public void Ads()
    {
        DeadScean.SetActive(false);
        life++;
        StartCoroutine(WaitFSec());
    }
     private IEnumerator WaitAD()
    {
        for(int i = 5; i >= 0; i--)
        {
            AdCount.text = string.Format("{0}", i);
            yield return new WaitForSecondsRealtime(1f);
        }
        AdCountOj.SetActive(false);
        closeBt.SetActive(true);
    }
    private IEnumerator WaitFSec()
    {
        counting = true;
        CountdownOj.SetActive(true);
        Countdown.text = string.Format("3");
        yield return new WaitForSecondsRealtime(1f);
        Countdown.text = string.Format("2");
        yield return new WaitForSecondsRealtime(1f);
        Countdown.text = string.Format("1");
        yield return new WaitForSecondsRealtime(1f);
        CountdownOj.SetActive(false);
        counting = false;
        BackMsgOn = false;
        Time.timeScale = 1f;
    }

    public void AddScore(int addscore)
    {
        score += addscore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        UpdateUI();
    }
    public void Dead()
    {
        life--;
        if (life <= 0){
            DeadScean.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine(WaitAD());
        }
        UpdateUI();
    }
    private IEnumerator FadeStage()
    {
        StagePanel.SetActive(true);
        StageText.GetComponent<Animator>().Play("FadeIn");
        Image image;
        image = StagePanel.GetComponent<Image>();
        Color color = image.color;
        color.a = 0f;
        for(float i = 0; i < 0.8f; i += 0.05f)
        {
            image.color = new Color(0.4f,0.4f,0.4f,i);
            yield return new WaitForSeconds (0.08f);
        }
        yield return new WaitForSeconds (1f);
        for(float i = 0.8f; i >= 0f; i -= 0.05f)
        {
            image.color = new Color(0.4f,0.4f,0.4f,i);
            yield return new WaitForSeconds (0.08f);
        }
        StagePanel.SetActive(false);
    }
    private void shake()
    {
        float cameraPosX = Random.value * 0.05f * 2 - 0.05f;
        float cameraPosY = Random.value * 0.05f * 2 - 0.05f;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }
    #endregion
    
    public void Shrink()
    {
        switch (life)
        {
            case 6:
                playerMove.SetDelay(1.1f);
                playerSprite.transform.localScale = new Vector2(2.8f, 2.8f);
                break;
            case 5:
                playerMove.SetDelay(0.9f);
                playerSprite.transform.localScale = new Vector2(2.5f, 2.5f);
                break;
            case 4:
                playerMove.SetDelay(0.7f);
                playerSprite.transform.localScale = new Vector2(2.5f, 2.5f);
                break;
            case 3:
                playerMove.SetDelay(0.5f);
                playerSprite.transform.localScale = new Vector2(2f, 2f);

                break;
            case 2:
                playerMove.SetDelay(0.4f);
                playerSprite.transform.localScale = new Vector2(1.5f, 1.5f);
                break;
            case 1:
                playerMove.SetDelay(0.2f);
                playerSprite.transform.localScale = new Vector2(1f, 1f);
                break;
        }
    }
    
     public void getLife(int num)
    {
        life = num;
        Shrink();
     }
    #endregion

    #region 적생성
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        float randomX = 0f;
        float randomY = 0f;
        float randomDelay = 0f;
        int randomEnemy = 0;
        while(true)
        {
            randomY = Random.Range(-2f, MaxPosition.y - 2f);
            randomX = Random.Range(MinPosition.x, MaxPosition.x);
            randomDelay = Random.Range(1f, 4f);
            if(stage == 1)randomEnemy = Random.Range(0,3);
            else if(stage == 2)randomEnemy = Random.Range(3,6);
            SpawnEnemy(randomX, randomY,randomEnemy);
            yield return new WaitForSeconds(randomDelay);
        }
    }
    private void SpawnEnemy(float randomX, float randomY, int randomEnemy)
    {
            GameObject result = null;
            if (objectManager.transform.childCount > 0)
            {
                result = objectManager.transform.GetChild(0).gameObject;
                if(result.GetComponent<EnemyMove>().isDragonFly){
                result.transform.position = new Vector2(3f, randomY);
                }
                else{
                result.transform.position = new Vector2(randomX, 6f);
                }
                result.transform.SetParent(null);
                result.SetActive(true);
            }
            else
            {
                switch(randomEnemy)
                {
                    case 0:
                        spawnHelpEnemy(enemySnake, randomX, 6f);
                        break;
                    case 1:
                        spawnHelpEnemy(enemyStone, randomX, 6f);
                        break;
                    case 2:
                        spawnHelpEnemy(enemyTroll, randomX, 6f);
                        break;
                    case 3:
                        StartCoroutine(SpawnDragonfly());
                        break;
                    case 4:
                        spawnHelpEnemy(enemyFlameMob, randomX, 6f);
                        break;
                    case 100:
                        break;
                }               
            }
   }
    private IEnumerator SpawnDragonfly()
    {
        float ranY = 0f;
        float ranD = 0f;

        while (true) 
        {
            ranY = Random.Range(2f, MaxPosition.y -4f);
            ranD = Random.Range(5f, 12f);
            break;
        }
            Instantiate(enemyDragonFly, new Vector2(3.3f, ranY), Quaternion.identity);
            yield return new WaitForSeconds(ranD);
    }
    private void spawnHelpEnemy(GameObject Enemy, float X, float Y)
    {
        Enemy = Instantiate(Enemy);
        Enemy.transform.position = new Vector2(X, Y);
        Enemy.transform.SetParent(null);
    }
    
    private IEnumerator randomItemEnemySpawn()
    {
        yield return new WaitForSeconds (5f);
        int randomEnemy = 100;
        if(stage == 1) randomEnemy = Random.Range(0,3);
        else if(stage == 2) randomEnemy = Random.Range(3,4);
        else if(stage == 3) randomEnemy = Random.Range(4,6);

        float randomX = Random.Range(-1.7f, 1.7f);
        float randomY=Random.Range(-1.4f,MaxPosition.y -2f);
        float randomDelay1 = Random.Range(3f, 10f);
         yield return new WaitForSeconds(randomDelay1);

         if (randomEnemy == 4)  {Instantiate(enemiesWithItem[randomEnemy], new Vector2(3f, randomY), Quaternion.identity);}
         else {Instantiate(enemiesWithItem[randomEnemy], new Vector2(randomX, 6f), Quaternion.identity);}

         yield return new WaitForSeconds(randomDelay1);
         StartCoroutine(randomItemEnemySpawn());
    }
    #endregion

    #region 바람 생성
    private IEnumerator SpawnWind()
    {
        if(wind)
        {
            while(true){
            InstantiateWind();
            yield return new WaitForSeconds(windDealy);
            }
        }
    }
    private GameObject InstantiateWind()
    {
        GameObject resul = null;
        if (windManager.transform.childCount > 0 && wind)
        {
            resul = windManager.transform.GetChild(0).gameObject;
            resul.transform.position = new Vector2(0f, 5);
            resul.transform.SetParent(null);
            resul.SetActive(true);
        }
        else if(wind)
        {
            GameObject Wind = Instantiate(windPrefab);
            Wind.transform.position = new Vector2(0f, 5);
            Wind.transform.SetParent(null);
            resul = Wind;
        }
        return resul;
    }
    #endregion
    #region 스테이지
    public void SetLhigh()
    {
        Lhigh = score;
    }
    public void lavaStage()
    {
        StageText.text = string.Format("휴화산");
        spawnHelpEnemy(enemyFlameMob, 3123123f, 6f);
        StartCoroutine(LS());
    }
    private IEnumerator LS()
    {
        yield return new WaitForSeconds (3f);
        wind = false;
        Instantiate(Fog, FogPosition, Quaternion.identity);
        stage = 2;
        yield return new WaitForSeconds (2.5f);
        StartCoroutine(backGroundMove.NextLavaStage());
        yield return new WaitForSeconds (1f);
        StartCoroutine(FadeStage());
    }
    #endregion
    private void Hack()
    {
        life = 99;
        playerMove.Hack();
    }
    public void SeaStage()
    {
        StageText.text = string.Format("깊은 심해");
        StartCoroutine(SS());
    } 
    private IEnumerator SS()
    {
        yield return new WaitForSeconds (3f);
        wind = false;
        Instantiate(Fog, FogPosition, Quaternion.identity);
        stage = 3;
        yield return new WaitForSeconds (2.5f);
        StartCoroutine(backGroundMove.NextSeaStage());
        yield return new WaitForSeconds (1f);
        StartCoroutine(FadeStage());
    }
    public void SkyStage()
    {
        StageText.text = string.Format("세상의 끝, 눈의 시작");
        StartCoroutine(SK());
    } private IEnumerator SK()
    {
        yield return new WaitForSeconds (3f);
        wind = true;
        Instantiate(Fog, FogPosition, Quaternion.identity);
        stage = 4;
        yield return new WaitForSeconds (2.5f);
        StartCoroutine(backGroundMove.NextSkyStage());
        yield return new WaitForSeconds (1f);
        StartCoroutine(FadeStage());
    }
}
