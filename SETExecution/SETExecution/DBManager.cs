using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SETExecution
{
    class DBManager
    {
        //check the default cnDB, this will change based of what your local server is
        private static string _cnDB = SETExecution.Properties.Settings.Default.cnDB;


        public static void ClearDB()
        {
            string sqlQuery = "Delete From final Delete From history Delete From history_authors Delete From history_repos Delete From joiners Delete From leavers Delete From seniority Delete From repos delete from authors";
            try
            {
                using (SqlConnection cn = new SqlConnection(_cnDB))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }//end using
            }//end try

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();

            }//end catch

        }
        public static void ExecuteStatements()
        {
            
            //this file path will change
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Lauren\Documents\Research\Round2\GNOMEDataSet.txt");
            string sqlQuery = "";
            string line;
            int counter = 0;
            int totalCount = 0;

            while (((line = file.ReadLine()) != null)) {
                if (!line.Equals("go", StringComparison.OrdinalIgnoreCase))
                {
                    sqlQuery += line  + "\n";
                    if (counter == 10)
                    {
                        try
                        {
                            using (SqlConnection cn = new SqlConnection(_cnDB))
                            {
                                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cn.Open();
                                    cmd.ExecuteNonQuery();
                                    cn.Close();
                                }
                            }//end using
                        }//end try

                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.Message);
                            Console.WriteLine(line + "\n");
                           // Console.WriteLine("\n" + sqlQuery + "\n\n\n");

                           

                        }//end catch

                        counter = 0;
                        sqlQuery = "";
                    }//end if
                    counter++;
                    
               }

                totalCount++;

            }//end while
            
            
        }
    }
}
