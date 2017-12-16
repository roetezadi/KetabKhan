using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhan.DB
{
    public class DB_Helper
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-T590RA7\\SQLEXPRESS;Initial Catalog=DBLab;Integrated Security=True");

        public void InsertToExam(long ExamID, long UserID)
        {
            con.Open();
            //we should write the sql command in sql server in programabality and create a stored procedure and write the query and execute it
            SqlCommand cmd = new SqlCommand("InsertToExam", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamID", SqlDbType.BigInt).Value = ExamID;
            cmd.Parameters.AddWithValue("@ChatID", SqlDbType.BigInt).Value = UserID;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertToExamQuestion(string Question, long QuestionID, long ExamID)
        {
            con.Open();
            //we should write the sql command in sql server in programabality and create a stored procedure and write the query and execute it
            SqlCommand cmd = new SqlCommand("InsertToExamQuestion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Question", SqlDbType.Text).Value = Question;
            cmd.Parameters.AddWithValue("@QuestionID", SqlDbType.BigInt).Value = QuestionID;
            cmd.Parameters.AddWithValue("@ExamID", SqlDbType.BigInt).Value = ExamID;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertToExamChoice(string Choice, long ChoiceID, long QuestionID)
        {
            con.Open();
            //we should write the sql command in sql server in programabality and create a stored procedure and write the query and execute it
            SqlCommand cmd = new SqlCommand("InsertToExamChoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Choice", SqlDbType.Text).Value = Choice;
            cmd.Parameters.AddWithValue("@ChoiceID", SqlDbType.BigInt).Value = ChoiceID;
            cmd.Parameters.AddWithValue("@QuestionID", SqlDbType.BigInt).Value = QuestionID;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
