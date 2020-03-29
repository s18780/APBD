using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string csvpath = Console.ReadLine();  
                string xmlpath = Console.ReadLine(); 
                string format = Console.ReadLine();  //xml
                if (csvpath is null)
                {
                    csvpath = "dane.csv";
                }
                else if (xmlpath is null)
                {
                    xmlpath = "result.xml";
                }
                else if (format is null)
                {
                    format = "xml";
                }
                Console.WriteLine(csvpath);
                string[] source = File.ReadAllLines(csvpath);
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),

                new XElement("Root",
                from str in source
                let fields = str.Split(',')
                select new XElement("Studenci",
                    new XAttribute("student_indexNumber", "s" + fields[4]),
                    new XElement("fname", fields[0]),
                    new XElement("lname", fields[1]),
                    new XElement("birthdate", fields[5]),
                    new XElement("email", fields[6]),//poprawic mail-dodac imie i nazw
                    new XElement("mothersName", fields[7]),
                    new XElement("fathersName", fields[8]),
                    new XElement("studies",
                        new XElement("name", fields[2]),
                        new XElement("mode", fields[3]))

                   ))
                );
                xml.Save("wynik.xml");


            }
            catch (FileNotFoundException e)
            {
                string filename = "log.txt";

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {

                        w.Write("Plik nazwa nie istnieje");
                    }
                }
            }
            catch (ArgumentException ex) {
                string filename = "log.txt";

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {

                        w.Write("Podana ścieżka jest niepoprawna");
                    }
                }
            }
        }
    }
}
