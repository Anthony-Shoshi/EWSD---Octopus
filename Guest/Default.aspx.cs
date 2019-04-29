using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Guest_Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindYear();
                GetrptContributionsWithoutComment();
                GetrptNumberOfContribution();
                GetrptNumberOfContributionpercentage();
            }
        }
        else
            Response.Redirect("~/Presentation/frmLogin.aspx");

    }

    public void BindYear()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 2; i >= 0; i--)
        {
            ddlYear.Items.Add((currentYear - i).ToString());
            ddlYear2.Items.Add((currentYear - i).ToString());
            ddlyear3.Items.Add((currentYear - i).ToString());
        }
    }

    public void GetrptContributionsWithoutComment()
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("GetRptcontributionsWithoutComment", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("Year", ddlYear.SelectedValue);
        com.Parameters.Add(p1);

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da.SelectCommand = com;
        con.Open();
        da.Fill(ds);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            chartWithoutcomment.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = row["Header"].ToString(),
                Data = Convert.ToDecimal(row["WithoutComment"])
            });
        }
    }
    public void GetrptNumberOfContribution()
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("NumberandperContributionByDept", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@Year", ddlYear2.SelectedValue);
        com.Parameters.Add(p1);

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da.SelectCommand = com;
        con.Open();
        da.Fill(ds);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            contributorNumbers.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = row["DepartmentName"].ToString(),
                Data = Convert.ToDecimal(row["Number"])
            });
        }
    }
    public void GetrptNumberOfContributionpercentage()
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("NumberandperContributionByDept", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@Year", ddlyear3.SelectedValue);
        com.Parameters.Add(p1);

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da.SelectCommand = com;
        con.Open();
        da.Fill(ds);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            chartpercentage.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = row["DepartmentName"].ToString(),
                Data = Convert.ToDecimal(row["percentage"])
            });
        }
    }
    protected void ddlYear2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetrptNumberOfContribution();
    }

    protected void ddlyear3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetrptNumberOfContributionpercentage();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetrptContributionsWithoutComment();
    }
}