using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using LiveStreamMusic.Business;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web
{
    public partial class SiteMobile : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucLogin.OnCancelarModal += UcLoginOnOnCancelarModal;
            if (!IsPostBack)
            {
                if (Request.QueryString["code"] != null && Request.QueryString["code"] != "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('hola');", true);
                    //var obj = GetFacebookUserData(Request.QueryString["code"]);
                    //ListView1.DataSource = obj;
                    //ListView1.DataBind();
                }
                new BusinessSeguridad().GeneraVisita(Request.ServerVariables["REMOTE_ADDR"]);
                //if (Request.Browser.IsMobileDevice)
                //    Response.Redirect("~/DefaultMobile.aspx");
            }
        }

        private void UcLoginOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSingIn\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}