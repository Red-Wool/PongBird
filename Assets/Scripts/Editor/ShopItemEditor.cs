using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShopItemEditor))]
public class ShopItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    }

}
