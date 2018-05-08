using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveStreamMusic.Web
{
    public partial class musicMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>LoadMenu()</script>", false);

        }
    }
}