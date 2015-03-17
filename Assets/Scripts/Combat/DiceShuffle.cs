using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dice shuffle. Static class who controlled all type of random dice. Based in shuffleBags
/// </summary>
public class DiceShuffle {
	#region STATIC_ENUM_CONSTANTS
	public static readonly	int	MAX_RANDOM_REPEATED = 10;

	public enum DICE{
		D4		= 4,
		D6		= 6,
		D8		= 8,
		D10		= 10,
		D12 	= 12,
		D20		= 20,
		D100	= 100
	}
	#endregion
	
	#region FIELDS
	private static	List<int>	shuffleD4;
	private static  List<int>	shuffleD6;
	private static  List<int>	shuffleD8;
	private static  List<int>	shuffleD10;
	private static  List<int>	shuffleD12;
	private static  List<int>	shuffleD20;
	private static  List<int>	shuffleD100;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM
	public static void Init(){
		FillDiceShuffle (ref shuffleD4, DICE.D4);
		FillDiceShuffle (ref shuffleD6, DICE.D6);
		FillDiceShuffle (ref shuffleD8, DICE.D8);
		FillDiceShuffle (ref shuffleD10, DICE.D10);
		FillDiceShuffle (ref shuffleD12, DICE.D12);
		FillDiceShuffle (ref shuffleD20, DICE.D20);
		FillDiceShuffle (ref shuffleD100, DICE.D100);
	}

	public static int Next(DICE typeDice, int modifier = 0){
		switch (typeDice) {
		case DICE.D4:
			return GetRandomFrom(ref shuffleD4, typeDice) + modifier;
			break;
		case DICE.D6:
			return GetRandomFrom(ref shuffleD6, typeDice) + modifier;
			break;
		case DICE.D8:
			return GetRandomFrom(ref shuffleD8, typeDice) + modifier;
			break;
		case DICE.D10:
			return GetRandomFrom(ref shuffleD10, typeDice) + modifier;
			break;
		case DICE.D12:
			return GetRandomFrom(ref shuffleD12, typeDice) + modifier;
			break;
		case DICE.D20:
			return GetRandomFrom(ref shuffleD20, typeDice) + modifier;
			break;
		default:
			return GetRandomFrom(ref shuffleD100, typeDice) + modifier;
			break;
		}
	}

	private static int GetRandomFrom(ref List<int> listShuffle, DICE typeDice){
		int result = 0;

		int index = UnityEngine.Random.Range (0, listShuffle.Count-1);
		result = listShuffle [index];
		listShuffle.RemoveAt (index);
		
		if (listShuffle.Count == 0){
			FillDiceShuffle(ref listShuffle, typeDice);
		}
		
		return result;
	}

	private static void FillDiceShuffle(ref List<int> listShuffle, DICE typeDice){
		int maxResult = (int)typeDice;
		int maxSize = maxResult * MAX_RANDOM_REPEATED;

		int value = 1;
		listShuffle = new List<int> (maxSize);
		for(int i=0; i<maxSize; i++){
			listShuffle.Add(value);
			value++;
			if (value > maxResult){
				value = 1;
			}
		}
	}
	#endregion
}
