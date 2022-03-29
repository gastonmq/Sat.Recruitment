using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IMoneyService
    {
        decimal DefineMoney(User user);
    }
    public class MoneyService: IMoneyService
    {
        public decimal DefineMoney(User user)
        {
            decimal percentage = 0;
            if (user.UserType == "Normal")
            {
                percentage = NormalUser(user.Money);
            }
            else if (user.UserType == "SuperUser")
            {
                percentage = SuperUser(user.Money);
            }
            else if (user.UserType == "Premium")
            {
                percentage = PremiumUser(user.Money);
            }

            FormulaUser(user.Money, percentage);

            return user.Money;
        }


        public decimal NormalUser(decimal money)
        {
            if (money > 10 && money < 100)
            {
                return  Convert.ToDecimal(0.8);
            }
            else if (money > 100)
            {
                return Convert.ToDecimal(0.12);
            }
            return 0;
        }

        public decimal SuperUser(decimal money)
        {
            if (money > 100)
            {
                return Convert.ToDecimal(0.20);
            }

            return 0;
        }

        public decimal PremiumUser(decimal money)
        {
            if (money > 100)
            {
                Convert.ToDecimal(2);
            }

            return 0;
        }

        public decimal FormulaUser(decimal money, decimal percentage)
        {
            var gif = money * percentage;
            money = money + gif;
            return money;
        }

    }
}
