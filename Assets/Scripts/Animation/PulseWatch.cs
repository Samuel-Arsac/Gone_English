using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseWatch : MonoBehaviour
{
    private Material watchSprite;
    private float defaultProgessValue = 0f;
    private float newProgessValue;

    [Header("Pulse Settings")]
    [SerializeField] private float oscillationSpeed;
    [SerializeField] private float reductionRange;
    [SerializeField] private float maxPulseValue;


    // Start is called before the first frame update
    void Start()
    {
        watchSprite = GetComponent<Image>().material;
        defaultProgessValue = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        newProgessValue = defaultProgessValue * Mathf.Abs(Mathf.Cos(Time.time * oscillationSpeed)) / reductionRange;
        watchSprite.SetFloat("_Progress",newProgessValue);
    }
}
