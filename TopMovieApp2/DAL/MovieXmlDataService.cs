using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using TopMovieApp.Models;

namespace TopMovieApp.DAL
{
    public class MovieXmlDataService : IMovieDataService, IDisposable
    {
        public List<Movie> Read()
        {
            // a movies model is defined to pass a type to the XmlSerializer object 
            Movies moviesObject;

            // initialize a FileStream object for reading
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamReader sReader = new StreamReader(xmlFilePath);

            // initialize an XML seriailizer object
            XmlSerializer deserializer = new XmlSerializer(typeof(Movies));

            using (sReader)
            {
                // deserialize the XML data set into a generic object
                object xmlObject = deserializer.Deserialize(sReader);

                // cast the generic object to the list class
                moviesObject = (Movies)xmlObject;
            }

            return moviesObject.movies;
        }

        public void Write(List<Movie> movies)
        {
            // initialize a FileStream object for reading
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamWriter sWriter = new StreamWriter(xmlFilePath, false);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (sWriter)
            {
                serializer.Serialize(sWriter, movies);
            }
        }

        public void Dispose()
        {
            // set resources to be cleaned up
        }
    }
}