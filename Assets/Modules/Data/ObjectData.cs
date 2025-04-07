using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectData", menuName = "Data/ObjectData")]
public class ObjectData : ScriptableObject
{
    [Header("Object Identification")]
    public string objectName; // 物体的唯一标识名称

    [Header("Highlight Settings")]
    public bool canBeHighlighted = true; // 是否可以高亮
    public Color highlightColor = Color.red; // 高亮时的颜色

    [Header("UI Settings")]
    public bool showUI = true; // 是否显示 UI
    public GameObject uiPrefab; // 对应的 UI 预制体

    [Header("Pickup Settings")]
    public bool canBePickedUp = true; // 是否可以被拾取

    [Header("makeup settings")]
    public bool canBemakeup = false;//是否可以被组装

    [Header("weight settings")]
    public int weight;//重量

    [Header("wending settings")]
    public int wnding;//稳定性

    [Header("showed settings")]
    public bool isshowed;//是否可以展示


    [Header("have been picked")]
    public bool havebeenpicked=false;//是否被拾取过

    [Header("have been printed")]
    public bool havebeenprinted = false;//是否被打印过

    [Header("have been qiege")]
    public bool havebeenqiege = false;//是否被拾取过
}