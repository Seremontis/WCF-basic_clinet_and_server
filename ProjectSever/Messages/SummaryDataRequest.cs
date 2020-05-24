using ProjectSever.Enums;
using System.Runtime.Serialization;

namespace ProjectSever.Messages
{
    [DataContract]
    public class SummaryDataRequest
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string GroupColumnName { get; set; }
        [DataMember]
        public OperationOnTable Operation { get; set; }
    }
}
