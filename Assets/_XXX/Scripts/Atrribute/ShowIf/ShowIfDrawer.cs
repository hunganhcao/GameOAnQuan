#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace XXX.Attribute
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        private float height = 0;
        private const int SpaceLeft = 30;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showif = (ShowIfAttribute)attribute;
            var path = property.propertyPath;
            var newPath = path.Substring(0, path.LastIndexOf(property.name)) + showif.Condition;
            var a = property.serializedObject.FindProperty(newPath);
            try
            {
                var value = a.intValue;
                if (value == Convert.ToInt32(showif.Value))
                {
                    DrawProperty();
                }
                else
                {
                    height = 0;
                }
            }
            catch
            {
                DrawProperty();
                Debug.Log("Thuộc tính điều hướng không phải kiểu int");
            }
            void DrawProperty()
            {
                var labelRect = new Rect(position.x + SpaceLeft, position.y, position.width, 16);
                EditorGUI.PropertyField(labelRect, property, label);
                height = EditorGUI.GetPropertyHeight(property);
            }
        }

    }
}
#endif