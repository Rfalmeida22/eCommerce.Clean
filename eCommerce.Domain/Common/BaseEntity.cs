using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAtualizacao { get; protected set; }

        protected BaseEntity() { }

        protected BaseEntity(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero");

            Id = id;
            DataCriacao = DateTime.UtcNow;
        }
    }
}
