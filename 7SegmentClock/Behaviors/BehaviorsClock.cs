using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _7SegmentClock.Behaviors
{
    class DraggingWindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            Window window = this.AssociatedObject;
            window.MouseDown += window_MouseDown;
            window.MouseMove += window_MouseMove;
            window.MouseUp += window_MouseUp;
            window.Deactivated += window_Deactivated;            
        }

        private bool isDragging = false;
        private Point startCursorPosition;

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                startCursorPosition = e.GetPosition(sender as Window);
            }
        }

        private void window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Window window = sender as Window;
                Vector translation =
                    e.GetPosition(window) - startCursorPosition;
                window.Left += translation.X;
                window.Top += translation.Y;
            }
        }

        private void window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging) isDragging = false;
        }

        private void window_Deactivated(object sender, EventArgs e)
        {
            window_MouseUp(sender, null);
        }
    }

    public class CloseWindowByMenuItemClick : Behavior<Window>
    {
        private static DependencyProperty KeyProperty = DependencyProperty.Register(
            nameof(menuItem),
            typeof(MenuItem),
            typeof(CloseWindowByMenuItemClick),
            new PropertyMetadata(null, itemChanged));

        private static void itemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = (d as CloseWindowByMenuItemClick).AssociatedObject;

            void CloseWindowByMenuItemClick_Click(object sender, RoutedEventArgs _e)
            {
                window.Close();
            }

            if (e.OldValue != null) (e.OldValue as MenuItem).Click -= CloseWindowByMenuItemClick_Click;
            if (e.NewValue != null) (e.NewValue as MenuItem).Click += CloseWindowByMenuItemClick_Click;
        }

        public MenuItem menuItem
        {
            get
            {
                return (MenuItem)GetValue(KeyProperty);
            }
            set
            {
                SetValue(KeyProperty, value);
            }
        }
    }
}
