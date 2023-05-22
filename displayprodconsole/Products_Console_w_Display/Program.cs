// See https://aka.ms/new-console-template for more information


using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Npgsql;

class Sample
{
    static void Main(string[] args)
    {
        // Connect to a PostgreSQL database
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1:5432;User Id=postgres; " +
           "Password=password1;Database=prods;");
        conn.Open();

        // Define a query returning a single row result set
        NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM product", conn);
        

        // Execute the query and obtain the value of the first column of the first row
        //Int64 count = (Int64)command.ExecuteScalar();
        NpgsqlDataReader reader1 = command1.ExecuteReader();
        DataTable dtp = new DataTable();
        dtp.Load(reader1);

        NpgsqlCommand command2 = new NpgsqlCommand("SELECT * FROM customer", conn);
        NpgsqlDataReader reader2 = command2.ExecuteReader();
        DataTable dtc = new DataTable();
        dtc.Load(reader2);

        print_results_num7(dtp);

        results20(dtc);

        conn.Close();
    }

    /*static void print_results(DataTable data)
    {
        Console.WriteLine();
        Dictionary<string, int> colWidths = new Dictionary<string, int>();

        foreach (DataColumn col in data.Columns)
        {
            Console.Write(col.ColumnName);
            var maxLabelSize = data.Rows.OfType<DataRow>()
                    .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                    .OrderByDescending(m => m).FirstOrDefault();

            colWidths.Add(col.ColumnName, maxLabelSize);
            for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 14; i++) Console.Write(" ");
        }

        Console.WriteLine();

        foreach (DataRow dataRow in data.Rows)
        {
            for (int j = 0; j < dataRow.ItemArray.Length; j++)
            {
                Console.Write(dataRow.ItemArray[j]);
                for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 14; i++) Console.Write(" ");
            }
            Console.WriteLine();
        }
    }*/

    static void print_results_num7(DataTable data)
    {
        Console.WriteLine();
        Dictionary<string, int> colWidths = new Dictionary<string, int>();

        foreach (DataColumn col in data.Columns)
        {

            if (col.ColumnName is ("prod_id" or "prod_desc" or "prod_quantity"))
            {
                Console.Write(col.ColumnName);
            }
                var maxLabelSize = data.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 14; i++) Console.Write(" ");

            
        }

        Console.WriteLine();

        foreach (DataRow dataRow in data.Rows)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Convert.ToInt32(dataRow.ItemArray[2]) is >= 12 and <= 30)
                {
                    Console.Write(dataRow.ItemArray[j]);

                    for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 14; i++) Console.Write(" ");

                }
            }
                    Console.WriteLine();
                
            
        }
    }


    
    public static void results20(DataTable data)
    {
        Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
        decimal sum;
        foreach (DataRow row in data.Rows)
        {


            //Console.WriteLine(row["rep_id"].ToString());

            if (!dict.ContainsKey(row["rep_id"].ToString()))
            {
                sum = (decimal)row["cust_balance"];
                dict.Add(row["rep_id"].ToString(),sum);
            }
            else
            {
                sum = dict[row["rep_id"].ToString()] + (decimal)row["cust_balance"];
                dict[row["rep_id"].ToString()] = sum;
            }
        }

        foreach (var item in dict) 
        {
            if (item.Value >= 12000)
            {
                Console.WriteLine(item.Key + "    " + item.Value);
            }
        }


    }
    
}


