using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Localization app. Manage localizacion defined in the app
/// </summary>
public static class LocalizationApp {
	#region STATIC_ENUM_CONSTANTS
	public enum LANGUAGE{
		SPANISH 	= 0,
		ENGLISH 	= 1
	}

	public static readonly string 	SPANISH_TAG = "es";		
	public static readonly string 	ENGLISH_TAG = "en";

	public static readonly string	PLAYER_PREF_FIELD = "language_app";

	public static readonly string 	PATH_LANGUAGE_FILE = "Localization/Language";
	#endregion
	
	#region FIELDS
	public static event Action	onChangeLanguage = delegate{};
	public static LANGUAGE		languageApp = LANGUAGE.SPANISH;
	
	private static JSONObject	jsonLanguageApp;
	private static string 		locationAppTag = SPANISH_TAG;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_CUSTOM	
	/****Funciones para los cambios de idioma****/
	//Change language of app
	public static void ChangeLanguageAppTo(LocalizationApp.LANGUAGE lang){
		if (lang == LANGUAGE.ENGLISH){
			locationAppTag = ENGLISH_TAG;
			languageApp= LANGUAGE.ENGLISH;
		}else if (lang == LANGUAGE.SPANISH){
			locationAppTag = SPANISH_TAG;
			languageApp= LANGUAGE.SPANISH;
		}else{
			Console.Warning("[LOCALIZATIONAPP] Language tag not found");
			return;
		}
		
		PlayerPrefs.SetString(PLAYER_PREF_FIELD, locationAppTag);
		
		LocalizationApp.Init();
		
		onChangeLanguage();
	}
	
	/****Funciones para la carga de los JSON****/
	//Load JSON
	public static void Init(){
		if (PlayerPrefs.HasKey(PLAYER_PREF_FIELD)){
			locationAppTag = PlayerPrefs.GetString(PLAYER_PREF_FIELD);
			
			if (locationAppTag == ENGLISH_TAG){
				languageApp= LANGUAGE.ENGLISH;
			}else if (locationAppTag == SPANISH_TAG){
				languageApp= LANGUAGE.SPANISH;
			}else{
				Console.Warning("[LOCALIZATIONAPP] Language tag not found");
				return;
			}
		}else{
			PlayerPrefs.SetString(PLAYER_PREF_FIELD, locationAppTag);
		}
		
		string text = Resources.Load(PATH_LANGUAGE_FILE).ToString();
		jsonLanguageApp = new JSONObject(text);
	}
	
	/****Funciones para leer los textos****/
	//Return text
	public static string TextApp(string key){

		if (jsonLanguageApp == null) {
			Init();
		}
		
		if (jsonLanguageApp.GetField (locationAppTag) != null){
			if (jsonLanguageApp.GetField (locationAppTag).GetField (key) != null) {
				return jsonLanguageApp.GetField (locationAppTag).GetField (key).str;
			}else{
				Console.Warning("Warning: Campo "+key+" no existe");
				return "[[key not found]]";
			}
		}else{
			Console.Warning("Warning: Location no encontrada");
			return "[[language not found]]";
		}
	}
	#endregion
}

