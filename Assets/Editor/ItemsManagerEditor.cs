using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ItemsManager))]
public class ItemsManagerEditor : Editor 
{
    private Vector2 _scrollPosition;
    private static string itemName;
    private ItemsManager self;
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        CreateItemsManagement();
        ExaminedItemsManagement();

    }

    private void CreateItemsManagement()
    {
        itemName = EditorGUILayout.TextField(itemName);
        if(GUILayout.Button("Add object to inventory"))
        {
            self.InstantiateObject(itemName);
        }
        if(GUILayout.Button("Remove object from inventory"))
        {
            self.RemoveItem(itemName, true);
        }
        if(GUILayout.Button("Clear Inventory"))
        {
            self.ClearInventory(true);
        }
    }

    private void ExaminedItemsManagement()
    {
        GUILayout.BeginVertical();
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true, GUILayout.ExpandHeight(true)); 

        self.GetInventory().ForEach(item => 
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(item.itemData.itemName, GUILayout.MaxWidth(120f));
            item.itemData.isExamined = EditorGUILayout.Toggle("isExamined",item.itemData.isExamined);
            EditorGUILayout.EndHorizontal();
        });

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
    private void OnEnable() 
    {
        self = target as ItemsManager;
    }
}
