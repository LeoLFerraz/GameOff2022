using UnityEngine;
using System.Collections.Generic;

public class LightFlickeringEffect : MonoBehaviour {
    [SerializeField] public bool MoveLight;
    [SerializeField] public Vector3 MoveFactor = new Vector3(0.1f, 0.0f, 0.1f);
    protected Vector3 MoveAnchor;
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new Light light;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;

    public void Reset() {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start() {
         smoothQueue = new Queue<float>(smoothing);
         if (light == null) {
            light = GetComponent<Light>();
         }

         MoveAnchor = light.transform.position;
    }

    void Update() {
        if (light == null)
            return;

        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
        if (MoveLight) {
            Vector3 posDelta = Vector3.zero;
            posDelta.x = (lastSum / (float)smoothQueue.Count) * MoveFactor.x;
            posDelta.y = (lastSum / (float)smoothQueue.Count) * MoveFactor.y;
            posDelta.z = (lastSum / (float)smoothQueue.Count) * MoveFactor.z;
            light.transform.position = MoveAnchor + posDelta;
        }
    }

}