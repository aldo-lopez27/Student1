using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class MemberPage : System.Web.UI.Page
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
                Member myUser = (from x in dbcon.Members
                                  where x.Member_UserID == userID
                                  select x).First();
                memberFirstName.Text = Convert.ToString(myUser.MemberFirstName);
                memberLastName.Text = Convert.ToString(myUser.MemberLastName);

                Section mySection = (from x in dbcon.Sections
                                     where x.Member_ID == userID
                                     select x).First();

                Instructor myInstructor = (from x in dbcon.Instructors
                                            where x.InstructorID == mySection.Instructor_ID
                                            select x).First();

                // Create a SQL command to select data
                string selectQuery = "SELECT DISTINCT Section.sectionName, Instructor.instructorFirstName, " +
                            "Instructor.instructorLastName, Section.sectionStartDate, Member.memberTotalPayment " +
                            "FROM Section, Instructor, Member " +
                            "WHERE SectionName = @SectionName AND " +
                            "InstructorFirstName = @InstructorFirstName AND " +
                            "InstructorLastName = @InstructorLastName AND " +
                            "SectionStartDate = @SectionStartDate AND " +
                            "MemberTotalPayment = @MemberTotalPayment";

                SqlCommand cmd = new SqlCommand(selectQuery, new SqlConnection(con));
                using (cmd)
                {
                    // Assuming you have a SqlConnection named yourSqlConnection
                    cmd.Parameters.AddWithValue("@SectionName", mySection.SectionName);
                    cmd.Parameters.AddWithValue("@InstructorFirstName", myInstructor.InstructorFirstName);
                    cmd.Parameters.AddWithValue("@InstructorLastName", myInstructor.InstructorLastName);
                    cmd.Parameters.AddWithValue("@SectionStartDate", mySection.SectionStartDate);
                    cmd.Parameters.AddWithValue("@MemberTotalPayment", myUser.MemberTotalPayment);

                    // Now execute your query using the SqlCommand
                }
                // Create a data adapter to fill a dataset
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                System.Data.DataSet ds = new System.Data.DataSet();

                // Fill the dataset with data from the database
                adapter.Fill(ds);

                // Set the dataset as the data source for the GridView
                gridViewMember.DataSource = ds;
                gridViewMember.DataBind();
            }
        }



    }
}