using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(List<List<GameObject>>))]
public class ListOfListDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 绘制标签
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // 计算每个元素的显示空间
        float height = EditorGUIUtility.singleLineHeight;
        float buttonWidth = 20f;
        float fieldWidth = (position.width - buttonWidth * 3) / 2;

        // 绘制每个内部列表
        for (int i = 0; i < property.arraySize; i++)
        {
            SerializedProperty innerList = property.GetArrayElementAtIndex(i);
            EditorGUI.indentLevel++;
            Rect innerRect = new Rect(position.x, position.y + i * height, position.width, height);

            // 绘制添加按钮
            if (GUILayout.Button("Add", GUILayout.Width(buttonWidth)))
            {
                innerList.InsertArrayElementAtIndex(innerList.arraySize);
            }

            // 绘制删除按钮
            if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
            {
                innerList.DeleteArrayElementAtIndex(innerList.arraySize - 1);
            }

            // 绘制内部列表字段
            for (int j = 0; j < innerList.arraySize; j++)
            {
                SerializedProperty item = innerList.GetArrayElementAtIndex(j);
                Rect itemRect = new Rect(innerRect.x + (j * fieldWidth), innerRect.y, fieldWidth, innerRect.height);
                EditorGUI.PropertyField(itemRect, item, GUIContent.none);
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int rows = property.arraySize * 2; // 每个内部列表占用两行
        return base.GetPropertyHeight(property, label) * rows;
    }
}