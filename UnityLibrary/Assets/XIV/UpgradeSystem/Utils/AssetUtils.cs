#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace XIV.UpgradeSystem.Utils
{
    public static class AssetUtils
    {
        public static Dictionary<Type, TList> LoadAssets<TAsset, TList>(string folderPath) where TAsset : UnityEngine.Object where TList : IList<TAsset> 
        {
            Dictionary<Type, TList> typeValuePair = new Dictionary<Type, TList>();
            var folders = Directory.GetDirectories(folderPath);
                
            for (int i = 0; i < folders.Length; i++)
            {
                string[] assetPaths = Directory.GetFiles(folders[i]);
                for (int j = 0; j < assetPaths.Length; j++)
                {
                    var assetPath = assetPaths[j];
                    string extension = Path.GetExtension(assetPath);
                    if (extension.Equals(".asset"))
                    {
                        TAsset asset = AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
                            
                        var assetType = asset.GetType();
                        if (typeValuePair.ContainsKey(assetType))
                        {
                            typeValuePair[assetType].Add(asset);
                        }
                        else
                        {
                            TList list = Activator.CreateInstance<TList>();
                            list.Add(asset);
                            typeValuePair.Add(assetType, list);
                        }
                    }
                }
            }

            return typeValuePair;
        }
    }
}
#endif