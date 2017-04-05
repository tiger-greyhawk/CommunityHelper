using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ViewCommunityHelper.View
{
    public class PresentationCommands 
    {
        private static RoutedCommand _showGameFunctionalWindowCommand = new RoutedCommand("ShowGameFunctionalWindowCommand", typeof(PresentationCommands));

        public static RoutedCommand ShowGameFunctionalWindowCommand
        {
            get { return _showGameFunctionalWindowCommand; }
        }

        private readonly static RoutedCommand accept = new RoutedCommand("Accept", typeof(PresentationCommands));

        public static RoutedCommand Accept
        {
            get { return PresentationCommands.accept; }
        }

        private static RoutedCommand _exit = new RoutedCommand("Exit", typeof(PresentationCommands));

        public static RoutedCommand Exit
        {
            get { return _exit; }
        }

        private static RoutedCommand _connect = new RoutedCommand("Connect", typeof(PresentationCommands));

        public static RoutedCommand Connect
        {
            get { return _connect; }
        }

        private static RoutedCommand _showFactionsWindowCommand = new RoutedCommand("ShowFactionsWindowCommand", typeof(PresentationCommands));

        public static RoutedCommand ShowFactionsWindowCommand
        {
            get { return _showFactionsWindowCommand; }
        }
    }
}
