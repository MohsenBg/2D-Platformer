using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public DBManager()
    {
        ScoreService = new ScoreService();
    }

    public ScoreService ScoreService { get; set; }
}
