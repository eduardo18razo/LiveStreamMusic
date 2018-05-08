using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.UserControls.Reproductor
{
    public partial class UcPlayer : UserControl
    {
        private readonly BusinessListaReproduccion _nLista = new BusinessListaReproduccion();

        private void LLenaListas()
        {
            try
            {
                rptListasReproduccion.DataSource = _nLista.ObtenerListaReproduccionUsuario(((Usuario)Session["UserData"]).Id, false);
                rptListasReproduccion.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Init();", true);
                    LLenaListas();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Users/Musica.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnSleccionaLista_OnClick(object sender, EventArgs e)
        {
            try
            {
                //LinkButton lnkBtn = (LinkButton)sender;
                //ListaReproduccion lst = _nLista.ObtenerListaReproduccionById(int.Parse(lnkBtn.CommandArgument));
                //rptDetalleLista.DataSource = lst.ListaReproduccionDetalle;
                //rptDetalleLista.DataBind();
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CargaLista();", true);
                //upReproductor.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void rptListasReproduccion_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
                LinkButton btn = e.Item.FindControl("btnSleccionaLista") as LinkButton;
                if (btn != null)
                {
                    btn.Click += btnSleccionaLista_OnClick;
                    scriptMan.RegisterAsyncPostBackControl(btn);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}