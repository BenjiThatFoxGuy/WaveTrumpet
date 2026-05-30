using System;
using System.Windows;
using System.Windows.Controls;

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

        public VolumeSlider()
        {
            InitializeComponent();
            Loaded += OnLoaded;
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

        private void SyncVisualState()
        {
            if (SliderPart == null || VolumeBar == null || PeakBar == null)
            {
                return;
            }

            if (Math.Abs(SliderPart.Value - Value) > 0.01)
            {
                SliderPart.Value = Value;
            }

            SliderPart.IsEnabled = !IsMuted;
            VolumeBar.Value = IsMuted ? 0 : Value;
            PeakBar.Value = IsMuted ? 0 : Math.Max(PeakLeft, PeakRight);
            PeakBar.Opacity = IsMuted ? 0.15 : 0.35;
        }
    }
}
