//Extension to DateTime Class
using System;

namespace API.Extensions
{

    //Function to get someones Age from date of Birth
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            //check if date of birth is bigger than the current year - age years
            if(dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}