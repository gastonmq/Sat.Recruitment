using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services
{
    public interface IUserService
    {
        Result CreateUser(User user);
        List<User> GetUsers();
    }
    public class UserService: IUserService
    {
        IFileService _fileService;
        IMoneyService _moneyService;
        public UserService(IFileService fileService, IMoneyService moneyService)
        {
            _fileService = fileService;
            _moneyService = moneyService;
        }
        public Result CreateUser(User user)
        {
            try
            {
                user.Money = _moneyService.DefineMoney(user);
                bool isDuplicated = ValidateDuplicates(user);
                string message = isDuplicated ? "The user is duplicated." : _fileService.AppendUserToTxt(user);

                Result result = new Result()
                {
                    IsSuccess = !isDuplicated,
                    Message = message
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateDuplicates(User newUser)
        {
            try
            {
                List<User> _users = GetUsers();
                bool isDuplicated = false;
                foreach (var user in _users)
                {
                    if (user.Email == newUser.Email || user.Phone == newUser.Phone || (user.Name == newUser.Name && user.Address == newUser.Address))
                    {
                        isDuplicated = true;
                    }
                }

                return isDuplicated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        public List<User> GetUsers()
        {
            try
            {
                var reader = _fileService.ReadUsersFromFile();
                List<User> _users = new List<User>();
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _users.Add(user);
                }
                reader.Close();
                return _users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
