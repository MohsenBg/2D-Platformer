using System;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

[System.Serializable]
public class ScoreModel
{


    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int Level { get; set; }


    public float TimeTaken { get; set; }

    public int CoinsCollected { get; set; }

    public override string ToString()
    {
        return string.Format("[id: Id={0},  level={1}, TimeTaken={2}, CoinsCollected={3}]", Id, Level, TimeTaken, CoinsCollected);
    }
}

