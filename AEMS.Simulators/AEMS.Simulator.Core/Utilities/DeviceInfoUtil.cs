using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Simulator.Core
{
    public class DeviceInfoUtil
    {
        public static void GetBatteryLevel()
        {
            System.Management.ObjectQuery query = new("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new(query);

            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
            {
                foreach (PropertyData property in mo.Properties)
                {
                    Console.WriteLine("Property {0}: Value is {1}", property.Name, property.Value);
                }
            }
        }
    }
}
