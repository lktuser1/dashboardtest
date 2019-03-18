using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EDS.Intranet.Base;

namespace EDS.Intranet.Error
{
    public partial class AccessDenied : ErrorPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            string[] authorizedRoles;

            base.OnLoad(e);

            Master.PageTitle = "Access Denied";

            if (User.Identity.IsAuthenticated)
            {
                this.LoginStatus.Text = "You are currently logged in as " + User.Identity.Name + ".";
            }
            else
            {
                this.LoginStatus.Text = "You are not currently logged in.";
            }

            //Display the list of roles the current user is authorized for.
            authorizedRoles = Roles.GetRolesForUser(this.LoginUserID);
            foreach (string roleName in authorizedRoles)
            {
                ListItem roleListItem = new ListItem();
                roleListItem.Text = roleName;
                RoleList.Items.Add(roleListItem);
            }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
        }

    }
}
