using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.ServiceModel.Activation;
using SongDAO;
using LocalAppLog;

namespace SongWCF
{
     /// <summary>
    /// Song GET WCF Services
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SongGET : ISongGET
    {
       
        private LogApp TheLog = new LogApp();
        private string ApplicationName = "SongWCF";

        /// <summary>
        /// Get Album and Song information
        /// </summary>
        /// <param name="AlbumName">Album Name</param>
        /// <returns>
        /// album
        /// </returns>        
        public album GetAlbumInfo(string AlbumName)
        {
            album TheAlbum = new album();
            FunctionReturnObject theReturnObject = new FunctionReturnObject();
            AlbumApp theAlbumApp = new AlbumApp();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=GetAlbumInfo ||AlbumName=" +AlbumName );

            theReturnObject=theAlbumApp.AlbumAppGetInfoByTitle(AlbumName);
            if (theReturnObject.ReturnObject == null)
            {
                TheLog.Addlog("Application=" + ApplicationName + " ||Function=GetAlbumInfo ||AlbumName=" + AlbumName + "|| Message= Album not found.");
                //theReturnObject.ReturnObject = "Not Found."; //Error ==> not as a Album object
            }
            else
            {
                TheLog.Addlog("Application=" + ApplicationName + " ||Function=GetAlbumInfo ||AlbumName=" + AlbumName + "|| Message= Album found.");
            }
            return (album)theReturnObject.ReturnObject;
        }
     }
}
