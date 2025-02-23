namespace eCommerce.Domain.Events
{
    public class Loja : Event
    {
        public int LojaId { get; }
        public string Nome { get; }
        public string Cnpj { get; }
        public int VarejistaId { get; }

        public Loja(int lojaId, string nome, string cnpj, int varejistaId, string userName) 
            : base(userName)
        {
            LojaId = lojaId;
            Nome = nome;
            Cnpj = cnpj;
            VarejistaId = varejistaId;
        }
    }
} 