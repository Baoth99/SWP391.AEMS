﻿using System;

namespace AEMS.MSAzureService
{
    public class MSUserProfile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }
    }
}
