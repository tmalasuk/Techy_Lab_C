namespace Techy_Lab_C
{
    public partial class MainPage : ContentPage
    {
        string[] questions = new string[]
        {
            "I think sleep is overrated.",
            "My wardrobe has more than 2 colors in it.",
            "I have sent a message I immediately regretted.",
            "I think I could survive on Mars.",
            "I own at least one boat.",
            "I prefer texting over talking on the phone.",
            "I could beat someone in an arm wrestling match.",
            "I've cried watching a product launch.",
            "I could eat the same meal every day and be fine.",
            "I think I'm the most interesting person in any room."
        };

        string[] images = new string[]
        {
            "q1.jpg", "q2.jpg", "q3.jpg", "q4.jpg", "q5.jpg",
            "q6.jpg", "q7.jpg", "q8.jpg", "q9.jpg", "q10.jpg"
        };

        bool[][] profiles = new bool[][]
        {
            new bool[] { true,  false, true,  false, false, true,  false, true,  true,  true  }, // Jobs
            new bool[] { false, true,  false, false, false, true,  false, false, true,  false }, // Gates
            new bool[] { false, false, true,  false, false, true,  true,  false, true,  true  }, // Zuck
            new bool[] { false, false, false, true,  true,  false, true,  false, false, false }, // Bezos
            new bool[] { true,  true,  true,  true,  true,  false, true,  true,  false, true  }  // Branson
        };

        string[] mogulNames = { "Steve Jobs", "Bill Gates", "Mark Zuckerberg", "Jeff Bezos", "Richard Branson" };
        string[] mogulDescriptions =
        {
            "You are Steve Jobs! Visionary, perfectionist, and you definitely cried at a keynote.",
            "You are Bill Gates! Methodical, generous, and you own at least one sensible sweater.",
            "You are Mark Zuckerberg! Efficient, consistent, and definitely not a robot.",
            "You are Jeff Bezos! Bold, jacked, and you have a boat. Probably two boats.",
            "You are Richard Branson! Adventurous, colorful, and you'd kitesurf to a board meeting."
        };

        string[] mogulImages = { "steve.jpg", "bill.jpg", "mark.jpg", "jeff.jpg", "richard.jpg" };

        bool[] userAnswers = new bool[10];
        int currentIndex = 0;

        public MainPage()
        {
            InitializeComponent();
            LoadQuestion();

            var swipeRight = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            swipeRight.Swiped += OnSwipedRight;

            var swipeLeft = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipeLeft.Swiped += OnSwipedLeft;

            Content.GestureRecognizers.Add(swipeRight);
            Content.GestureRecognizers.Add(swipeLeft);
        }

        void LoadQuestion()
        {
            QuestionLabel.Text = questions[currentIndex];
            QuestionNumberLabel.Text = $"Question {currentIndex + 1} of 10";
            QuestionImage.Source = images[currentIndex];
        }

        async void OnSwipedRight(object sender, SwipedEventArgs e)
        {
            await ShowSwipeFeedback(true);
            userAnswers[currentIndex] = true;
            NextQuestion();
        }

        async void OnSwipedLeft(object sender, SwipedEventArgs e)
        {
            await ShowSwipeFeedback(false);
            userAnswers[currentIndex] = false;
            NextQuestion();
        }

        async Task ShowSwipeFeedback(bool isTrue)
        {
            if (isTrue)
            {
                TrueIndicator.Opacity = 1;
                SwipeOverlay.BackgroundColor = Color.FromArgb("#8000B4D8");
            }
            else
            {
                FalseIndicator.Opacity = 1;
                SwipeOverlay.BackgroundColor = Color.FromArgb("#80E63946");
            }

            SwipeOverlay.Opacity = 1;
            await Task.Delay(400);

            TrueIndicator.Opacity = 0;
            FalseIndicator.Opacity = 0;
            SwipeOverlay.Opacity = 0;
        }

        void NextQuestion()
        {
            currentIndex++;

            if (currentIndex >= 10)
            {
                ShowResults();
            }
            else
            {
                LoadQuestion();
            }
        }

        void ShowResults()
        {
            int bestMatch = 0;
            int bestScore = 0;

            for (int i = 0; i < profiles.Length; i++)
            {
                int score = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (userAnswers[j] == profiles[i][j])
                        score++;
                }
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMatch = i;
                }
            }

            ResultLabel.Text = mogulNames[bestMatch];
            ResultDescription.Text = mogulDescriptions[bestMatch];
            ResultImage.Source = mogulImages[bestMatch];
            ResultOverlay.IsVisible = true;
        }
    }
}