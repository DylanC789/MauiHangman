using MauiHangman;
public partial class MainPage : ContentPage
{
    private HangmanGame _game;

    private Random _random = new Random();

    // List of words to randomly select from
    private List<string> _words = new List<string>
{
"maui", "tropical", "volcano", "island", "adventure",
"hangman", "ocean", "paradise", "beach", "sunset"
};
    public MainPage()
    {
        InitializeComponent();
        StartNewGame();
    }
    private void StartNewGame()
    {
        // Get a random word from the list
        string randomWord = _words[_random.Next(_words.Count)];

        _game = new MauiHangman.HangmanGame(randomWord); // Initialize the game with the random word

        // Reset the UI
        PlayAgainButton.IsVisible = false; // Hide the Play Again button
        GameOverMessage.IsVisible = false; // Hide Game Over message
        EnableLetterButtons(); // Enable all letter buttons
        UpdateUI(); // Update UI for new game
    }
    private void UpdateUI()
    { // Update the displayed word with underscores for unguessed letters
      WordDisplay.Text = string.Join("",_game.CurrentGuess. ToUpper().ToCharArray());
        
      // Update the number of attempts left
      AttemptsLabel.Text = $"Attempts left: {_game.AttemptsLeft}"; 
        
        // Display guessed letters
        GuessedLettersLabel.Text = $"Used: {_game.GuessedLetters.ToUpper()}";

        // Update Hangman graphic based on incorrect guesses
        UpdateHangmanImage(_game.IncorrectGuesses);

        // Check if the game is over (win/loss condition)
        if (_game.GameOver)
        {
            GameOverMessage.Text = _game.WordToGuess == _game.CurrentGuess
                ? "You won!" 
                : $"Game Over! The word was {_game.WordToGuess.ToUpper()}"; 
            GameOverMessage.IsVisible = true;
            DisableLetterButtons(); } 
    }
    // Updates the Hangman image based on the number of incorrect guesses
    private void UpdateHangmanImage(int incorrectGuesses)
    {
        string imageName = $"hangman{incorrectGuesses}.gif";
        HangmanImage.Source = ImageSource.FromFile(imageName);
    }
    private void DisableLetterButtons()
    {
        foreach (var view in LetterGrid.Children)
        {
            if (view is Button button)
                button.IsEnabled = false;
        }
        PlayAgainButton.IsVisible = true; // Show the Play Again button
    }

    // Enable all letter buttons when a new game starts

    private void EnableLetterButtons()
    {
        foreach (var view in LetterGrid.Children)
        {
            if (view is Button button)
            {
                button.IsEnabled = true;
            }
        }
    }
    private void LetterButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            char guessedLetter = button.Text[0];
            bool correct = _game.GuessLetter(guessedLetter);

            button.IsEnabled = false;
            UpdateUI();
        }

    }
    // When the "Play Again" button is clicked, start a new game
    private void PlayAgainButton_Clicked(object sender, EventArgs e)
    {
        StartNewGame();
    }
}