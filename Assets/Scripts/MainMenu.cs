using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private RectTransform scoreRectTransform;

    private void Start()
    {
        scoreRectTransform.anchoredPosition = new Vector2(scoreRectTransform.anchoredPosition.x, 60);

        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.2f, 1.2f), 0.4f).setLoopPingPong();
    }
    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f).setOnComplete(onComplete);
    }

    private void onComplete()
    {
        LeanTween.moveY(scoreRectTransform, -40f, 0.75f).setEaseOutBounce();

        gameManager.Enable();
        Destroy(gameObject);
    }
}
