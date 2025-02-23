using System;
using System.Security.Cryptography;
using System.Text;
using eCommerce.Domain.Common;

namespace eCommerce.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa uma Senha.
    /// </summary>
    public class Senha : ValueObject
    {
        /// <summary>
        /// Valor da senha hash.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Construtor privado para inicializar uma nova instância da classe Senha.
        /// </summary>
        /// <param name="value">Valor da senha hash.</param>
        private Senha(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Cria uma nova instância de Senha.
        /// </summary>
        /// <param name="senha">Senha em texto plano.</param>
        /// <returns>Instância de Senha com o valor hash.</returns>
        /// <exception cref="ArgumentException">Lançada quando a senha é nula ou vazia.</exception>
        public static Senha Create(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha é obrigatória");

            return new Senha(HashSenha(senha));
        }

        /// <summary>
        /// Gera o hash da senha utilizando SHA256.
        /// </summary>
        /// <param name="senha">Senha em texto plano.</param>
        /// <returns>Hash da senha em formato Base64.</returns>
        private static string HashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Obtém os componentes que determinam a igualdade do Value Object.
        /// </summary>
        /// <returns>Coleção de objetos que compõem a identidade do Value Object.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Retorna uma string que representa o Value Object.
        /// </summary>
        /// <returns>String que representa o Value Object.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
