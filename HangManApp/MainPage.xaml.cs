﻿using System.ComponentModel;
using System.Diagnostics;

namespace HangManApp;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	#region UI Properties

	public string SpotLight 
	{ 
		get => spotLight;
		set
		{
            spotLight = value;
			OnPropertyChanged();
		} 
	}

    #endregion

    #region Fields

    List<string> words = new List<string>()
	{
		"python",
		"javascript",
		"maui",
		"csharp",
		"mongodb",
		"sql",
		"xaml",
		"word", 
		"excel",
		"powerpoint",
		"code",
		"hotreload",
		"snipetts"
	};
    string answer = "";
	private string spotLight;
	List<char> guessed = new List<char>();

    #endregion

    public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
		PickWord();
		CalculateWord(answer, guessed);
	}

	#region Game Engine

	private	void PickWord()
	{
		answer = words[new Random().Next(0,words.Count)];
	}

	private void CalculateWord(string answer, List<char> guessed)
	{
		var temp = 
			answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_' ))
			.ToArray();
		SpotLight = string.Join(' ', temp);
	}

	#endregion

}

