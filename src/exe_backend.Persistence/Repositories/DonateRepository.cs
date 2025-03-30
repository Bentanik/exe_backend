using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exe_backend.Persistence.Repositories;

public class DonateRepository(ApplicationDbContext context) : RepositoryBase<Donate, Guid>(context), IDonateRepository
{
}
