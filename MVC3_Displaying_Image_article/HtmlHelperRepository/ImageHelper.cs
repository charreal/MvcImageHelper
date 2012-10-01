using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Drawing;
using System.Web.Hosting;

namespace MVC3_Displaying_Image.HtmlHelperRepository
{
    /// <summary>
    /// Class Contains the Html Helper method for Rendering Image
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// The Extension method which makes call to Database and
        /// Get the Data from the table by querting to it. e
        /// It will read the byte array from table and concert into 
        /// Bitmap
        /// </summary>
        /// <param name="html"></param>
        /// <param name="ImageId"></param>
        /// <returns></returns>
        public static MvcHtmlString ImageData(this HtmlHelper helper, int imageId)
        {
             TagBuilder imageData = null; //To Build the Image Tag
             var imgUrl = new UrlHelper(helper.ViewContext.RequestContext);

            SqlConnection Conn = new SqlConnection("Data Source=.;Initial Catalog=Company;Integrated Security=SSPI");
            Conn.Open();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select [Image] From ImageTable where ImageId=@ImageId";
            Cmd.Parameters.AddWithValue("@ImageId", imageId);

            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    long imgData = Reader.GetBytes(0, 0, null, 0, int.MaxValue);
                    byte[] imageArray = new byte[imgData];
                    Reader.GetBytes(0, 0, imageArray, 0, Convert.ToInt32(imgData));
                    //Convert to Image
                    TypeConverter bmpConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap imageReceived = (Bitmap)bmpConverter.ConvertFrom(imageArray);

                    //Now Generate the Image Tag for Mvc Html String
                    imageReceived.Save(HostingEnvironment.MapPath("~/Images")+@"\I" + imageId.ToString() + ".jpg");
                    imageData = new TagBuilder("img");
                    //Set the Image Url for <img> tag as <img src="">
                    imageData.MergeAttribute("src", imgUrl.Content("~/Images") + @"/I" + imageId.ToString() + ".jpg");  
                    imageData.Attributes.Add("height", "50");
                    imageData.Attributes.Add("width", "50");

                }
            }
            Reader.Close(); 
            Conn.Close();
            Cmd.Dispose();
            Conn.Dispose();
            //The <img> tag will have auto closing as <img src="<Image Path>" height="50" width="50" />
            return MvcHtmlString.Create(imageData.ToString(TagRenderMode.SelfClosing));  
        }
    }
}