using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ViewCommunityHelper.View
{
    public class MyCommand
    {
        private static RoutedCommand requery;

        static MyCommand()
        {
            // Инициализация команды
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl + R"));
            requery = new RoutedCommand("Requery", typeof(MyCommand), inputs);
        }

        public static RoutedCommand Requery
        {
            get { return requery; }
        }
    }
}
