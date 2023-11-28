using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class InstructorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve user information from session
                int userID = Convert.ToInt32(Session["userID"]);


                PagesDataContext dbcon;
                string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";


                dbcon = new PagesDataContext(con);
                Instructor myUser = (from x in dbcon.Instructors
                                 where x.InstructorID == userID
                                 select x).First();
                instructorFirstName.Text = Convert.ToString(myUser.InstructorFirstName);
                instructorLastName.Text = Convert.ToString(myUser.InstructorLastName);


                // Retrieve sections and related members for the instructor
                var query = from section in dbcon.Sections
                            where section.Instructor_ID == userID
                            join member in dbcon.Members on section.Member_ID equals member.Member_UserID
                            select new
                            {
                                section.SectionID,
                                section.SectionName,
                                member.MemberFirstName,
                                member.MemberLastName
                                // Add more properties as needed
                            };

                // Bind the query result to the GridView
                gridViewInstructor.DataSource = query.ToList();
                gridViewInstructor.DataBind();
            }
        }
        
    }
}