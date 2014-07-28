﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public class IOSNativeSettings : ScriptableObject {

	public const string VERSION_NUMBER = "4.3";

	public string AppleId = "XXXXXXXXX";

	public List<string> InAppProducts = new List<string>();
	public List<string> DefaultStoreProductsView = new List<string>();




	private const string ISNSettingsAssetName = "IOSNativeSettings";
	private const string ISNSettingsPath = "Extensions/IOSNative/Resources";
	private const string ISNSettingsAssetExtension = ".asset";

	private static IOSNativeSettings instance = null;

	
	public static IOSNativeSettings Instance {

		
		get {
			if (instance == null) {
				instance = Resources.Load(ISNSettingsAssetName) as IOSNativeSettings;
				
				if (instance == null) {
					
					// If not found, autocreate the asset object.
					instance = CreateInstance<IOSNativeSettings>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, ISNSettingsPath);
					if (!Directory.Exists(properPath)) {
						AssetDatabase.CreateFolder("Extensions/", "IOSNative");
						AssetDatabase.CreateFolder("Extensions/IOSNative", "Resources");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", ISNSettingsPath),
					                               ISNSettingsAssetName + ISNSettingsAssetExtension
					                               );
					
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

}
