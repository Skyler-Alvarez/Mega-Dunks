using Normal.Realtime;
using Normal.Realtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RealtimeModel]
public partial class Scores
{
    [RealtimeProperty(1, true, true)]
    public int _score;
}

[RealtimeModel]
public partial class UserScores
{
    [RealtimeProperty(1, true, true)]
    public RealtimeDictionary<Scores> _userScores;
}

//public class ScoreSync : RealtimeComponent<Scores>
//{
//    protected override void OnRealtimeModelReplaced(Scores previousModel, Scores currentModel)
//    {
//        Scoreboard.Instance.UpdateScores();
//    }
//}

public class Scoreboard : RealtimeComponent<UserScores>
{
    [SerializeField] private List<GameObject> _scoreObjects = new List<GameObject> ();
    [SerializeField] private List<TextMeshPro> _scoreTexts = new List<TextMeshPro> ();

    private static Scoreboard _instance = null;
    public static Scoreboard Instance { get; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Update()
    {
        UpdateScores();
    }

    public void AddScore(uint id, int score)
    {
        if (model.userScores == null) { return; }

        if (model.userScores.ContainsKey(id) == false)
        {
            var s = new Scores();
            s.score = score;
            model.userScores.Add(id, s);
        }
        else
        {
            var s = new Scores();
            s.score = score + model.userScores[id].score;
            model.userScores[id] = s;
        }
        UpdateScores();
    }

    protected override void OnRealtimeModelReplaced(UserScores previousModel, UserScores currentModel)
    {
        UpdateScores();
    }

    public void UpdateScores()
    {
        for (int i = 0; i < model.userScores.Count; i++)
        {
            _scoreObjects[i].SetActive(true);
        }

        int textIndex = 0;
        foreach (var player in model.userScores)
        {
            _scoreTexts[textIndex].text = $"Player {player.Key}: {model.userScores[player.Key].score}";
            textIndex++;
        }
    }
}
