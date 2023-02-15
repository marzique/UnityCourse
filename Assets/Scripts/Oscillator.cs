using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // params
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // state
    float movementFactor;
    Vector3 startingPosotion;

    void Start(){
        startingPosotion = transform.position;
    }

    void Update(){
        if (period <= MathF.Epsilon){ return; }

        const float tau = Mathf.PI * 2;

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosotion + offset;
    }
}
