using ProjectSever.Messages;
using System.Data;
using System.ServiceModel;

namespace ProjectSever.ServiceContracts
{
    [ServiceContract]
    public interface ISimpleInfo
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
