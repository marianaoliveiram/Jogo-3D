using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.2f, 1.2f), 0.4f).setLoopPingPong();
    }
    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f).setOnComplete(onComplete);
    }

    private void onComplete()
    {
        gameManager.Enable();
        Destroy(gameObject);
    }
}
