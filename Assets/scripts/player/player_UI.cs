using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    private playerManager _PM;

    [SerializeField] private Slider _health;

    void Start()
    {
        _PM = GetComponentInChildren<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        setUI();
    }

    private void setUI()
    {
        _health.maxValue = _PM.getMaxHealth();
        _health.value = _PM.getHealth();
    }
}
