using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightController : MonoBehaviour
{
    new Light light;
    [Header("Flickering")]
    public bool enableFlickering = false;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 1;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 50;

    [Tooltip("Adjust light intensity range")]
    [Range(0, 10)]
    public int multiplyer = 1;

    [Tooltip("How much to smooth out the randomness")]
    [Range(1, 50)]
    public int smoothing = 5;
    Queue<float> smoothQueue;
    float lastSum = 0;


    [Header("Movement")]
    public bool enableWiggle = false;
    public float amplitudeX = 1f; 
    public float frequencyX = 1f; 
    public float amplitudeY = 1f; 
    public float frequencyY = 1f; 
    public float amplitudeZ = 1f; 
    public float frequencyZ = 1f;

    private Vector3 refPos;



    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        light = GetComponent<Light>();

        refPos = transform.position;

    }

    void Update()
    {
        if (enableFlickering) { Flicker(); };
        if (enableWiggle) { Move(); };
    }

    void Flicker()
    {
        //Applying Light Flickering
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count * multiplyer;

    }

    void Move()
    {
        float dx = Random.Range(0, 1f) *amplitudeX * (Mathf.PerlinNoise(Time.time * frequencyX, 1f) - 0.5f);
        float dy = Random.Range(0, 1f) * amplitudeY * (Mathf.PerlinNoise(1f, Time.time * frequencyY) - 0.5f);
        float dz = Random.Range(0, 1f) * amplitudeZ * (Mathf.PerlinNoise(Time.time * frequencyZ, 1f) - 0.5f);

        Vector3 pos = new Vector3(refPos.x, refPos.y, refPos.z);
        pos += transform.right * dx;
        pos += transform.up * dy;
        pos += transform.forward * dz;
        transform.position = pos;
    }

}