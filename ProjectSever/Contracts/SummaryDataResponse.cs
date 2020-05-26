using ProjectSever.DTO;
using ProjectSever.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProjectSever.Contracts
{
    [DataContract]
    public class SummaryDataResponse
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public OperationOnTable Operation { get; set; }
        [DataMember]
        public double? Value { get; set; }
        [DataMember]
        public List<GroupByList> ListValue { get; set; }
    }
}
