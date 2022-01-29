using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ContentController : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private Button temp;
	[SerializeField] private List<RowController> rows;
	[SerializeField] private WordManager wordManager;

	private int _index;

	private void Start()
	{
		inputField.onValueChanged.AddListener(OnUpdateContent);
		inputField.onSubmit.AddListener(OnSubmit);
	}

	private void OnUpdateContent(string msg)
	{
		var row = rows[_index];
		row.UpdateText(msg);
	}

	private bool UpdateState()
	{
		var states = wordManager.GetStates(inputField.text);
		rows[_index].UpdateState(states);

		foreach (var state in states)
		{
			if (state != State.Correct)
				return false;
		}

		return true;
	}

	private void OnSubmit(string msg)
	{
		temp.Select();
		inputField.Select();

		if (!IsEnough())
		{
			Debug.Log("YETERSIZ...");
			return;
		}

		var isWin = UpdateState();
		if (isWin)
		{
			Debug.Log("Kazandın...");
			inputField.enabled = false;
			return;
		}

		_index++;
		var isLost = _index == rows.Count;
		if (isLost)
		{
			Debug.Log("Kaybettin!");
			inputField.enabled = false;
			return;
		}

		inputField.text = "";
	}

	private bool IsEnough()
	{
		return inputField.text.Length == rows[_index].CellAmount;
	}
}