using System;
using TMPro;
using UnityEngine;

public class LaserRestoration : MonoBehaviour
{
    [SerializeField] private float shotRestorationTime;
    [SerializeField] private TMP_Text restorationTimer;
    [SerializeField] private TMP_Text shotsAvailable;

    private bool _shotAvailable;
    private int _shotsAmount;
    private float _timeToRestore;

    private void Start()
    {
        UpdateShotsAmount(1);
        UpdateShotsCounter();
        _timeToRestore = shotRestorationTime;
    }

    private void Update()
    {
        _timeToRestore -= Time.deltaTime;
        restorationTimer.text = "New Laser Shot: " + TimeSpan.FromSeconds(_timeToRestore).ToString("mm':'ss");

        if (_timeToRestore < 0)
        {
            UpdateShotsAmount(1);
            UpdateShotsCounter();
            _timeToRestore = shotRestorationTime;
        }
    }

    private void UpdateShotsAmount(int amount)
    {
        _shotsAmount += amount;

        if (_shotsAmount > 0)
        {
            shotsAvailable.color = Color.yellow;
        }
        else
        {
            shotsAvailable.color = Color.white;
        }
    }

    public bool CanShoot()
    {
        if (_shotsAmount > 0)
        {
            UpdateShotsAmount(-1);
            UpdateShotsCounter();
            return true;
        }
        else
        {
                return false;
        }
    }

    private void UpdateShotsCounter()
    {
        shotsAvailable.text = "Laser Shots: " + _shotsAmount;
    }
}
