using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Helpers
{
    public class SideBarExtracter
    {
        private readonly LCMEntities context;
        public List<Menu> Menus;
        public List<SecondLevelMenu> subMenu;
        public List<NavMenu> Forms;
        public List<RolePermission> Authentication;
        public List<SubcriptionModule> Subcription;
        public SideBarExtracter()
        {
            context = new LCMEntities();
            Menus = context.Menus.Where(x => x.IsActive == true).OrderBy(x => x.SequenceOrder).ToList();
            subMenu = context.SecondLevelMenus.OrderBy(x => x.SequenceOrder).ToList();
            Forms = context.NavMenus.Where(x => x.IsActive == true).OrderBy(x => x.SequenceOrder).ToList();
            Subcription = context.SubcriptionModules.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
        }
      

    }
}