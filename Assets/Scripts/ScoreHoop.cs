using Normal.Realtime;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreHoop : MonoBehaviour
{
    [SerializeField] private int _scoreDifficulty = 1;
    [SerializeField] private Scoreboard _scoreboard = null;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj == null || obj.CompareTag("Ball") == false) { return; }

        RealtimeView view = obj.GetComponent<RealtimeView>();
        if (view == null || view.isOwnedLocallyInHierarchy == false) { return; }

        uint id = (uint)view.realtime.clientID;

        _scoreboard.AddScore(id, _scoreDifficulty);
    }
}
