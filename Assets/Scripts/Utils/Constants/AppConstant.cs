using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//Flag for it is necesary create distintc kind of build
public static class AppDevelopFlag{
	public static readonly bool	DEVELOP = true;
}

public static class AppLayers{
	public static readonly LayerMask	LAYER_PC = (LayerMask)9;
}

public static class AppScenes{
	public static readonly string	SCENE_INIT = "0_Init";
	public static readonly string	SCENE_TAVERN = "1_Tavern";
	public static readonly string	SCENE_MAIN_MENU = "2_MainMenu";
	public static readonly string	SCENE_COMBAT = "3_Combat";
}

public static class AppGameFlag{
	public static readonly bool 	PAUSE	= false;
}

public static class AppFiles{
	public static readonly string 	FILE_USER = Application.persistentDataPath+"/configuration.json";
	public static readonly string	RESOURCES_FILE_USER = "JSON/configuration";
}
