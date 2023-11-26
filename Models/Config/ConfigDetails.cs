using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial  class ConfigDetails
    {
        public MudPump MudPump1 { set; get; }=new MudPump();
        public MudPump MudPump2 { set; get; }= new MudPump();  
        public CementPump CementPump { set; get; }= new MudPump();
        public BOP BopModel { set; get; }=new BOP();
        public BopControlSystem BopControlSystemModel { set; get; } = new();
      
    }
}
