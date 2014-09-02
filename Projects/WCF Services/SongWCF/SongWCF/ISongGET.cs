using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SongWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISongGET" in both code and config file together.
    [ServiceContract]
    public interface ISongGET
    {
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAlbumInfo/{AlbumName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        album GetAlbumInfo(string AlbumName);

               
    }
}
