using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WaveTrumpet.UI.Controls
{
    public partial class VolumeSlider : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(VolumeSlider),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVisualPropertyChanged));

        public static readonly DependencyProperty PeakLeftProperty = DependencyProperty.Register(
            "PeakLeft",
            typeof(double),
            typeof(VolumeSlider),
            new PropertyMetadata(0d, OnVisualPropertyChanged));

        public static readonly DependencyProperty PeakRightProperty = DependencyProperty.Register(
            "PeakRight",
            typeof(double),
            typeof(VolumeSlider),
            new PropertyMetadata(0d, OnVisualPropertyChanged));

        public static readonly DependencyProperty IsMutedProperty = DependencyProperty.Register(
            "IsMuted",
            typeof(bool),
            typeof(VolumeSlider),
            new PropertyMetadata(false, OnVisualPropertyChanged));

        private Border _peakMeter1;
        private Border _peakMeter2;
        private Thumb _thumb;

        public VolumeSlider()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double PeakLeft
        {
            get { return (double)GetValue(PeakLeftProperty); }
            set { SetValue(PeakLeftProperty, value); }
        }

        public double PeakRight
        {
            get { return (double)GetValue(PeakRightProperty); }
            set { SetValue(PeakRightProperty, value); }
        }

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        private static void OnVisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VolumeSlider)d).SyncVisualState();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CacheTemplateParts();
            SyncVisualState();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SyncVisualState();
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(Value - e.NewValue) > 0.01)
            {
                SetCurrentValue(ValueProperty, e.NewValue);
            }

            SyncVisualState();
        }

        private void OnSliderPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetValueFromPoint(e.GetPosition(SliderPart));
            Mouse.Capture(SliderPart);
            e.Handled = true;
        }

        private void OnSliderPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (SliderPart.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                SetValueFromPoint(e.GetPosition(SliderPart));
                e.Handled = true;
            }
        }

        private void OnSliderPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SliderPart.IsMouseCaptured)
            {
                Mouse.Capture(null);
                e.Handled = true;
            }
        }

        private void OnSliderPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ChangePositionByAmount(Math.Sign(e.Delta) * 2.0);
            e.Handled = true;
        }

        private void CacheTemplateParts()
        {
            if (SliderPart == null)
            {
                return;
            }

            SliderPart.ApplyTemplate();
            _thumb = SliderPart.Template.FindName("SliderThumb", SliderPart) as Thumb;
            _peakMeter1 = SliderPart.Template.FindName("PeakMeter1", SliderPart) as Border;
            _peakMeter2 = SliderPart.Template.FindName("PeakMeter2", SliderPart) as Border;
        }

        private void SetValueFromPoint(Point point)
        {
            var width = Math.Max(1, SliderPart.ActualWidth);
            var percent = point.X / width;
            var nextValue = Bound((SliderPart.Maximum - SliderPart.Minimum) * percent + SliderPart.Minimum);
            SetCurrentValue(ValueProperty, nextValue);

            if (Math.Abs(SliderPart.Value - nextValue) > 0.01)
            {
                SliderPart.Value = nextValue;
            }

            SyncVisualState();
        }

        private void ChangePositionByAmount(double amount)
        {
            var nextValue = Bound(Value + amount);
            SetCurrentValue(ValueProperty, nextValue);

            if (Math.Abs(SliderPart.Value - nextValue) > 0.01)
            {
                SliderPart.Value = nextValue;
            }

            SyncVisualState();
        }

        private double Bound(double value)
        {
            return Math.Max(SliderPart.Minimum, Math.Min(SliderPart.Maximum, value));
        }

        private void SyncVisualState()
        {
            if (SliderPart == null)
            {
                return;
            }

            if (_thumb == null || _peakMeter1 == null || _peakMeter2 == null)
            {
                CacheTemplateParts();
            }

            if (Math.Abs(SliderPart.Value - Value) > 0.01)
            {
                SliderPart.Value = Value;
            }

            SliderPart.Opacity = IsMuted ? 0.55 : 1.0;
            UpdatePeakMeters();
        }

        private void UpdatePeakMeters()
        {
            if (_thumb == null || _peakMeter1 == null || _peakMeter2 == null)
            {
                return;
            }

            var usableWidth = Math.Max(0, SliderPart.ActualWidth - _thumb.ActualWidth);
            var volumePercent = Math.Max(0, Math.Min(100, Value)) / 100.0;
            var peakLeftPercent = Math.Max(0, Math.Min(100, PeakLeft)) / 100.0;
            var peakRightPercent = Math.Max(0, Math.Min(100, PeakRight)) / 100.0;

            _peakMeter1.Width = IsMuted ? 0 : usableWidth * peakLeftPercent * volumePercent;
            _peakMeter2.Width = IsMuted ? 0 : usableWidth * peakRightPercent * volumePercent;
            _peakMeter1.Opacity = IsMuted ? 0.15 : 0.65;
            _peakMeter2.Opacity = IsMuted ? 0.15 : 0.65;
        }
    }
}
