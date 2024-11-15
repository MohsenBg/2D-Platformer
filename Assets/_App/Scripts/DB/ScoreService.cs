using System;
using System.Collections.Generic;
using System.Linq;

public class ScoreService
{
    private readonly DB _db;

    public ScoreService()
    {
        _db = new DB();
    }

    public void CreateDB()
    {
        if (!TableExists<ScoreModel>())
        {
            ExecuteDbOperation(() =>
            {
                _db.GetConnection().CreateTable<ScoreModel>();
            }, "Error while creating the table.");
        }
        else
        {
            Console.WriteLine("Table already exists.");
        }
    }

    public void DropTable()
    {
        if (TableExists<ScoreModel>())
        {
            ExecuteDbOperation(() =>
            {
                _db.GetConnection().DropTable<ScoreModel>();
            }, "Error while dropping the table.");
        }
        else
        {
            Console.WriteLine("Table does not exist.");
        }
    }

    public List<ScoreModel> GetAll()
    {
        return ExecuteDbOperation(() =>
        {
            return _db.GetConnection().Table<ScoreModel>().ToList();
        }, "Error while retrieving all scores.");
    }

    public List<ScoreModel> GetAllByLevel(int level)
    {

        return ExecuteDbOperation(() =>
        {
            return _db.GetConnection().Table<ScoreModel>().Where(s => s.Level == level).ToList();
        }, $"Error while retrieving scores for level {level}.");
    }

    public int GetMaxLevel()
    {
        return ExecuteDbOperation(() =>
        {
            var tableName = typeof(ScoreModel).Name;
            var query = $"SELECT Max(Level) FROM {tableName};";
            var result = _db.GetConnection().ExecuteScalar<string>(query);

            return string.IsNullOrEmpty(result) ? 0 : int.Parse(result);
        }, "Error while finding the max.");
    }

    public void Remove(ScoreModel scoreModel)
    {
        ExecuteDbOperation(() =>
        {
            _db.GetConnection().Delete(scoreModel);
        }, "Error while deleting the score.");
    }

    public void Insert(ScoreModel scoreModel)
    {
        ExecuteDbOperation(() =>
        {
            _db.GetConnection().Insert(scoreModel);
        }, "Error while inserting the score.");
    }

    private void ExecuteDbOperation(Action operation, string errorMessage)
    {
        try
        {
            operation();
        }
        catch (Exception ex)
        {
            HandleException(ex, errorMessage);
        }
    }

    private T ExecuteDbOperation<T>(Func<T> operation, string errorMessage)
    {
        try
        {
            return operation();
        }
        catch (Exception ex)
        {
            HandleException(ex, errorMessage);
            return default;
        }
    }

    private void HandleException(Exception ex, string errorMessage)
    {
        Console.WriteLine($"{errorMessage} Exception details: {ex.Message}");
    }

    private bool TableExists<T>()
    {
        var tableName = typeof(T).Name;
        var query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";

        var result = _db.GetConnection().ExecuteScalar<string>(query);
        return !string.IsNullOrEmpty(result);
    }

}

