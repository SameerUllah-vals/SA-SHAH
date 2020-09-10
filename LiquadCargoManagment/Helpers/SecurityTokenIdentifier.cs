using LiquadCargoManagment.Models;
using System;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Helpers
{
    public class SecurityTokenIdentifier
    {
        private readonly LCMEntities context;
        public SecurityTokenIdentifier()
        {
            context = new LCMEntities();
        }
        public string IdentifyToken(string Token, string FormName)
        {
            string parameter = "";
            if (Token != "" && Token != null)
            {
                try
                {
                    string _parameter = HttpUtility.UrlDecode(ApplicationHelper.IDecrypt(Token));
                    if (_parameter!=string.Empty)
                    {
                        long ID = ApplicationHelper.SplitNumberFromString(_parameter);
                        if (ID > 0)
                        {
                            if (context.NavMenus.Where(x => x.FormID == ID).FirstOrDefault().Url.ToLower()
                                .Contains(FormName))
                            {
                                parameter = _parameter;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    parameter = string.Empty;
                }
            }
            return parameter.ToString();
        }

    

    }
}