using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.AWSService
{
    public interface IAWSIotCoreService
    {
        Task PublishMQTTMessage<T>(string topic, T model);


        Task AddNewThing(CreateThingModel model);
    }
}
