namespace eCommerce.Domain.Events
{
    public class Varejista : Event
    {
        public int VarejistaId { get; }
        public string Nome { get; }
        public string Cnpj { get; }

        public Varejista(int varejistaId, string nome, string cnpj, string userName) 
            : base(userName)
        {
            VarejistaId = varejistaId;
            Nome = nome;
            Cnpj = cnpj;
        }
    }
} 