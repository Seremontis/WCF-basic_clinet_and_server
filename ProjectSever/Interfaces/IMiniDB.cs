using ProjectSever.Contracts;
using System.Data;
using System.ServiceModel;

namespace ProjectSever.Interfaces
{
    [ServiceContract]
    public interface IMiniDB
    {

        [OperationContract]
        [XmlSerializerFormat]
        ReadFileResponse ReadFile(ReadFileRequest request);

        [OperationContract]
        [XmlSerializerFormat]
        bool SaveFile(DataTable table);

        [OperationContract]
        [XmlSerializerFormat]
        ListNamesResponse GetListName();

        [OperationContract]
        [XmlSerializerFormat]
        SummaryDataResponse GetSummary(SummaryDataRequest request);

        [OperationContract]
        [XmlSerializerFormat]
        FileTransferResponse GetFile(FileTransferRequest request);
    }
}
