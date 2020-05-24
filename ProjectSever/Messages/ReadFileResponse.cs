using System.Data;
using System.Runtime.Serialization;

namespace ProjectSever.Messages
{
    [DataContract]
    public class ReadFileResponse
    {
        [DataMember]
        public DataTable Result { get; set; }
    }
}
