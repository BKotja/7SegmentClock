using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace _7SegmentClock.ViewModel
{
    using Model;

    public class ViewModelClock : INotifyPropertyChanged
    {
        ModelClock model = new ModelClock();

        public ViewModelClock()
        {
            model.dateTime = DateTime.Now;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Tick += dispatcherTimer_Tick;

            if (!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) dispatcherTimer.Start();          
        }

        public void OnTop(object sender, KeyboardFocusChangedEventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            onPropertyChanged();
        }

        private DateTime PreviousTime
        {
            get
            {
                return model.dateTime;
            }
            set
            {
                model.dateTime = value;
            }
        }

        public DateTime ActualTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged()
        {
            if (PreviousTime.Second == ActualTime.Second) return;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ActualTime)));
            PreviousTime = ActualTime;
        }

    }
}
