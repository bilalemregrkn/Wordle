using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
	[SerializeField] private string origin;

	public List<State> GetStates(string msg)
	{
		var result = new List<State>();

		var list = origin.ToCharArray().ToList();
		var listCurrent = msg.ToCharArray().ToList();

		for (var i = 0; i < listCurrent.Count; i++)
		{
			var currentChar = listCurrent[i];
			var contains = list.Contains(currentChar);
			if (contains)
			{
				var index = list.FindIndex(x => x == currentChar);
				var isCorrect = index == i;
				result.Add(isCorrect ? State.Correct : State.Contain);
			}
			else
			{
				result.Add(State.Fail);
			}
		}
		
		return result;
	}
}