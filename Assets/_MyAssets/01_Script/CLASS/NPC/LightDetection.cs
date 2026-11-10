using System;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [SerializeField] Light _light;
    [SerializeField] ConeDetection _detector;

    private void Awake()
    {
        if (_light == null) _light = GetComponent<Light>();
        if (_light == null) this.enabled = false;
        
        if (_detector == null) _detector = GetComponent<ConeDetection>();
        if (_detector == null) this.enabled = false;
    }

    private void Update()
    {
        if (!_detector.GetDetectionState()) _light.color = Color.softYellow;
        else _light.color = Color.red;
    }
}
