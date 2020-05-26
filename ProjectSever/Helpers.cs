using ProjectSever.DTO;
using ProjectSever.Enums;
using ProjectSever.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;

namespace ProjectSever
{
    public static class Helpers
    {
        #region public methods

        #region prepare object methods
        public static OutgoingWebResponseContext ReturnRequest()
        {
            OutgoingWebResponseContext responseContext = WebOperationContext.Current.OutgoingResponse;
            responseContext.StatusCode = System.Net.HttpStatusCode.BadRequest;
            responseContext.StatusDescription = "Wystąpił problem z odczytem danych";
            return responseContext;
        }

        public static FileTransferResponse PreapreToSend(string path)
        {
            path+= ".xml";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                FileTransferResponse file = new FileTransferResponse()
                {
                    File = bytes,
                    NameFile = fs.Name.Substring(fs.Name.LastIndexOf("\\")).Replace("\\", string.Empty)
                };
                return file;
            }
        }
        #endregion

        #region Operation on data
        public static List<GroupByList> ReturnResultStatsList(SummaryDataRequest request, DataTable data)
        {
            List<GroupByList> result = new List<GroupByList>();
            switch (request.Operation)
            {
                case OperationOnTable.SUM:
                    if (request.ColumnName != "*")
                    {
                        List<GroupByList> lists = CreateListCompute(data, request.ColumnName, request.GroupColumnName);
                        result = lists.GroupBy(t => t.Name).Select(g => new GroupByList
                        {
                            Result = g.Sum(p => p.Result),
                            Name = g.Key
                        }).ToList();
                    };
                    break;
                case OperationOnTable.AVG:
                    if (request.ColumnName != "*")
                    {
                        List<GroupByList> lists = CreateListCompute(data, request.ColumnName, request.GroupColumnName);
                        result = lists.GroupBy(t => t.Name).Select(g => new GroupByList
                        {
                            Result = g.Average(p => p.Result),
                            Name = g.Key
                        }).ToList();
                    }
                    else
                    {

                    };
                    break;
                case OperationOnTable.COUNT:
                    var list = CreateListCount(data, request.GroupColumnName);
                    result = (from res in list
                              group res by res into resGroup
                              select new GroupByList
                              {
                                  Name = resGroup.Key,
                                  Result = resGroup.Count(),
                              }).ToList();
                    break;
                default:
                    break;
            }
            return result;
        }

        public static double ReturnResultStatsColumn(string columnname, OperationOnTable operation, DataTable data)
        {
            double result = 0;
            switch (operation)
            {
                case OperationOnTable.SUM:
                    result = ReturnListNumber(data, columnname).Sum();
                    break;
                case OperationOnTable.AVG:
                    result = ReturnListNumber(data, columnname).Average();
                    break;
                default:
                    break;
            }
            return result;
        }

        public static DataTable LoadData(string path)
        {
            DataSet test = new DataSet();
            using (Stream reader = new FileStream(path + ".xml", FileMode.Open, FileAccess.Read))
                test.ReadXml(reader);

            return test.Tables[0];
        }

        public static void ToCsv(string path,string DirectoryPath)
        {
            DataTable dataTable = LoadData(path);
            StreamWriter sw = new StreamWriter(DirectoryPath + "tmp.csv", false);
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sw.Write(dataTable.Columns[i]);
                if (i < dataTable.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(';'))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dataTable.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        #endregion

        #endregion

        #region private methods
        private static List<string> CreateListCount(DataTable data, string columnName)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in data.Rows)
                list.Add((string)row[columnName]);
            return list;
        }

        private static List<GroupByList> CreateListCompute(DataTable data, string columnName, string groupName)
        {
            List<GroupByList> list = new List<GroupByList>();
            foreach (DataRow row in data.Rows)
            {
                double.TryParse(row[columnName].ToString().Replace(".",","), out double value);
                list.Add(new GroupByList() { Name = row[groupName].ToString(), Result = value });
            }
            return list;
        }

        private static List<double> ReturnListNumber(DataTable data, string columnName)
        {
            List<double> list = new List<double>();
            foreach (DataRow row in data.Rows)
            {
                double.TryParse(row[columnName].ToString().Replace(".",","), out double value);
                list.Add(value);
            }
            return list;
        }
        #endregion

    }
}
