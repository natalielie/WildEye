using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DB
{
    public interface IDbContextGenerator
    {
        IMyDbContext GenerateMyDbContext();
    }
}
