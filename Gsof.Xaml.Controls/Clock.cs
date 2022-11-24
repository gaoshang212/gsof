using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Gsof.Xaml.Controls
{
    /// <summary>
    /// Clock
    /// </summary>
    public class Clock : Control
    {
        private DispatcherTimer _timer;

        /// <summary>
        /// Current DateTime
        /// </summary>
        public DateTime Now
        {
            get { return (DateTime)GetValue(NowProperty); }
            set { SetValue(NowProperty, value); }
        }

        public static readonly DependencyProperty NowProperty =
            DependencyProperty.Register("Now", typeof(DateTime), typeof(Clock), new PropertyMetadata(DateTime.Now));

        /// <summary>
        /// Update Interval
        /// </summary>
        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(Clock), new PropertyMetadata(TimeSpan.FromSeconds(1), OnIntervalChanged));

        private static void OnIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var clock = (Clock)d;

            clock.UpdateDatetime();
            clock._timer = clock.CreateTimer();
        }

        static Clock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Clock), new FrameworkPropertyMetadata(typeof(Clock)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _timer = CreateTimer();
        }

        private DispatcherTimer CreateTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }

            var timer = new DispatcherTimer();
            timer.Interval = Interval;
            timer.Tick += (sender, args) => UpdateDatetime();
            timer.Start();

            return timer;
        }

        protected virtual void UpdateDatetime()
        {
            Now = DateTime.Now;
        }
    }
}
