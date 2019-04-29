using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mail_MailMasterPage : System.Web.UI.MasterPage
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");

        }
        #region Notification Part
        if (Session["RoleId"].ToString() == "5")
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            SqlCommand com = new SqlCommand("GetTaskForCoodinator", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@CoodId", Session["UserId"]);
            SqlParameter p2 = new SqlParameter("@deptId", "0");
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
                                            "<span class='message'> You Have a new submission from " + dr["StudentName"].ToString() + ". <br/>" + dr["filename"].ToString() + "<br/> Date: " + Convert.ToDateTime(dr["DC"].ToString()).ToString("dd/MM/yyyy") + "</span>" +
                                        "</a>" +
                                    "</li>";
            }
            spanCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
            spanCountAll.InnerHtml = ds.Tables[0].Rows.Count.ToString();
            ulMessageList.InnerHtml = ul;
            spaninboxCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
            mailCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
            liNavHome.InnerHtml = "<a href='../MarketingCoordinator/Default.aspx'>" +
                                       "<i class='fa fa-home' aria-hidden='true'></i>" +
                                        "<span>Home</span>" +
                                    "</a>";
        }
        if (Session["RoleId"].ToString() == "2")
        {
            liNavHome.InnerHtml = "<a href='../Student/Default.aspx'>" +
                                       "<i class='fa fa-home' aria-hidden='true'></i>" +
                                        "<span>Home</span>" +
                                    "</a>";
            Notificationforstudent();
        }


            #endregion
            spUserName.InnerHtml = Session["UserName"].ToString();
        GetRoleName(Session["RoleId"].ToString());
        BindEmailNotification();
    }
    public string SetTitle
    {
        get
        {
            return spanSent.InnerHtml;
        }
        set
        {
            spanSent.InnerHtml = value;
        }
    }
    public void BindEmailNotification()
    {
        try
        {
            int studentid = 0;
            int cordid = 0;

            if (Session["RoleId"].ToString() == "5")
            {
                cordid = Convert.ToInt32(Session["UserId"]);
                studentid = 0;
            }
            else if (Session["RoleId"].ToString() == "2")
            {
                studentid = Convert.ToInt32(Session["UserId"]);
                cordid = 0;
            }
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            SqlCommand com = new SqlCommand("GetMail", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@studentId", studentid);
            SqlParameter p2 = new SqlParameter("@cordid", cordid);
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = com;
            con.Open();
            da.Fill(ds);
            con.Close();
            string ul = "";
            DataTable dt = ds.Tables[0];
            var filtered = dt.AsEnumerable().Where(r => r.Field<String>("fromMail").Contains(Session["Email"].ToString()));
            spanSent.InnerHtml = filtered.Count().ToString();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void Notificationforstudent()
    {
        spUserName.InnerHtml = Session["UserName"].ToString();
        GetRoleName(Session["RoleId"].ToString());
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("GetAllMail", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@studentId", Session["UserId"]);
        //SqlParameter p2 = new SqlParameter("@cordid", "0");
        com.Parameters.Add(p1);
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
                                       " <span class=title'>" + dr["CoordinatiorName"].ToString() + "</span>" +
                                        "<span class='message'>" + dr["mailBody"].ToString() + "</span>" +
                                    "</a>" +
                                "</li>";
        }
        spanCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        spanCountAll.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        ulMessageList.InnerHtml = ul;
        mailCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        spaninboxCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();
        mailCount.InnerHtml = ds.Tables[0].Rows.Count.ToString();

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
