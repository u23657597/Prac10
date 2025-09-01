using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;


namespace RandomCourseFBook.Models
{
    public class DefaultDataService
    {
        private String ConnectionString;
        public static List<MarkRange> markRanges = null;
      
        public DefaultDataService() {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            markRanges = new List<MarkRange>();
            markRanges.Add(new MarkRange { Symbol = "F", MinOfRange = 0, MaxOfRange = 59 });
            markRanges.Add(new MarkRange { Symbol = "D", MinOfRange = 60, MaxOfRange = 64 });
            markRanges.Add(new MarkRange { Symbol = "D+", MinOfRange = 65, MaxOfRange = 69 });
            markRanges.Add(new MarkRange { Symbol = "C", MinOfRange = 70, MaxOfRange = 74 });
            markRanges.Add(new MarkRange { Symbol = "C+", MinOfRange = 75, MaxOfRange = 79 });
            markRanges.Add(new MarkRange { Symbol = "B", MinOfRange = 80, MaxOfRange = 84 });
            markRanges.Add(new MarkRange { Symbol = "B+", MinOfRange = 85, MaxOfRange = 89 });
            markRanges.Add(new MarkRange { Symbol = "A", MinOfRange = 90, MaxOfRange = 94 });
            markRanges.Add(new MarkRange { Symbol = "A+", MinOfRange = 95, MaxOfRange = 100 });
        }

        public List<Student> getAllStudentsByMarkRange(int min, int max)
        {
            List<Student> students = new List<Student>();
            //TODO: get all students whose grade falls between min and max (inclusive)
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "SELECT * FROM Students WHERE grade BETWEEN " + min + " AND " + max + "";

            SqlCommand GetLearners = new SqlCommand(Query, Connection);
            SqlDataReader reader;

            try
            {
                Connection.Open();
                reader = GetLearners.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student { FirstName = reader["FirstName"].ToString() });
                    students.Add(new Student { Surname = reader["LastName"].ToString() });
                    students.Add(new Student { Sex = reader["Sex"].ToString() });
                    students.Add(new Student { Grade = Convert.ToInt32(reader["Grade"]) });
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
            }
            finally 
            {
                Connection.Close();
            }
            
            return students;
        }

        
        public List<Student> getAllStudentsBySexAndMarkRange(String sex, int min, int max)
        {
            List<Student> students = new List<Student>();
            //TODO: get all students whose grade falls between min and max (inclusive) and whose recorded sex matches the method's first argument

            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection Connection2 = new SqlConnection(ConnectionString);

            string Query = "SELECT * FROM Students WHERE Sex = " + sex + " AND  grade NOT BETWEEN " + min +" AND " + max + "";

            SqlCommand GetLearners2 = new SqlCommand(Query, Connection2);
            SqlDataReader reader2;

            try
            {
                Connection2.Open();
                reader2 = GetLearners2.ExecuteReader();
                while (reader2.Read())
                {
                    students.Add(new Student { FirstName = reader2["FirstName"].ToString() });
                    students.Add(new Student { Surname = reader2["LastName"].ToString() });
                    students.Add(new Student { Sex = reader2["Sex"].ToString() });
                    students.Add(new Student { Grade = Convert.ToInt32(reader2["Grade"]) });
                    reader2.Close();
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
            }
            finally
            {
                Connection2.Close();
            }

            return students;
        }

        public List<Image> getAllImages() {
            List<Image> images = new List<Image>();
            //TODO: Retrieve all the information associated with the images

            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection Connection3 = new SqlConnection(ConnectionString);

            string Query = "SELECT * FROM Images";

            SqlCommand GetLearners3 = new SqlCommand(Query, Connection3);
            SqlDataReader reader3;

            try
            {
                Connection3.Open();
                reader3 = GetLearners3.ExecuteReader();
                while (reader3.Read())
                {
                    images.Add(new Image { StudentID = Convert.ToInt32(reader3["StudentID"]) });
                    images.Add(new Image { ImageRaw = reader3["B64Image"].ToString() });                   
                    reader3.Close();
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
            }
            finally
            {
                Connection3.Close();
            }

            return images;
        }       
    }
}