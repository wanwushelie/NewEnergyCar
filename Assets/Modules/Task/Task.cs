using System;

[System.Serializable]
public class Task
{
    public string id;
    public string description;
    public bool isCompleted;

    public Task(string id, string description)
    {
        this.id = id;
        this.description = description;
        this.isCompleted = false;
    }
}