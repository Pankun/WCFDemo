Coding Exercise:
1. [WCF Applications] You are tasked with writing WCF service methods capable of exposing song album information to clients. The source information is available in the form of an XML file containing information about various song albums. (Please see the attached song.xml file for some sample info). Provide the following functionality

                a.            Get album details by name. (Return information about Songs in album.)
                b.            Add songs to an album
                c.            Update song info

                While designing your services make the following considerations-

                a.     Handle Large XML files / Caching in memory.
                b.     Handle Concurrent access and large number of requests.
==============================================================================
Target accomplishment:
1.	Get album details by name. (Return information about Songs in album.)
- Created AlbumApp Data Access Object and SongGET WCF service to implement this requirement.
2.	Add songs to an album
- Created Song App Data Access Object and Song SET WCF service to implement this requirement.
3.	Update song info
- Created Song App Data Access Object and Song SET WCF service to implement this requirement.
4.	Handle Large XML files / Caching in memory.
- To handle large XML files ==> using XML reader to access XML data
- To handle caching in memory ==> using XML document and XML Linq to access XML data
5.	Handle Concurrent access and large number of requests
- Created concurrent control methods in Song SET service (SET service will modify the XML file) 
- Created Global.asax for global access flag control
- Using Dekker's algorithm


Project Design Structure:
The structure chart: (the chart is in another picture file)
 
This is a 4-tiers and MVC structure.
•	Data Source : XML file 
•	Data Access Objects : Search, Add, and Update methods to Data Source
•	WCF Service Provider : Search, Add, and Update services
•	Front end web pages: Provide UI for search, add, update (By using Ajax calls to access the data from WCF services)


Project Demo Link:
http://webdemo.homedns.org or http://webdemo.dyndns.org

Project Document List
1.	Song WCF Provider Visual Studio Solution
a.	Song Data Access Object Project (SongDAO)
b.	Song WCF Service Project (SongWCF)
c.	Local App Log Project (LocalAppLog)
2.	Program API document (chm file)
a.	SongWCFRestAPI.chm
b.	SongDAO_Library.chm
3.	WCF Project Design Structure (png picture file)
4.	Web site files (pages and javascript)





