using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gsof.Xaml.Controls
{
    [TemplatePart(Name = "PART_INCREASE_BUTTON", Type = typeof(Button))]
    [TemplatePart(Name = "PART_DECREASE_BUTTON", Type = typeof(Button))]
    [TemplatePart(Name = "PART_PAGE_TEXTBOX", Type = typeof(TextBox))]
    public class NumericUpDown : Control
    {
        private const string IncreaseKey = "PART_INCREASE_BUTTON";
        private const string DecreaseKey = "PART_DECREASE_BUTTON";
        private const string PageKey = "PART_PAGE_TEXTBOX";

        private Button _increaseButton;
        private Button _decreaseButton;

        private TextBox _pageTextBox;
        private bool _isKeyDown = false;

        private readonly Regex _numMatch = new Regex(@"^-?\d+$");

        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public override void OnApplyTemplate()
        {
            _increaseButton = this.GetTemplateChild(IncreaseKey) as Button;
            _decreaseButton = this.GetTemplateChild(DecreaseKey) as Button;
            _pageTextBox = this.GetTemplateChild(PageKey) as TextBox;

            if (_increaseButton != null)
            {
                _increaseButton.Click += OnIncreaseClick;
            }

            if (_decreaseButton != null)
            {
                _decreaseButton.Click += OnDecreaseClick;
            }

            if (_pageTextBox != null)
            {
                _pageTextBox.PreviewTextInput += this.OnPagePreviewTextInput;
                _pageTextBox.TextChanged += this.OnPageTextChanged;
                _pageTextBox.PreviewKeyDown += this.OnPagePreviewKeyDown;
            }

            base.OnApplyTemplate();
        }

        #region 保护事件

        protected virtual void OnIncreaseClick(object sender, RoutedEventArgs e)
        {
            this.Increase();
        }

        protected virtual void OnDecreaseClick(object sender, RoutedEventArgs e)
        {
            this.Decrease();
        }

        protected virtual void OnPagePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null)
            {
                return;
            }

            var text = tb.Text;

            var index = tb.CaretIndex;
            if (index > text.Length)
            {
                index = text.Length;
            }

            text = text.Insert(index, e.Text);
            e.Handled = !_numMatch.IsMatch(text);
        }

        protected virtual void OnPageTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null)
            {
                return;
            }

            e.Handled = true;

            if (!_numMatch.IsMatch(tb.Text))
            {
                ResetText(tb);
            }

            var ovalue = Convert.ToInt32(tb.Text);
            var value = ovalue;
            if (value < Minimum) value = Minimum;
            if (value > Maximum) value = Maximum;

            if (!_isKeyDown)
            {
                if (value < Minimum) value = Minimum;
                if (value > Maximum) value = Maximum;
                _isKeyDown = false;
                Value = value;
            }
            else if (Value != ovalue)
            {
                tb.Text = $"{value}";
            }

            if (Value != value)
            {
                Value = value;
            }
        }

        protected virtual void OnPagePreviewKeyDown(object sender, KeyEventArgs e)
        {
            _isKeyDown = true;
            if (!e.IsDown)
            {
                return;
            }

            if (e.Key == Key.Up && Value < Maximum)
            {
                this.Increase();
            }
            else if (e.Key == Key.Down && Value > Minimum)
            {
                this.Decrease();
            }
            else if (e.Key == Key.Enter)
            {
                var binding = _pageTextBox.GetBindingExpression(TextBox.TextProperty);
                if (binding != null)
                {
                    binding.UpdateSource();
                }
                //this.OnRaiseValueChanged();
            }
        }

        #endregion

        private void ResetText(TextBox tb)
        {
            tb.SetCurrentValue(TextBox.TextProperty, 0 < Minimum ? Minimum.ToString() : "0");
            tb.SelectAll();
        }

        public void Increase(int p_value)
        {
            var nv = Value + p_value;

            if (nv > Maximum)
            {
                nv = Maximum;
            }

            if (nv < Minimum)
            {
                nv = Minimum;
            }

            if (nv == Value)
            {
                return;
            }

            Value = nv;

            this.OnRaiseValueChanged();
        }

        public void Increase()
        {
            Increase(1);
        }

        public void Decrease()
        {
            Increase(-1);
        }

        private void CheckMinnum()
        {
            this.DecreaseEnable = Value > Minimum;
        }

        private void CheckMaxnum()
        {
            this.IncreaseEnable = Value < Maximum;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnSomeValuePropertyChanged, OnValueCoerce));

        private static object OnValueCoerce(DependencyObject d, object p_newValue)
        {
            var nud = d as NumericUpDown;
            if (nud == null)
            {
                return p_newValue;
            }

            var nv = (int)(p_newValue ?? 0);

            if (nv > nud.Maximum)
            {
                nv = nud.Maximum;
            }

            if (nv < nud.Minimum)
            {
                nv = nud.Minimum;
            }

            return nv;
        }

        private static void OnSomeValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nud = d as NumericUpDown;
            if (nud == null)
            {
                return;
            }

            nud.CheckMinnum();
            nud.CheckMaxnum();

            nud.OnRaiseValueChanged();
        }

        /// <summary>
        /// Maximum value for the Numeric Up Down control
        /// </summary>
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(int.MaxValue));

        /// <summary>
        /// Minimum value of the numeric up down conrol.
        /// </summary>
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(0));

        public bool IncreaseEnable
        {
            get { return (bool)GetValue(IncreaseEnableProperty); }
            set { SetValue(IncreaseEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncreaseEnableProperty =
            DependencyProperty.Register("IncreaseEnable", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));

        public bool DecreaseEnable
        {
            get { return (bool)GetValue(DecreaseEnableProperty); }
            set { SetValue(DecreaseEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecreaseVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecreaseEnableProperty =
            DependencyProperty.Register("DecreaseEnable", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));


        // Value changed
        private static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NumericUpDown));

        /// <summary>The ValueChanged event is called when the TextBoxValue of the control changes.</summary>
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        protected virtual void OnRaiseValueChanged()
        {
            this.RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = true;
        }
    }
}
