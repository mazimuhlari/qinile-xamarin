using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Qinile.Core.Behaviours
{
    public static class ViewCellAttachedBehaviour
    {
        private const string Command = "Command";
        private const string CommandParameter = "CommandParameter";

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                Command,
                typeof(ICommand),
                typeof(ViewCellAttachedBehaviour),
                default(ICommand),
                BindingMode.OneWay,
                null,
                PropertyChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached(
                CommandParameter,
                typeof(object),
                typeof(ViewCellAttachedBehaviour),
                default(object),
                BindingMode.OneWay,
                null);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ViewCell cell)
            {
                cell.Tapped -= ViewCellOnTapped;
                cell.Tapped += ViewCellOnTapped;
            }
        }

        private static void ViewCellOnTapped(object sender, EventArgs e)
        {
            if (sender is ViewCell cell && cell.IsEnabled)
            {
                var command = GetCommand(cell);
                var parameter = GetCommandParameter(cell);

                if (command != null && command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        public static ICommand GetCommand(BindableObject bindableObject) =>
            (ICommand)bindableObject.GetValue(CommandProperty);

        public static void SetCommand(BindableObject bindableObject, object value) =>
            bindableObject.SetValue(CommandProperty, value);

        public static object GetCommandParameter(BindableObject bindableObject) =>
            bindableObject.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(BindableObject bindableObject, object value) =>
            bindableObject.SetValue(CommandParameterProperty, value);
    }
}
