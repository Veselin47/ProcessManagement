using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManagement
{
    public interface ISaveStrategy
    {
        void Save(Company company, string fileName);
    }
}
