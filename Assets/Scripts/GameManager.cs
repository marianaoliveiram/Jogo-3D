using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;
    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;
    private int score;
    private float timer;
    private static bool gameOver;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnHazards());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.5f));
                backgroundMenu.gameObject.SetActive(false);
            }  
            if(Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.5f));
                backgroundMenu.gameObject.SetActive(true);
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

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
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

    public static void GameOver()
    {
        gameOver = true;
    }
}
