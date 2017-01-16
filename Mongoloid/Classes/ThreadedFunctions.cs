using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Mongoloid
{
    internal class ThreadedFunctions
    {
        /// <summary>
        /// Gets a control value across threads.  
        /// If the control is a label or text box, it will retrutn the text.
        /// If the control is a checkbox or radio button, it will return the check state.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="dispatcher"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static string GetControlValue(string controlName, Dispatcher dispatcher, object parent)
        {
            var output = string.Empty;
            dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                var child = ((MainWindow)parent).FindName(controlName);
                if (child is TextBox)
                    output = ((TextBox)child).Text;
                else if (child is Label)
                    output = ((Label)child).Content.ToString();
                else if (child is RadioButton)
                    output = ((RadioButton) child).IsChecked.ToString().ToLower();
                else if (child is CheckBox)
                    output = ((CheckBox)child).IsChecked.ToString().ToLower();
            });
            return output;
        }

        /// <summary>
        /// Sets a control value across threads.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="value"></param>
        /// <param name="dispatcher"></param>
        /// <param name="parent"></param>
        public static void SetControlValue(string controlName, string value, Dispatcher dispatcher, object parent)
        {
            dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                var child = ((MainWindow)parent).FindName(controlName);
                if (child is TextBox)
                    ((TextBox)child).Text = value;
                else if (child is Label)
                    ((Label)child).Content = value;
            });
        }

        /// <summary>
        /// Increments the value of a control by one
        /// </summary>
        /// <param name="dispatcher">The parent dispatcher to use across threads</param>
        /// <param name="parent">The parent object containing the control</param>
        /// <param name="controlName">The control to increment</param>
        public static void IncrementControlValue(Dispatcher dispatcher, object parent, string controlName)
        {
            dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                var child = ((MainWindow)parent).FindName(controlName);
                if (child is TextBox)
                    ((TextBox)child).Text = (int.Parse(((TextBox)child).Text) + 1).ToString();
                if (child is Label)
                    ((Label)child).Content = (int.Parse(((Label)child).Content.ToString()) + 1).ToString();
            });
        }

        public static void SetControlEnabledState(Dispatcher dispatcher, object parent, string controlName, bool enabled)
        {
            dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                var child = ((MainWindow)parent).FindName(controlName);
                if (child is TextBox)
                    ((TextBox)child).IsEnabled = enabled;
                if (child is Label)
                    ((Label)child).IsEnabled = enabled;
                if (child is RadioButton)
                    ((RadioButton)child).IsEnabled = enabled;
                if (child is Button)
                    ((Button)child).IsEnabled = enabled;
            });
        }

        /// <summary>
        /// Shows an error popup.  Not really needed as multi-threaded, but for the sake of consistency...
        /// </summary>
        /// <param name="errorMessage">Error message to show</param>
        /// <param name="dispatcher">The parent dispatcher to use across threads</param>
        public static void ShowError(string errorMessage, Dispatcher dispatcher)
        {
            dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }
    }
}
