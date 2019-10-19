using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSqlServerToSqlLite
{
    public interface ISqLiteMigration
    {
        void DoMigration();
        void Misc();
    }
}
