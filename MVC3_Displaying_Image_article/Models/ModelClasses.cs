using System;
using System.Data.SqlClient;
using System.Data;

namespace MVC3_Displaying_Image.Models
{
    public class ImageTable
    {
        public int ImadeId { get; set; }
        public byte[] Image { get; set; }
    }

    public class DataAcceess
    {
        public ImageTable[] GetImages()
        {
            ImageTable[] Images = null;
            SqlConnection Conn = new SqlConnection("Data Source=.;Initial Catalog=Company;Integrated Security=SSPI");
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("Select * From ImageTable", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(Reader);
            Images = new ImageTable[dt.Rows.Count];
            int i = 0;
            foreach (DataRow Dr in dt.Rows)
            {
                Images[i] = new ImageTable() 
                {
                     ImadeId = Convert.ToInt32(Dr["ImageId"]),
                      Image = (byte[])Dr["Image"]
                }; 
                i = i + 1;
            }
            Conn.Close(); 
            return Images;
        }
    }
}