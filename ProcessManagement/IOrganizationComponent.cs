using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public interface IOrganizationComponent
    {
        string Name { get; set; }
        void Display(int depth); 
    }
}
