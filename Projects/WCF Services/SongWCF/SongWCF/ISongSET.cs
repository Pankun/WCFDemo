using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SongDAO;


namespace SongWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISongSET" in both code and config file together.
    [ServiceContract]
    public interface ISongSET
    {
        /// <summary>
        /// WCF Interface of Adding Song Information
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "AddSongInfo/{TheArtist}/{TheAlbum}/{TheTitle}/{TheLength}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FunctionReturnObject AddSongInfo(string TheArtist, string TheAlbum, string TheTitle, string TheLength);

        /// <summary>
        /// WCF Interface of Updating Song Information
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheSongId">Song ID</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "UpdateSongInfo/{TheArtist}/{TheAlbum}/{TheSongId}/{TheTitle}/{TheLength}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FunctionReturnObject UpdateSongInfo(string TheArtist, string TheAlbum, string TheSongId, string TheTitle, string TheLength);
    }
}
