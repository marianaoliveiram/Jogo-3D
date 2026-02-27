using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hazardPrefab;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private Image backgroundMenu;
    [SerializeField]
    
    private GameObject cinemachineCamera;
    [SerializeField]
    private GameObject zoomCamera;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject player;

    private int score;
    private float timer;
    private bool gameOver;
    private Coroutine hazardsCoroutine;
    private int highScore;

    private static GameManager instance;
    private const string HighScorePreferenceKey = "HighScore";

    public static GameManager Instance => instance;
    public int HighScore => highScore;

    void Start()
    {
        instance = this;

        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);
        Debug.Log(highScore);
    }

    private void OnEnable()
    {
        player.SetActive(true);

        zoomCamera.SetActive(false);
        cinemachineCamera.SetActive(true);

        gameOver = false;
        score = 0;
        scoreText.text = "0";
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                UnPause();
            }  

            if(Time.timeScale == 1)
            {
                Pause();
            }
        }

        if(gameOver)
            return;

        timer += Time.deltaTime;

        if(timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();

            timer = 0;
        }
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * value;
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1,3);

        for (int i = 0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-7, 7);
            var linearDamping = Random.Range(0f, 3f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, -1.35f), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().linearDamping = linearDamping;
        }
        
        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        gameOver = true;

        if(Time.timeScale < 1)
        {
            UnPause();
        }

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScorePreferenceKey, highScore);
            Debug.Log(highScore);
        }

        cinemachineCamera.SetActive(false);
        zoomCamera.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    private void Pause()
    {
        LeanTween.value(1, 0, 0.75f)
                            .setOnUpdate(SetTimeScale)
                            .setIgnoreTimeScale(true);
        backgroundMenu.gameObject.SetActive(true);
    }

    private void UnPause()
    {
        LeanTween.value(Time.timeScale, 1, 0.75f)
                        .setOnUpdate(SetTimeScale)
                        .setIgnoreTimeScale(true);
        backgroundMenu.gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
