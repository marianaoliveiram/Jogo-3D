using UnityEngine;
using Unity.Cinemachine;
using MyGameManager = GameManager;

public class Player : MonoBehaviour
{

    public float forceMultiplier = 10f;
    public float maximumVelocity = 4f;
    private Rigidbody rb;
    public ParticleSystem deathParticles;
    private CinemachineImpulseSource cinemachineImpulseSource;
    public GameObject cinemachineCamera;
    public GameObject zoomCamera;

    void Start()
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            MyGameManager.GameOver();
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();

            cinemachineCamera.SetActive(false);
            zoomCamera.SetActive(true);
        }
    }
}
