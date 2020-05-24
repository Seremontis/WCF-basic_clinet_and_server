using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProjectSever.Messages
{
    [DataContract]
    public class ListNamesResponse
    {
        [DataMember]
        public List<string> ListNames{ get; set; }
    }
}
