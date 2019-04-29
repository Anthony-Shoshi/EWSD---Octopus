using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Guest_GuestMasterPage : System.Web.UI.MasterPage
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");
        
        }
        spUserName.InnerHtml = Session["UserName"].ToString();
        GetRoleName(Session["RoleId"].ToString());
        if (Session["RoleId"].ToString() == "1")
        {
            homeUrl.InnerHtml = "<a href='../Admin/Default.aspx'>" +
                                        "<i class='fa fa-home' aria-hidden='true'></i>"+
                                        "<span>Home</span>"+
                                    "</a>";
            liReport.Visible = true;
            liMail.Visible = false;
            notificationSpan.Visible = true;
            #region Notification Part
            if (Session["RoleId"].ToString() == "1")
            {
                SqlConnection con;
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                SqlCommand com = new SqlCommand("GetApprovedTaskForAdmin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 600;
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
                ulMessageList.InnerHtml = ul;
            }

            #endregion

        }
        if (Session["RoleId"].ToString() == "6")
        {
            homeUrl.InnerHtml = "<a href='../MarketingManager/Default.aspx'>" +
                                        "<i class='fa fa-home' aria-hidden='true'></i>" +
                                        "<span>Home</span>" +
                                    "</a>";
            liReport.Visible = true;
            BindGrid(0, 0);
            notificationSpan.Visible = true;
           // liMail.Visible = true;
        }

    }
    public void BindGrid(int cordId, int deptId)
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("GetTaskForCoodinator", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@CoodId", cordId);
        SqlParameter p2 = new SqlParameter("@deptId", deptId);
        com.Parameters.Add(p1);
        com.Parameters.Add(p2);

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
                                       " <span class=title'>New Submission</span>" +
                                        "<span class='message'>New submission from " + dr["StudentName"].ToString() + ". <br/> Dept: " + dr["StudentDept"].ToString() + "<br/> Date: " + Convert.ToDateTime(dr["DC"].ToString()).ToString("dd/MM/yyyy") + "</span>" +
                                    "</a>" +
                                "</li>";
        }
        spanCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        spanCountAll.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        ulMessageList.InnerHtml = ul;

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
}
