using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETExecution
{
    class Program
    {
        static void Main(string[] args)
        {

            DBManager.ClearDB();
            DBManager.ExecuteStatements();
            Console.WriteLine("Execution Complete");
            Console.ReadLine();
        }
    }
}
