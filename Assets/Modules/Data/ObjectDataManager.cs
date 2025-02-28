using UnityEngine;
using System.Collections.Generic;

public class ObjectDataManager : MonoBehaviour
{
    // 单例模式
    public static ObjectDataManager Instance { get; private set; }

    // 存储所有 ObjectData 的字典，通过 objectName 快速查找
    private Dictionary<string, ObjectData> objectDataDictionary = new Dictionary<string, ObjectData>();

    // 在 Inspector 中暴露的 ObjectData 列表
    [SerializeField] private List<ObjectData> objectDataList = new List<ObjectData>();

    private void Awake()
    {
        // 确保只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保持实例
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化数据字典
        InitializeDataDictionary();
    }

    // 初始化数据字典
    private void InitializeDataDictionary()
    {
        foreach (var data in objectDataList)
        {
            if (data != null && !string.IsNullOrEmpty(data.objectName))
            {
                objectDataDictionary[data.objectName] = data;
            }
        }
    }

    // 根据 objectName 获取对应的 ObjectData
    public ObjectData GetData(string objectName)
    {
        if (objectDataDictionary.TryGetValue(objectName, out ObjectData data))
        {
            return data;
        }
        else
        {
            Debug.LogWarning($"ObjectData for '{objectName}' not found.");
            return null;
        }
    }

    // 可选：添加新的 ObjectData（运行时动态添加）
    public void AddData(ObjectData newData)
    {
        if (newData != null && !string.IsNullOrEmpty(newData.objectName))
        {
            objectDataDictionary[newData.objectName] = newData;
            objectDataList.Add(newData);
        }
    }

    // 可选：移除 ObjectData（运行时动态移除）
    public void RemoveData(string objectName)
    {
        if (objectDataDictionary.ContainsKey(objectName))
        {
            objectDataDictionary.Remove(objectName);
            objectDataList.RemoveAll(data => data.objectName == objectName);
        }
    }
}