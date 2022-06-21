using System;
using System.Collections.Generic;


namespace AEMS.Application
{
    public class AreaListQuery : IQuery<IEnumerable<AreaViewModel>>
    {
    }

    public class AreaViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
