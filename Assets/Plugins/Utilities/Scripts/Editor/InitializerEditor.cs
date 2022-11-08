using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Utils.Init;

namespace UtilsEditor
{
    [CustomEditor(typeof(Initializer), true)]
    public class InitializerEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var iterator = serializedObject.GetIterator();
            iterator.Next(true);

            int i = 0;
            while (iterator.NextVisible(false))
            {
                // string propertyName = iterator.type.Substring(6, iterator.type.Length - 7);
                string[] splits = i != 0 ? GetType(iterator, i).ToString().Split('.') : new string[1] { "Script" };
                string propertyName = iterator.name == "m_Script" ? "Script" : splits[splits.Length - 1];

                PropertyField field = new PropertyField(iterator, propertyName);
                field.SetEnabled(iterator.name != "m_Script");

                if (i == 1)
                {
                    Label label = new Label("<b>Client</b>");
                    container.Add(label);
                }
                else if (i == 2)
                {
                    Label label = new Label("<b>Arguments</b>");
                    container.Add(label);
                }

                container.Add(field);

                i++;
            }

            return container;
        }

        public static System.Type GetType(SerializedProperty property, int index)
        {
            System.Type type = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = null;
            System.Type[] args = type.BaseType.GenericTypeArguments;

            if (index >= args.Length + 1)
            {
                fi = type.GetField(property.propertyPath);
            }
            else if (index <= args.Length)
            {
                return args[index - 1];
            }
            return fi.FieldType;
        }
    }
}