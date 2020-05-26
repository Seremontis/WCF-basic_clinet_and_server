using ProjectSever.DTO;
using ProjectSever.Enums;
using ProjectSever.Contracts;
using ProjectSever.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ProjectSever.ServiceImplementations
{
    public class MiniDB : IMiniDB
    {
        #region ctor and fields
        readonly string DirectoryPath;

        public MiniDB()
        {
            DirectoryPath = Directory.GetCurrentDirectory() + "\\";
        }
        #endregion

        #region services
        public ReadFileResponse ReadFile(ReadFileRequest request)
        {
            if (request.NameFile == null)
                return null;
            ReadFileResponse dataTable = new ReadFileResponse();
            
            string path = DirectoryPath + request.NameFile;
            try
            {
                dataTable.Result =Helpers.LoadData(path) ;
                return dataTable;
            }
            catch (Exception)
            {
                Helpers.ReturnRequest();
                return null;
            }
        }

        public ListNamesResponse GetListName()
        {
            ListNamesResponse response = new ListNamesResponse()
            {
                ListNames = new List<string>()
            };
            string[] array = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
            foreach (var item in array)
            {
                string value = item.Substring(item.LastIndexOf("\\")).Replace("\\", string.Empty);
                value = value.Substring(0, value.IndexOf('.'));
                response.ListNames.Add(value);
            }
            return response;
        }

        public bool SaveFile(DataTable table)
        {
            string result;
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    table.WriteXml(sw);
                    result = sw.ToString();
                    using (StreamWriter writer = new StreamWriter(table.TableName+".xml"))
                    {
                        writer.Write(sw);
                    }
                    
                }
                catch (Exception)
                {

                    return false;
                }
                
            }
            return true;
        }

        public SummaryDataResponse GetSummary(SummaryDataRequest request)
        {
            var data = Helpers.LoadData(request.FileName);
            SummaryDataResponse response = new SummaryDataResponse()
            {
                FileName = data.TableName,
                ColumnName = request.ColumnName,
                Operation = request.Operation,
            };
            if (request.ColumnName!="*" && request.GroupColumnName=="*")
                response.Value= Helpers.ReturnResultStatsColumn(request.ColumnName,request.Operation,data);
            else if (request.ColumnName == "*" && request.GroupColumnName == "*")
            {
                response.ListValue = new List<GroupByList>();
                foreach (DataColumn item in data.Columns)
                {
                    GroupByList groupByList = new GroupByList();
                    groupByList.Name = item.ColumnName;
                    groupByList.Result=Helpers.ReturnResultStatsColumn(item.ColumnName,request.Operation, data);
                    response.ListValue.Add(groupByList);
                }
            }                
            else
                response.ListValue = Helpers.ReturnResultStatsList(request,data);

            return response;
        }

        public FileTransferResponse GetFile(FileTransferRequest request)
        {
            string path = DirectoryPath + request.FileName;
            try
            {
                if (request.FileType == FileType.XML)
                {
                    return Helpers.PreapreToSend(path);
                }
                else
                {
                    Helpers.ToCsv(path,DirectoryPath);
                    var result=Helpers.PreapreToSend(DirectoryPath + "tmp.csv");
                    File.Delete(DirectoryPath + "tmp.csv");
                    return result;
                }      
            }
            catch (Exception)
            {
                Helpers.ReturnRequest();
                return null;
            }
        }

        #endregion
    }

}
