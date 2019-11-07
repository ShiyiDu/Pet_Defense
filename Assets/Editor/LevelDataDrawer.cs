using UnityEditor;
using UnityEngine;

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(PetLevel.SpawnInfo))]
public class LevelDataDrawer : PropertyDrawer
{
    //public override VisualElement CreatePropertyGUI(SerializedProperty property)
    //{
    //    // Create property container element.
    //    var container = new VisualElement();

    //    // Create property fields.
    //    var time = new PropertyField(property.FindPropertyRelative("time"));
    //    var type = new PropertyField(property.FindPropertyRelative("type"));
    //    var spot = new PropertyField(property.FindPropertyRelative("spot"));

    //    // Add fields to the container.
    //    container.Add(time);
    //    container.Add(type);
    //    container.Add(spot);

    //    return container;
    //}

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x - 10, position.y, 60, position.height);
        var unitRect = new Rect(position.x + 55, position.y, 30, position.height);
        var nameRect = new Rect(position.x + 90, position.y, 30, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("time"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("type"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("spot"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}