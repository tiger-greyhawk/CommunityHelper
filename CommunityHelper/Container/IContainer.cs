using System.Windows;
using ViewCommunityHelper.View;

namespace CommunityHelper.Container
{
    interface IContainer
    {
        IWindow ResolveWindow();
    }
}
