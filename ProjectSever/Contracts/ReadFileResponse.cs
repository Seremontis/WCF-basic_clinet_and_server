using System.Data;
using System.Runtime.Serialization;

namespace ProjectSever.Contracts
{
    [DataContract]
    public class ReadFileResponse
    {
        [DataMember]
        public DataTable Result { get; set; }
    }
}
