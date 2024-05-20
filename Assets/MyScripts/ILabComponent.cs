using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MyScripts
{
    public interface ILabComponent
    {
        
        string GreekName { get; set; }

        int Id { get; set; }
        List<Pin> Pins { get; set; }

        
    }
}
