using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    class FileBackup
    {

        public static void SaveToFile(string content)
        {
            if (content != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text-file | *.txt| XML-file | *.xml| Any-file | *.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, content);
                }
            }
            else
            {
                Console.WriteLine("No result to save...");
            }
           
        }

        public static string LoadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text-file (*.txt) | *.txt| Any-file (*.*) | *.*| XML-file (*.xml) | *.xml";
            
            string text = "";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
               text = File.ReadAllText(ofd.FileName);
            }
            
            return text; 
        }
    }
}
