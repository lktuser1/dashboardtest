using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EDS.Intranet.ServerControls
{
    /// <summary>
    ///  The edsSiteMapProvider class is derived from the System.Web.XmlSiteMapProvider
    ///  class and is the default site map provider for the ASP.NET templates. 
    ///  The edsSiteMapProvider class generates site map trees from XML files.
    /// </summary>
    /// <change date="2007/08/22" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public class edsSiteMapProvider : XmlSiteMapProvider
    {
        /// <summary>
        //     This method retrieves a Boolean value indicating whether the specified System.Web.SiteMapNode
        //     object can be viewed by the user in the specified context.
        /// </summary>
        /// <param name="context">The System.Web.HttpContext that contains user information.</param>
        /// <param name="node">The System.Web.SiteMapNode that is requested by the user.</param>
        /// <returns>true if security trimming is enabled and node can be viewed by the user.</returns>
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            string currentUser;

            // Security trimming should be enabled, otherwise everything is accessible
            if (!this.SecurityTrimmingEnabled)
            {
                return true;
            }

            // If no roles are explicitly associated with the node, then it is accessible
            if (node.Roles.Count == 0)
            {
                return true;
            }

            // Get the current user ID, minus the domain
            currentUser = this.GetLoginUserID();

            foreach (string role in node.Roles)
            {
                if (role == String.Empty || role == "*")
                {
                    return true;
                }

                if (Roles.IsUserInRole(currentUser, role))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        ///  This method returns the Global Net ID of the current user.
        /// </summary>
        /// <returns></returns>
        private string GetLoginUserID()
        {
            System.Security.Principal.IPrincipal currentUser;
            currentUser = System.Web.HttpContext.Current.User;

            string userName = currentUser.Identity.Name;
            if (userName.Length == 0)
            {
                userName = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CT_REMOTE_USER"];
            }
            if (userName.Length > 0)
            {
                //Strip the Domain Name from the Global Net ID.
                Int32 slashPosition = userName.IndexOf('\\');
                if (slashPosition > -1)
                {
                    userName = userName.Substring(slashPosition + 1);
                }
                userName = userName.ToUpper();
            }

            return userName;
        }
    }
}
