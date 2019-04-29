using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentMasterPage : System.Web.UI.MasterPage
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
            GetDepartmentName(Session["DepId"].ToString());

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
    public void GetDepartmentName(string deptId)
    {
        if (deptId == "1")
            spanDept.InnerHtml = " of Information Technology Department";
        else if (deptId == "2")
            spanDept.InnerHtml = " of Mathematics Department";
        else if (deptId == "3")
            spanDept.InnerHtml = " of History Department";
    }
}
