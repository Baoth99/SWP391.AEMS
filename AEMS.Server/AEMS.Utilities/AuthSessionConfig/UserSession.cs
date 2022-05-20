using System;

namespace AEMS.Utilities
{
    public class UserSession
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Token { get; set; }
    }
}
