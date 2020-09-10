using LiquadCargoManagment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class ModelDML
    {
        private readonly LCMEntities context;
        public ModelDML()
        {
            context = new LCMEntities();
        }
        public List<PrincipleCompany> getPrincipleCompany()
        {
            return context.PrincipleCompanies.Where(x=>x.OwnCompanyID==ApplicationHelper.OwnCompanyID).ToList();
        }
        public PrincipleCompany getPrincipleCompany(long Id)
        {
            return context.PrincipleCompanies
                .Where(x => x.ID == Id && x.OwnCompanyID == ApplicationHelper.OwnCompanyID).FirstOrDefault();
        }
        public List<PrincipleCompany> getPrincipleCompany(string Code, string Name)
        {
            return context.PrincipleCompanies
                .Where(x => x.Code == Code && x.Name== Name && x.OwnCompanyID == ApplicationHelper.OwnCompanyID).ToList();
        }

        public List<Area> getArea()
        {
            return context.Areas.Where(x => x.OwnCompanyId == ApplicationHelper.OwnCompanyID).ToList();
        }
        public Area getArea(long Id)
        {
            return context.Areas
                .Where(x => x.ID == Id && x.OwnCompanyId == ApplicationHelper.OwnCompanyID).FirstOrDefault();
        }
        public List<Area> getArea(string Code, string Name)
        {
            return context.Areas
                .Where(x => x.Code == Code && x.Name == Name && x.OwnCompanyId == ApplicationHelper.OwnCompanyID).ToList();
        }
    }
}