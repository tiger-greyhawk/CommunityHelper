using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ViewCommunityHelper.View
{
    public interface IWindow 
    {
        void Close();
        IWindow CreateChild(object viewModel);
        IWindow CreateChildByViewModel(object viewModel, Window window);
        void Show();
        bool? ShowDialog();
    }
}
