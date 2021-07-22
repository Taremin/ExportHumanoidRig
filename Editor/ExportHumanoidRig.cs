namespace ExportHumanoidRig
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    [System.Serializable]
    public class ExportHumanoidRigWindow : EditorWindow
    {
        private const string version = "0.0.1";
        private GameObject obj = null;

        [MenuItem("GameObject/ExportHumanoidRig", false, 20)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ExportHumanoidRigWindow));
        }

        private string JsonEscape(string str)
        {
            var tmp = string.Copy(str);
        
            /*
                RFC 8259: The JavaScript Object Notation (JSON) Data Interchange Format
                https://datatracker.ietf.org/doc/html/rfc8259
                %x22 /          ; "    quotation mark  U+0022
                %x5C /          ; \    reverse solidus U+005C
                %x2F /          ; /    solidus         U+002F
                %x62 /          ; b    backspace       U+0008
                %x66 /          ; f    form feed       U+000C
                %x6E /          ; n    line feed       U+000A
                %x72 /          ; r    carriage return U+000D
                %x74 /          ; t    tab             U+0009
            */
            tmp = tmp.Replace("\u005C", @"\\");
            tmp = tmp.Replace("\u0022", @"\""");
            tmp = tmp.Replace("\u002F", @"\/");
            tmp = tmp.Replace("\u0008", @"\b");
            tmp = tmp.Replace("\u000C", @"\f");
            tmp = tmp.Replace("\u000A", @"\n");
            tmp = tmp.Replace("\u000D", @"\r");
            tmp = tmp.Replace("\u0009", @"\t");
            
            return "\"" + tmp + "\"";
        }

        private string ConvertAvatarToJSON()
        {
            var animator = obj.GetComponent<Animator>();
            var dictionary = CreateEnumDictionary<HumanBodyBones>();
            var output = "{\n";
            var lines = new List<string>();
            foreach (var kvp in dictionary)
            {
                var bone = kvp.Value;
                if (bone == HumanBodyBones.LastBone)
                {
                    continue;
                }
                var transform = animator.GetBoneTransform(kvp.Value);
                lines.Add("  " + JsonEscape(kvp.Key) + ": " + (transform ? JsonEscape(transform.name) : "null"));
            }
            output += string.Join(",\n", lines) + "\n";
            output += "}\n";
            return output;
        }

        static public Dictionary<string, EnumType> CreateEnumDictionary<EnumType>()
        {
            return System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToDictionary(t => t.ToString(), t => t);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Version:", version);
            obj = (GameObject)EditorGUILayout.ObjectField("GameObject", obj, typeof(GameObject), true);
            if (GUILayout.Button("Export"))
            {
                Debug.Log("ExportHumanoidRig: Start");
                var json = ConvertAvatarToJSON();
                var path = Application.dataPath + "/" + obj.name + ".json";
                Debug.Log("ExportHumanoidRig: Writing to File: " + path);
                System.IO.File.WriteAllText(path, json);
                Debug.Log("ExportHumanoidRig: Done");
            }
        }
    }
}