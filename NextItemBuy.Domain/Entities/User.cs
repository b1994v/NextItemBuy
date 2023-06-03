using System;

namespace NextItemBuy.Domain
{
    public partial class User
    {
        public User()
        {

        }
        public User(string firstName, string lastName, string email, string userName, string password)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = userName;
            Password = password;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
