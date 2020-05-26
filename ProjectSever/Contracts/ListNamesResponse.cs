using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProjectSever.Contracts
{
    [DataContract]
    public class ListNamesResponse
    {
        [DataMember]
        public List<string> ListNames{ get; set; }
    }
}
