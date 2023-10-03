using BookingManagementApp.Models;

namespace API.DTOs.Accounts
{
    public class CreateAccountDto
    {
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredTime { get; set; }


        //membuat implicit operator untuk create
        public static implicit operator Account(CreateAccountDto createAccountDto)
        {
            return new Account
            {
                OTP = createAccountDto.Otp,
                IsUsed = createAccountDto.IsUsed,
                Password = createAccountDto.Password,
                ExpiredTime = createAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
