using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMasterPage : System.Web.UI.MasterPage
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");

        }
        if (!IsPostBack)
        {
            spUserName.InnerHtml = Session["UserName"].ToString();
            GetRoleName(Session["RoleId"].ToString());

            #region Notification Part
            if (Session["RoleId"].ToString() == "1")
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                SqlCommand com = new SqlCommand("GetApprovedTaskForAdmin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 600;
                //SqlParameter p1 = new SqlParameter("@CoodId", "0");
                //SqlParameter p2 = new SqlParameter("@deptId", "0");
                //com.Parameters.Add(p1);
                //com.Parameters.Add(p2);

                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = com;
                con.Open();
                da.Fill(ds);
                con.Close();
                string ul = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ul += "<li>" +
                          "<a href='#' class='clearfix'>" +
                                                "<figure class='image'>" +
                                                    "<img src = 'assets/images/!sample-user.jpg' alt='Joseph Doe Junior' class='img-circle' />" +
                                                "</figure>" +
                                               " <span class=title'>New Approved Submission</span>" +
                                                "<span class='message'> You Have a new approved submission from " + dr["StudentName"].ToString() + ". <br/>" + dr["filename"].ToString() + "<br/> Date: " + Convert.ToDateTime(dr["DC"].ToString()).ToString("dd/MM/yyyy") + "</span>" +
                                            "</a>" +
                                        "</li>";
                }
                spanCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
                spanCountAll.InnerHtml = ds.Tables[0].Rows.Count.ToString();
                spanMailcount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
                ulMessageList.InnerHtml = ul;
            }

            #endregion

            
        }

    }
    public void GetRoleName(string roleId)
    {
        if (roleId == "1")
            spanRole.InnerHtml = "Admin";
        else if (roleId == "2")
            spanRole.InnerHtml = "Student";
        else if (roleId == "3")
            spanRole.InnerHtml = "Guest";
        else if (roleId == "5")
            spanRole.InnerHtml = "Marketing Coordinator";
        else if (roleId == "6")
            spanRole.InnerHtml = "Marketing Manager";

    }

    public string SetTitle
    {
        get
        {
            return h2Header.InnerHtml;
        }
        set
        {
            h2Header.InnerHtml = value;
        }
    }
}
