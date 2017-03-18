using ViewCommunityHelper.View;

namespace CommunityHelper.ViewModel
{
    public interface IViewModelFactory
    {
        //IWindow Create(IWindow window);  ////  ТАК НЕЛЬЗЯ!!!  Надо возвращать конкретный тип, а не интерфейс!!!!       ?????????????????  
        MainViewModel Create(IWindow window);
    }
}
