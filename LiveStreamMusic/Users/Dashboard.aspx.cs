using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.Users
{
    public partial class Dashboard : System.Web.UI.Page
    {
        BusinessUsuarios _nUsuario = new BusinessUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["code"] != null && Request.QueryString["code"] != "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "alert('hola');", true);
                    var obj = GetFacebookUserData(Request.QueryString["code"]);
                    //_nUsuario.CrearCuenta(obj[0].first_name, obj[0].last_name, obj[0].);
                    //ListView1.DataSource = obj;
                    //ListView1.DataBind();
                }
            }
        }

        protected List<Facebook.User> GetFacebookUserData(string code)
        {
            try
            {
                // Exchange the code for an access token
                Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/Users/Dashboard.aspx&code=" + code);
                HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

                System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
                string token = str.ReadToEnd().ToString().Replace("access_token=", "");

                // Split the access token and expiration from the single string
                string[] combined = token.Split('&');
                string accessToken = combined[0];

                // Exchange the code for an extended access token
                Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
                HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

                StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
                string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

                // Split the access token and expiration from the single string
                string[] eatWords = eatToken.Split('&');
                string extendedAccessToken = eatWords[0];

                // Request the Facebook user information
                Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=email,first_name,last_name,gender,locale,link&access_token=" + accessToken);
                HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

                // Read the returned JSON object response
                StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
                string jsonResponse = string.Empty;
                jsonResponse = userInfo.ReadToEnd();

                // Deserialize and convert the JSON object to the Facebook.User object type
                JavaScriptSerializer sr = new JavaScriptSerializer();
                string jsondata = jsonResponse;
                Facebook.User converted = sr.Deserialize<Facebook.User>(jsondata);
                // Write the user data to a List
                List<Facebook.User> currentUser = new List<Facebook.User>();
                currentUser.Add(converted);

                // Return the current Facebook user
                return currentUser;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}