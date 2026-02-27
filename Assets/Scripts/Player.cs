using UnityEngine;
using Unity.Cinemachine;
using MyGameManager = GameManager;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplier = 10f;
    [SerializeField]
    private float maximumVelocity = 4f;

    private Rigidbody rb;

    [SerializeField]
    private ParticleSystem deathParticles;
    private CinemachineImpulseSource cinemachineImpulseSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if(GameManager.Instance == null)
        {
            return;
        }
        
        var horizontalInput = Input.GetAxis("Horizontal");

        if(rb.linearVelocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, 0.768f, -1.35f);
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            GameOver();

            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("FallDown"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        MyGameManager.Instance.GameOver();

        gameObject.SetActive(false);
    }
}
