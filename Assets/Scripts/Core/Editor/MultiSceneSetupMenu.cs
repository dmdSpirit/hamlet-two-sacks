#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace dmdspirit.Editor.Core.Editor
{
    public static class MultiSceneSetupMenu
    {
        private readonly static int _applicationFullPathLength = Path.GetFullPath(Application.dataPath).Length + 1;

        [MenuItem("Assets/Multi Scene Setup/Create")]
        public static void CreateNewSceneSetup()
        {
            if (!TryGetSelectedFolderPathInProjectsTab(out var path))
            {
                Debug.LogError($"Could not get selected folder path in projects tab");
                return;
            }

            string assetPath = ConvertFullAbsolutePathToAssetPath(Path.Combine(path, "SceneSetup.asset"));

            SaveCurrentSceneSetup(assetPath);
        }

        [MenuItem("Assets/Multi Scene Setup/Create", true)]
        public static bool CreateNewSceneSetupValidate()
            => TryGetSelectedFolderPathInProjectsTab(out var _);

        [MenuItem("Assets/Multi Scene Setup/Overwrite")]
        public static void SaveSceneSetup()
        {
            if (!TryGetSelectedFilePathInProjectsTab(out var path))
            {
                Debug.LogError($"Could not get selected file path in projects tab");
                return;
            }

            string assetPath = ConvertFullAbsolutePathToAssetPath(path);

            SaveCurrentSceneSetup(assetPath);
        }

        [MenuItem("Assets/Multi Scene Setup/Load")]
        public static void RestoreSceneSetup()
        {
            if (!TryGetSelectedFilePathInProjectsTab(out var path))
            {
                Debug.LogError($"Could not get selected file path in projects tab");
                return;
            }

            string assetPath = ConvertFullAbsolutePathToAssetPath(path);

            var loader = AssetDatabase.LoadAssetAtPath<MultiSceneSetup>(assetPath);

            EditorSceneManager.RestoreSceneManagerSetup(loader.Setups);

            Debug.Log($"Scene setup '{Path.GetFileNameWithoutExtension(assetPath)}' restored");
        }

        [MenuItem("Assets/Multi Scene Setup", true)]
        public static bool SceneSetupRootValidate()
            => HasSceneSetupFileSelected();

        [MenuItem("Assets/Multi Scene Setup/Overwrite", true)]
        public static bool SaveSceneSetupValidate()
            => HasSceneSetupFileSelected();

        [MenuItem("Assets/Multi Scene Setup/Load", true)]
        public static bool RestoreSceneSetupValidate()
            => HasSceneSetupFileSelected();

        private static void SaveCurrentSceneSetup(string assetPath)
        {
            var loader = ScriptableObject.CreateInstance<MultiSceneSetup>();
            loader.Setups = EditorSceneManager.GetSceneManagerSetup();

            AssetDatabase.CreateAsset(loader, assetPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Scene setup '{Path.GetFileNameWithoutExtension(assetPath)}' saved");
        }

        private static bool HasSceneSetupFileSelected()
            => TryGetSelectedFilePathInProjectsTab(out string _);

        private static List<string> GetSelectedFilePathsInProjectsTab()
            => GetSelectedPathsInProjectsTab().Where(File.Exists).ToList();

        private static bool TryGetSelectedFilePathInProjectsTab(out string path)
        {
            path = string.Empty;
            List<string> selectedPaths = GetSelectedFilePathsInProjectsTab();

            if (selectedPaths.Count != 1)
                return false;
            path = selectedPaths[0];
            return true;
        }

        // Returns the best guess directory in projects pane
        // Useful when adding to Assets -> Create context menu
        // Returns null if it can't find one
        // Note that the path is relative to the Assets folder for use in AssetDatabase.GenerateUniqueAssetPath etc.
        private static bool TryGetSelectedFolderPathInProjectsTab(out string path)
        {
            path = string.Empty;
            List<string> selectedPaths = GetSelectedFolderPathsInProjectsTab();

            if (selectedPaths.Count != 1)
                return false;

            path = selectedPaths[0];
            return true;
        }

        // Note that the path is relative to the Assets folder
        private static List<string> GetSelectedFolderPathsInProjectsTab()
            => GetSelectedPathsInProjectsTab().Where(Directory.Exists).ToList();

        private static List<string> GetSelectedPathsInProjectsTab()
        {
            var paths = new List<string>();

            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

            foreach (Object item in selectedAssets)
            {
                string? relativePath = AssetDatabase.GetAssetPath(item);

                if (string.IsNullOrEmpty(relativePath))
                    continue;

                string fullPath =
                    Path.GetFullPath(Path.Combine(Application.dataPath, Path.Combine("..", relativePath)));

                paths.Add(fullPath);
            }

            return paths;
        }

        private static string ConvertFullAbsolutePathToAssetPath(string fullPath)
        {
            string path = Path.GetFullPath(fullPath).Remove(0, _applicationFullPathLength).Replace("\\", "/");
            return $"Assets/{path}";
        }
    }
}