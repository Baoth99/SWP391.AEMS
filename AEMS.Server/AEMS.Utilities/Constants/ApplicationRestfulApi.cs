﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Utilities
{
    public class ApplicationRestfulApi
    {
        public const string BaseApiUrl = "/api/v{ver:apiVersion}/";

        public const string ApplicationProduce = "application/json";

        public const string UrlencodedContentType = "application/x-www-form-urlencoded";

        public const string ApplicationContentType = "application/json; charset-utf-8";
    }
}
