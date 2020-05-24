using ProjectClient.ServerProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.ServiceModel;

namespace ProjectClient
{
    public class ChannelProjectServer
    {
        #region fields and getters
        private ISimpleInfo proxy = null;
        private ChannelFactory<ISimpleInfo> factory = null;
        public string ErroMessage { get; private set; } = string.Empty;
        #endregion

        #region public methods
        public bool ConnectWCF()
        {
            ErroMessage = string.Empty;
            try
            {
                Uri uri = new Uri("http://localhost:2333/");//SimpleInfoClient client =new SimpleInfoClient();
                WebRequest wr = WebRequest.Create(uri);
                wr.GetResponse();
                factory = new ChannelFactory<ISimpleInfo>(new BasicHttpBinding(),
                new EndpointAddress(uri));
                proxy = factory.CreateChannel();
                return true;
            }
            catch (Exception e)
            {
                ErroMessage = e.Message;
                return false;
            }
        }
        public List<string> GetNamesList()
        {
            var response = proxy.GetListName();
            return response.ListNames.ToList();
        }
        public DataTable LoadFromServer(string fileName)
        {
            ErroMessage = string.Empty;
            ReadFileRequest request = new ReadFileRequest(){ NameFile = fileName};
            DataTable table = new DataTable();
            try
            {
                var response = proxy.ReadFile(request);
                return response.Result;
            }
            catch (Exception e)
            {
                ErroMessage = e.Message;
                return null;
            }
        }
        public Tuple<double?,List<GroupByList>> GetStats(string filename,string columname,string groupname,OperationOnTable operation)
        {
            ErroMessage = string.Empty;
            SummaryDataRequest request = new SummaryDataRequest()
            {
                FileName = filename,
                ColumnName = columname,
                GroupColumnName=groupname,
                Operation = operation
            };
            try
            {
                var result = proxy.GetSummary(request);
                return new Tuple<double?, List<GroupByList>>(result.Value, result.ListValue.ToList());

            }
            catch (Exception e)
            {
                ErroMessage = e.Message;
                return null;
            }
           
        }
        public bool SaveToServer(DataTable table,string name)
        {        
            table.TableName=name;
            ErroMessage = string.Empty;
            try
            {
                bool response = proxy.SaveFile(table);
                return response;
            }
            catch (Exception e)
            {
                ErroMessage = e.Message;
                return false;
            }

        }
        public byte[] GetFile(string name,FileType type)
        {
            FileTransferRequest request = new FileTransferRequest()
            {
                FileName = name,
                FileType = type
            };
            var result = proxy.GetFile(request);
            return result.File;
        }

        #endregion
    }
}
