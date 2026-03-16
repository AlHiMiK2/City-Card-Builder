using UnityEngine;
using UnityEditor;
using System.IO;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "New Card DB", menuName = "Create Card DB", order = 0)]
    public class CardConfigDatabase : ScriptableObject
    {
        [SerializeField] private CardConfig[] _cardConfigs;

        public CardConfig[] GetConfigs()
        {
            return _cardConfigs;
        }
        
#if UNITY_EDITOR
        public void LoadCardConfigsFromFolder()
        {
            string folderPath = GetFolderPath();
            
            string[] guids = AssetDatabase.FindAssets("t:CardConfig", new[] { folderPath });
            
            _cardConfigs = new CardConfig[guids.Length];
            
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                _cardConfigs[i] = AssetDatabase.LoadAssetAtPath<CardConfig>(path);
            }
            
            EditorUtility.SetDirty(this);
            Debug.Log($"Loaded {_cardConfigs.Length} card configs from {folderPath}");
        }

        private string GetFolderPath()
        {
            string assetPath = AssetDatabase.GetAssetPath(this);
            return Path.GetDirectoryName(assetPath);
        }

        [CustomEditor(typeof(CardConfigDatabase))]
        public class CardConfigDatabaseEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                CardConfigDatabase database = (CardConfigDatabase)target;

                EditorGUILayout.Space(10);
                
                if (GUILayout.Button("Load Card Configs from Folder", GUILayout.Height(30)))
                {
                    database.LoadCardConfigsFromFolder();
                }

                EditorGUILayout.Space(5);
                
                if (GUILayout.Button("Clear Array", GUILayout.Height(25)))
                {
                    database._cardConfigs = new CardConfig[0];
                    EditorUtility.SetDirty(database);
                }
            }
        }
#endif
    }
}