using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetStarted_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into users values(@firstname,@lastname,@role,@department,@email,@phone,@password)", con);
            cmd.Parameters.AddWithValue("firstname", txtFirstName.Text);
            cmd.Parameters.AddWithValue("lastname", txtLastName.Text);
            cmd.Parameters.AddWithValue("role", ddRole.Text);
            cmd.Parameters.AddWithValue("department", ddDepartment.Text);
            cmd.Parameters.AddWithValue("email", txtEmailAddress.Text);
            cmd.Parameters.AddWithValue("phone", txtPhoneNumber.Text);
            cmd.Parameters.AddWithValue("password", txtPassword.Text);
            cmd.ExecuteNonQuery();


            txtFirstName.Text = "";
            txtLastName.Text = "";
            ddRole.Text = "";
            ddDepartment.Text = "";
            txtEmailAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtPassword.Text = "";

       
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Presentation/frmLogin.aspx");
    }
}