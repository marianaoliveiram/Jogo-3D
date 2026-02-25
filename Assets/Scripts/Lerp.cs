using System;
using UnityEngine;

public class Lerp : MonoBehaviour
{

    public Transform A;
    public Transform B;

    //[Range(0f, 1f)]
    //public float t;
    public Color ColorA;
    public Color ColorB;

    public float duration = 5f;
    private float elapsedTime = 0;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {

        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;

        transform.position = Vector3.Lerp(A.position, B.position, t);
        transform.rotation = Quaternion.Lerp(A.rotation, B.rotation, t);

        meshRenderer.material.color = Color.Lerp(ColorA, ColorB, t);
    }
}
