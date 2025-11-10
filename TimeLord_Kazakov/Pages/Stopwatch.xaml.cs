using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeLord_Kazakov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Stopwatch.xaml
    /// </summary>
    public partial class Stopwatch : Page
    {
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public float full_second = 0;
        private float lastIntervalTime = 0;
        public bool start_stopwatch = false;

        public Stopwatch()
        {
            InitializeComponent();

            dispatcherTimer.Tick += TimerSecond;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void TimerSecond(object sesnder, EventArgs e)
        {
            full_second++;

            float hours = (int)(full_second / 60 / 60);
            float minuts = (int)(full_second / 60) - (hours * 60);
            float seconds = full_second - (hours * 60 * 60) - (minuts * 60);

            string s_seconds = seconds.ToString();
            if (seconds < 10) s_seconds = "0" + seconds;

            string s_minuts = minuts.ToString();
            if (minuts < 10) s_minuts = "0" + minuts;

            string s_hours = hours.ToString();
            if(hours < 10) s_hours = "0" + hours;

            time.Content = s_hours + ":" + s_minuts + ":" + s_seconds;
        }

        private void StartStopwatch(object sender, RoutedEventArgs e)
        {
            

            if (start_stopwatch == false)
            {
                dispatcherTimer.Tick -= TimerSecond;
                dispatcherTimer.Tick -= BackStartStopwatchTick;

                dispatcherTimer.Tick += TimerSecond;

                full_second = 0;
                dispatcherTimer.Start();
                start_stopwatch = true;
                start.Content = "Стоп";

                lastIntervalTime = 0;
                intervalsList.Items.Clear();
            }
            else {
                dispatcherTimer.Stop();
                start_stopwatch = false;
                start.Content = "Начать"; }
        }

        private void AddInterval_Click(object sender, RoutedEventArgs e)
        {
            if (start_stopwatch)
            {
                float intervalDiff = full_second - lastIntervalTime;
                lastIntervalTime = full_second;

                float hours = (int)(intervalDiff / 60 / 60);
                float minuts = (int)(intervalDiff / 60) - (hours * 60);
                float seconds = intervalDiff - (hours * 60 * 60) - (minuts * 60);

                string s_seconds = seconds.ToString();
                if (seconds < 10) s_seconds = "0" + seconds;

                string s_minuts = minuts.ToString();
                if (minuts < 10) s_minuts = "0" + minuts;

                string s_hours = hours.ToString();
                if (hours < 10) s_hours = "0" + hours;

                intervalsList.Items.Add($"{s_hours}:{s_minuts}:{s_seconds}");
            }
        }

        private void BackStartStopwatchTick(object sesnder, EventArgs e)
        {
            if (full_second > 0)
            {
                full_second--;

                float hours = (int)(full_second / 60 / 60);
                float minuts = (int)(full_second / 60) - (hours * 60);
                float seconds = full_second - (hours * 60 * 60) - (minuts * 60);

                string s_seconds = seconds.ToString();
                if (seconds < 10) s_seconds = "0" + seconds;

                string s_minuts = minuts.ToString();
                if (minuts < 10) s_minuts = "0" + minuts;

                string s_hours = hours.ToString();
                if (hours < 10) s_hours = "0" + hours;

                time.Content = s_hours + ":" + s_minuts + ":" + s_seconds;
            }
            else
            {
                dispatcherTimer.Stop();
                start_stopwatch = false;
                start.Content = "Начать";
            }
        }

        private void BackStartStopwatch(object sender, RoutedEventArgs e)
        {
            if (full_second > 0)
            {
                dispatcherTimer.Tick -= TimerSecond;

                dispatcherTimer.Tick += BackStartStopwatchTick;

                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Start();

                start_stopwatch = true;
                start.Content = "Стоп";
            }
        }
    }
}
