using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public interface IMyEntityManager
    {
        EcoClean.Models.Enterprise.Enterprise GetMyEntity();
        bool SaveMyEntity(EcoClean.Models.Enterprise.Enterprise myEntity);
    }
}
