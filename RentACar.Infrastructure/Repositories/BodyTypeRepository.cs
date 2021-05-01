using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class BodyTypeRepository : BaseRepository<BodyType>, IBodyTypeRepository
    {
        public BodyTypeRepository(RentACarContext context) : base(context) { }
    }
}
