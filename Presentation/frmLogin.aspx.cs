using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Presentation_frmLogin : System.Web.UI.Page
{
    private object lbllogin;
    private string strcon;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserId"] = null;
        Session["RoleId"] = null;
        Session["Email"] = null;
        Session["UserName"] = null;
        Session["DepId"] = null;
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/register.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

        SqlCommand com = new SqlCommand("select * from users where email=@em and password=@pass", con);
        SqlParameter p1 = new SqlParameter("@em", txtEmailAddress.Text);
        SqlParameter p2 = new SqlParameter("@pass", txtPassword.Text);
        com.Parameters.Add(p1);
        com.Parameters.Add(p2);
        con.Open();
        SqlDataReader rd = com.ExecuteReader();

        if (rd.Read())
        {
            Session["UserId"] = rd.GetValue(0).ToString();
            Session["RoleId"] = rd.GetValue(7).ToString();
            Session["Email"] = rd.GetValue(4).ToString();
            Session["UserName"] = rd.GetValue(1).ToString();
            Session["DepId"] = rd.GetValue(3).ToString();


            if (rd.GetValue(7).ToString() == "1")
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            else if (rd.GetValue(7).ToString() == "2")
            {
                Response.Redirect("~/Student/Default.aspx");
            }
            else if (rd.GetValue(7).ToString() == "3")
            {
                Response.Redirect("~/Guest/Default.aspx");
            }
            else if (rd.GetValue(7).ToString() == "5")
            {
                Response.Redirect("~/MarketingCoordinator/Default.aspx");
            }
            else if (rd.GetValue(7).ToString() == "6")
            {
                Response.Redirect("~/MarketingManager/Default.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please give the correct Email/password')", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}