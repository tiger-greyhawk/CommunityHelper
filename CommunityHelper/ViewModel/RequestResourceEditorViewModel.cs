using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryCommunityHelper;

namespace CommunityHelper.ViewModel
{
    public class RequestResourceEditorViewModel: BaseMagic
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string PlayerNick { get; set; }

        /* TODO про команды
         * По идее команды описываются здесь. Но у Симана сделано через ConfigureBehavior в WindowAdapter с помощью имени команды, которая передается в PresentationCommand
         * 
         * Это если я правильно понял смысл.
         */
    }
}
