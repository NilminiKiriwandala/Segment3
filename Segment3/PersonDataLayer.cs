using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Segment3
{
  public class PersonDataLayer
    {
        public static string ConnectionString() 
        {
            string con = string.Empty;
            con = "Data Source=TASP13SERVER;Initial Catalog=test;Integrated Security=True";
            return con;
        
        }
        public static bool AddPerson(PersonBusinessLayer person)
        {
            bool success = false;
            string cmdString = "INSERT INTO Person (LastName,FirstName,Gender,DOB) VALUES (@val1, @val2, @val3,@val4)";
            string connString = ConnectionString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@val1", person.LastName);
                    comm.Parameters.AddWithValue("@val2", person.FirstName);
                    comm.Parameters.AddWithValue("@val3", person.Gender);
                    comm.Parameters.AddWithValue("@val4", person.DOB);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        success = true;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
            return success;
        }

        public static bool  UpdatePerson(PersonBusinessLayer person)
        {
            bool success = false;
            string cmdString = "UPDATE Person SET LastName=@val1,FirstName= @val2, Gender=@val3,DOB=@val4 WHERE ID=@val0";
            string connString = ConnectionString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@val0", person.ID);
                    comm.Parameters.AddWithValue("@val1", person.LastName);
                    comm.Parameters.AddWithValue("@val2", person.FirstName);
                    comm.Parameters.AddWithValue("@val3", person.Gender);
                    comm.Parameters.AddWithValue("@val4", person.DOB);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        success = true;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
            return success;
        }
     

        public static PersonBusinessLayer FindPerson(int id) 
        {
            PersonBusinessLayer person = null;
                
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ID, LastName, FirstName, Gender, DOB FROM Person WHERE ID = @NM", con))
                {
                    cmd.Parameters.AddWithValue("@NM", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           person = new PersonBusinessLayer();
                           person.ID = (int)reader["ID"];
                           person.LastName = (string)reader["LastName"];
                           person.FirstName = (string)reader["FirstName"];
                           person.Gender = (string)reader["Gender"];
                           person.DOB = (DateTime)reader["DOB"];
                        }
                    }
                }
            }
            return person;
        
        }

        public static void  FillDataset(ref DataSet ds, bool searchForMales) 
        {
            string SelectCmd=string.Empty;
            if (!searchForMales)
            {
                SelectCmd = "SELECT * FROM Person";
            }
            else
            {
                SelectCmd = "SELECT * FROM Person WHERE Gender = 'Male'";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectCmd,ConnectionString()); 

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
             ds = new DataSet();
            dataAdapter.Fill(ds);
   
        }

    }
}
