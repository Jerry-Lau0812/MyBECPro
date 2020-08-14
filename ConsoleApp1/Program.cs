using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            private funcation  GetDataTableFromIDataReader(IDataReader reader) as DataTable
            {
                DataTable dt = new DataTable();
                bool init = false;
                dt.BeginLoadData();
                object[] vals = new object[0];
                while (reader.Read())
                {
                    if (!init)
                    {
                        init = true;
                        int fieldCount = reader.FieldCount;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }
                        vals = new object[fieldCount];
                    }
                    reader.GetValues(vals);
                    dt.LoadDataRow(vals, true);
                }
                reader.Close();
                dt.EndLoadData();
                return dt;
            }

        }
        
    }
}
