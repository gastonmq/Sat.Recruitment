using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    public interface IFileService
    {
        StreamReader ReadUsersFromFile();
        string AppendUserToTxt(User user);
    }
    public class FileService: IFileService
    {
        public StreamReader ReadUsersFromFile()
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

                FileStream fileStream = new FileStream(path, FileMode.Open);

                StreamReader reader = new StreamReader(fileStream);

                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AppendUserToTxt(User user)
        {
            try
            {
                string money = user.Money.ToString().Replace(",", ".");
                using (var sw = File.AppendText(Directory.GetCurrentDirectory() + "/Files/Users.txt"))
                {
                    sw.Write(Environment.NewLine + $"{user.Name},{user.Email},{user.Address},{user.Phone},{user.UserType},{money}");
                }

                string message = "User Created.";

                return message;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
