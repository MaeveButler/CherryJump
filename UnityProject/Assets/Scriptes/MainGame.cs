using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGame : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject scoreDuringGame;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreDuringGameText;
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private TextMeshProUGUI[] topTenScores;
    
    [Header("Game")]
    [SerializeField] private GameObject background;
    [SerializeField] private float backgroundWidth = 12f;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 playerStart;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip gameOverAud;
    [SerializeField] private AudioClip gameWonAud;
    [SerializeField] private AudioClip backgroundStartAud;
    [SerializeField] private AudioClip backgroundLoopAud;

    [Header("Game Design")]
    [Range(0f, 10f)] public float levelSpeed;
    [SerializeField] private int maxLevelSpeed = 8;
    [Range(1, 10)] [SerializeField] private float timeBetweenPlusScore;
    [SerializeField] private int scoreChangePerSeconds;
    [SerializeField] private float timerDifficultyChange;

    private LevelPattern levelPattern;
    private AudioSource audioSource;

    [HideInInspector] public  bool gameRunning = false;

    
    private float cameraBorderHorizontal;
    public float GetCameraBorderHorizontal() { return cameraBorderHorizontal; }


    private float groundSpawningPoint;
    public float GetGroundSpawningPoint() { return groundSpawningPoint - 0.5f; }
    private List<GameObject> backgroundCopys = new List<GameObject>();


    private int score;
    private List<int> scoreList = new List<int>();
    
    
    private void Start()
    {
        levelPattern = GetComponent<LevelPattern>();
        audioSource = GetComponent<AudioSource>();

        LoadScoreList();

        StartBackgroundMusic();
        ActivateStartPanel();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && startPanel.activeSelf == true)
        {
            StartGame();
        }

        if (Input.GetKeyUp(KeyCode.Return) && scorePanel.activeSelf == true)
        {
            ActivateStartPanel();
        }
    
        if (gameRunning)
        {
            ReallignScorePanel();
            CheckBackgroundPos();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartMainMenu()
    {
        gameRunning = false;
        ActivateStartPanel();
        StartBackgroundMusic();
    }
    
    public void StartGame()
    {
        cameraBorderHorizontal = backgroundWidth / 2f;
        StartBackgroundMoving();

        score = 0;
        Score(score);
        scoreDuringGame.SetActive(true);
        StartCoroutine(ScorePerSeconds());
        StartCoroutine(ChangeLevelDifficulty());
        
        startPanel.SetActive(false);
        player.GetComponent<Player>().MyStart();
        
        gameRunning = true;

        levelPattern.MyStart();
        player.GetComponent<Player>().MyStart();
    }

    private void ReallignScorePanel()
    {
        Vector2 targetRatio = FindObjectOfType<LetterboxCamera.ForceCameraRatio>().ratio;

        int gcd = GetGreatestCommonDivider(Screen.width, Screen.height);
        Vector2 currentAspectRatio = new Vector2Int(Screen.width / gcd, Screen.height / gcd);


        if (currentAspectRatio.x > currentAspectRatio.y)
        {
            Vector2 modifiedTargetRatio = targetRatio * currentAspectRatio.y;
            Vector2 modifiedCurrentRatio = currentAspectRatio * targetRatio.y;

            float modifier = modifiedTargetRatio.x / modifiedCurrentRatio.x;


            float canvasWidth = scoreDuringGame.GetComponent<RectTransform>().rect.width;
            float xPosition = Mathf.RoundToInt(canvasWidth - (canvasWidth * modifier)) / 2f;

            scoreDuringGame.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, 0f);
        }
        else
        {
            Vector2 modifiedTargetRatio = targetRatio * currentAspectRatio.x;
            Vector2 modifiedCurrentRatio = currentAspectRatio * targetRatio.x;

            float modifier = modifiedTargetRatio.y / modifiedCurrentRatio.y;


            float canvasHeight = scoreDuringGame.GetComponent<RectTransform>().rect.height;
            float yPosition = Mathf.RoundToInt(canvasHeight - (canvasHeight * modifier)) / 2f;

            scoreDuringGame.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -yPosition);
        }
    }
    
    private int GetGreatestCommonDivider(int num1, int num2)
    {
        int remainder;

        while (num2 != 0)
        {
            remainder = num1 % num2;
            num1 = num2;
            num2 = remainder;
        }

        return num1;
    }

    public void GameOver()
    {
        ActivateLosePanel();
        GameStop(true);
    }
    public void GameWon()
    {
        ActivateWinPanel();
        GameStop(false);
    }
    private void GameStop(bool gameOver)
    {
        gameRunning = false;

        audioSource.loop = false;

        StopAllCoroutines();
        
        for (int i = 0; i < backgroundCopys.Count; i++)
            Destroy(backgroundCopys[i]);
        
        levelPattern.DestroyAllLevelObjects();

        StartCoroutine(FadeOut(0.2f, gameOver));
    }

    private IEnumerator ChangeLevelDifficulty()
    {
        yield return new WaitForSeconds(timerDifficultyChange);

        if (levelSpeed >= maxLevelSpeed)
            StopCoroutine(ChangeLevelDifficulty());
        else
        {
            levelSpeed += 0.5f;
            scoreChangePerSeconds += 10;
            timerDifficultyChange += 20;
            StartCoroutine(ChangeLevelDifficulty());
        }
    }
    
    private IEnumerator ScorePerSeconds()
    {
        yield return new WaitForSeconds(timeBetweenPlusScore);

        Score(scoreChangePerSeconds);
        StartCoroutine(ScorePerSeconds());

        yield return null;
    }

    #region Score
    public void Score(int value)
    {
        score += value;
        scoreDuringGameText.text = score.ToString();
        
        if (score >= 999999999)
        {
            GameWon();
        }
    }
    
    private void DisplayTopScores()
    {
        yourScoreText.text = score.ToString();

        SortScoreList(ref scoreList);

        for (int i = 0; i < scoreList.Count; i++)
        {
            if (scoreList[i] < 0)
                topTenScores[i].text = "-";
            else
                topTenScores[i].text = scoreList[i].ToString();
        }
        ActivateScorePanel();
    }
    private void SortScoreList(ref List<int> scoreList)
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            if (score == scoreList[i])
                return;

            if (score > scoreList[i])
            {
                for (int x = 0; x < scoreList.Count - 1 - i; x++)
                {
                    scoreList[scoreList.Count - 1 - x] = scoreList[scoreList.Count - 2 - x];
                }

                scoreList[i] = score;
                return;
            }
        }
        SaveScoreList();
    }

    private void SaveScoreList()
    {
        string scoreData = null;

        for (int i = 0; i < scoreList.Count; i++)
        {
            if (i < scoreList.Count - 1)
            {
                scoreData += scoreList[i].ToString() + "%";
            }
            else
            {
                scoreData += scoreList[i].ToString();
            }
        }
        PlayerPrefs.SetString("scoreData", scoreData);
    }
    private void LoadScoreList()
    {
        string scoreData = PlayerPrefs.GetString("scoreData", "Leer");

        for (int i = 0; i < topTenScores.Length; i++)
            scoreList.Add(-i);

        if (scoreData != "Leer")
        {
            string[] splittedScoreData = scoreData.Split('%');
            for (int i = 0; i < splittedScoreData.Length; i++)
            {
                scoreList[i] = int.Parse(splittedScoreData[i]);
            }
        }
    }
    #endregion

    #region Panels
    private void ActivateStartPanel()
    {
        startPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        scorePanel.SetActive(false);
        scoreDuringGame.SetActive(false);

        player.SetActive(false);

        startPanel.GetComponent<UIAnim>().StartAnim();
    }
    private void ActivateWinPanel()
    {
        startPanel.SetActive(false);
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        scorePanel.SetActive(false);
        scoreDuringGame.SetActive(false);

        player.SetActive(false);

        winPanel.GetComponent<UIAnim>().StartAnim();
    }
    private void ActivateLosePanel()
    {
        startPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(true);
        scorePanel.SetActive(false);
        scoreDuringGame.SetActive(false);

        player.SetActive(false);

        losePanel.GetComponent<UIAnim>().StartAnim();
    }
    private void ActivateScorePanel()
    {
        startPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        scorePanel.SetActive(true);
        scoreDuringGame.SetActive(false);

        player.SetActive(false);

        //scorePanel.GetComponent<UIAnim>().StartAnim();
    }
    #endregion

    #region Audio
    private void StartBackgroundMusic()
    {
        audioSource.clip = backgroundStartAud;
        audioSource.Play();
        StartCoroutine(GameMusicLoop());
    }
    private IEnumerator GameMusicLoop()
    {
        float transitionTime = 1.8f;
        yield return new WaitForSeconds(audioSource.clip.length - transitionTime);

        audioSource.clip = backgroundLoopAud;
        audioSource.loop = true;
        audioSource.Play();

        yield return null;
    }

    IEnumerator FadeOut(float fadeTime, bool isGameOver)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return new WaitForEndOfFrame();
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        if (isGameOver)
        {
            audioSource.clip = gameOverAud;
            audioSource.Play();
            StartCoroutine(GameOverAudio());
        }
        else
        {
            audioSource.clip = gameWonAud;
            audioSource.Play();
            StartCoroutine(GameWonAudio());
        }
    }
    IEnumerator GameOverAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        StartBackgroundMusic();
        DisplayTopScores();
    }
    IEnumerator GameWonAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        StartBackgroundMusic();
        ActivateStartPanel();
    }
    #endregion

    #region Background
    private void StartBackgroundMoving()
    {
        backgroundCopys.Clear();
        NewBackground(0f);

        groundSpawningPoint = cameraBorderHorizontal + (backgroundWidth / 2f);
        
        NewBackground(groundSpawningPoint);
    }

    private void CheckBackgroundPos()
    {
        float spawning = (levelSpeed * 0.05f);
        
        if (backgroundCopys[backgroundCopys.Count - 1].transform.position.x <= spawning)
        {
            NewBackground(groundSpawningPoint);
        }
        if (backgroundCopys[0].transform.position.x <= backgroundWidth * -1f)
        {
            DestroyBackground(backgroundCopys[0]);
        }
    }

    private void NewBackground(float xPos)
    {
        Vector3 pos = new Vector3(xPos, 0f, 0f);
        GameObject copy = Instantiate(background, pos, Quaternion.identity);
        backgroundCopys.Add(copy);
        copy.SetActive(true);
    }
    private void DestroyBackground(GameObject background)
    {
        backgroundCopys.Remove(background);
        Destroy(background);
    }
    #endregion
}