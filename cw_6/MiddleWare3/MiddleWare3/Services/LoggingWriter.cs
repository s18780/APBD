using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWare3.Services
{
    public class LoggingWriter : IOService
    {
        private readonly static string path = "Loginng.txt";
        public void Write(string loginng)
        {
           
            using (var writer = new StreamWriter(path, true))
            {


                writer.WriteLine(loginng);
            }
        }
    }
}
