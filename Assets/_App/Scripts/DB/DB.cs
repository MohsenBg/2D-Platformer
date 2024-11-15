using System.Collections;
using System.Collections.Generic;
using System.IO;
using SQLite4Unity3d;
using UnityEngine;

public class DB
{
    private const string DBBaseDir = "DB";
    private string DatabaseName = "GameDataBase.db";

    private SQLiteConnection _connection;

    public DB()
    {
#if UNITY_EDITOR
        var dbPath = Path.Combine("Assets", DBBaseDir, DatabaseName);
#else
        var filepath = Path.Combine(Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");

            string loadDbPath;
#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + CustomAssetsDirectory + "/" + DatabaseName);
            while (!loadDb.isDone) { }  // CAREFUL here, consider adding a timeout and error check
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS || UNITY_WP8 || UNITY_WINRT || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            loadDbPath = Path.Combine(Application.dataPath, GetPlatformSpecificPath(), CustomAssetsDirectory, DatabaseName);
            File.Copy(loadDbPath, filepath);
#else
            loadDbPath = Path.Combine(Application.dataPath, CustomAssetsDirectory, DatabaseName);
            File.Copy(loadDbPath, filepath);
#endif
            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
    }


    #region GetPatformSpecificPath 
#if !UNITY_EDITOR
    private string GetPlatformSpecificPath()
    {
#if UNITY_IOS
        return "Raw";
#elif UNITY_WP8 || UNITY_WINRT || UNITY_STANDALONE_LINUX
        return CustomAssetsDirectory;
#elif UNITY_STANDALONE_OSX
        return Path.Combine("Resources", "Data", CustomAssetsDirectory);
#else
        return CustomAssetsDirectory;
#endif
    }
#endif

    #endregion


    public SQLiteConnection GetConnection()
    {
        return _connection;
    }
}

