using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace GuessNumber
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView txtTop;
        private TextView txtRange;
        private EditText txtLowGuess;
        private EditText txtHighGuess;
        private Button btnStart;
        private EditText txtGuess;
        private Button btnGuess;
        private TextView txtAnswer;
        private TextView txtNumOfGuesses;
        private ImageView imageFaceBla;

        private int low;
        private int high;
        private int guess;
        private int numOfGuesses = 0;
        private int answer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Initializawiews();
        }
        private void setRandom()
        {
            Random r = new Random();
            bool parsed = int.TryParse(txtLowGuess.Text, out low);
            if (parsed == false)
            {
                return;
            }
            parsed = int.TryParse(txtHighGuess.Text, out high);
            if (parsed == false)
            {
                return;
            }
            if (low >= high)
            {
                Toast.MakeText(this, "low cant be bigger then high", ToastLength.Long).Show();
            }
            answer = r.Next(low, high + 1);
        }
        private void Initializawiews()
        {
            txtTop =          FindViewById<TextView>(Resource.Id.txtTop);
            txtRange =        FindViewById<TextView>(Resource.Id.txtRange);
            txtLowGuess =     FindViewById<EditText>(Resource.Id.txtLowGuess);
            txtHighGuess =    FindViewById<EditText>(Resource.Id.txtHighGuess);
            btnStart =        FindViewById<Button>(Resource.Id.btnStart);
            txtGuess =        FindViewById<EditText>(Resource.Id.txtGuess);
            btnGuess =        FindViewById<Button>(Resource.Id.btnGuess);
            txtAnswer =       FindViewById<TextView>(Resource.Id.txtAnswer);
            txtNumOfGuesses = FindViewById<TextView>(Resource.Id.txtNumOfGuesses);
            imageFaceBla =    FindViewById<ImageView>(Resource.Id.imageFace);
            btnGuess.Click += CheckButton_Click;
            btnStart.Click += NewButton_Click;


        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            bool parsed = int.TryParse(txtGuess.Text, out guess);
            if (parsed == false)
            {
                return;
            }
            if (guess > high || guess < low)
            {
                Toast.MakeText(this, "out of range", ToastLength.Long).Show();
            }
            else if (guess > answer)
            {
                txtAnswer.Text = "too big";
                numOfGuesses += 1;
                txtNumOfGuesses.Text = (numOfGuesses + " guesses");
            }
            else if (guess < answer)
            {
                txtAnswer.Text = "too small";
                numOfGuesses += 1;
                txtNumOfGuesses.Text = (numOfGuesses + " guesses");
            }
            else
            {
                txtAnswer.Text = "Correct";
                if (numOfGuesses < 5)
                {
                    imageFaceBla.SetImageResource(Resource.Drawable.happy_face);

                }
                else if (numOfGuesses >= 5 && numOfGuesses < 10)
                {
                    imageFaceBla.SetImageResource(Resource.Drawable.normal_face);
                }
                else
                {
                    imageFaceBla.SetImageResource(Resource.Drawable.sad_face);
                }
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            setRandom();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}