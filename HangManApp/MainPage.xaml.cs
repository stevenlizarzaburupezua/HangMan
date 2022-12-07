using System.ComponentModel;
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

    public List<char> Letters
    {
		get => letters;
		set
		{
			letters = value;
			OnPropertyChanged();
		}
    }

	public string Message
	{
		get => message;
		set
		{
            message = value;
			OnPropertyChanged();
		}
	}
	public string GameStatus 
	{
		get => GameStatus;
		set
		{
			gameStatus = value;
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
	public string message;
    public string gameStatus;
    List<char> guessed = new List<char>();
	private List<char> letters = new List<char>();
	int mistakes = 0;
	int maxWrongs = 6;
    #endregion

    public MainPage()
	{
		InitializeComponent();
		Letters.AddRange("abcdefghijklmnopqrsstuvwxyz");
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

	private void CheckIfGameWon()
	{
		if (SpotLight.Replace(" ", "") == answer)
		{
			Message = "You Win!";
		}
	}

	private void UpdateStatus()
	{
		GameStatus = $"Errors: {mistakes} of {maxWrongs}";
	}

	#endregion

	private void Button_Clicked(object sender, EventArgs e)
	{
		var btn = sender as Button;
		if (btn != null)
		{
			var letter = btn.Text;
			btn.IsEnabled = false;
			HandleGuess(letter[0]);
		}
	}



	private void HandleGuess(char letter)
	{
		if (guessed.IndexOf(letter) == -1)
		{
			guessed.Add(letter);
		}

		if (answer.IndexOf(letter) >= 0)
		{
			CalculateWord(answer, guessed);
			CheckIfGameWon();
		}
		else if(answer.IndexOf(letter) == -1)
		{
			mistakes++;
			UpdateStatus();
			CheckIfGameLost();
		}
	}

	private void CheckIfGameLost()
	{
		if (mistakes == maxWrongs)
		{
			Message = "You Lost!!";
		}
	}

}

