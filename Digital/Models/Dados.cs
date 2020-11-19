using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Digital.Models
{
    [Table("Dados")]
    public class Dados
    {
        [Key]

        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public int CardId { get; set; }
    }

    public class DadosRetorno
    {
        public long Token { get; set; }
        public DateTime Registrationdate { get; set; }

        public DadosRetorno(Dados dados)
        {
            Token = CircularArray(dados);
            Registrationdate = DateTime.UtcNow;
        }

        public int CircularArray(Dados dados)
        {
            int token = 0;
            var tokenNumber = new List<int>();
            var cardNumber = dados.CardNumber.ToString();
            var lastFourNumbers =  cardNumber.Substring(cardNumber.Length - 4);
            char[] chars = lastFourNumbers.ToCharArray();
            int[] numbers = chars.Select(_ => int.Parse(_.ToString())).ToArray();
            tokenNumber.Add(numbers[0]);
            tokenNumber.Add(numbers[1]);
            tokenNumber.Add(numbers[2]);
            tokenNumber.Add(numbers[3]);

            for (int i = 0; i < dados.CVV; i++)
            {
                var aux = tokenNumber.Last();
                tokenNumber.RemoveAt(tokenNumber.Count - 1);
                tokenNumber.Insert(0, aux);

                if (i == dados.CVV - 1)
                {
                    var concatenateToken = tokenNumber[0].ToString();
                    concatenateToken = concatenateToken + tokenNumber[1].ToString();
                    concatenateToken = concatenateToken + tokenNumber[2].ToString();
                    concatenateToken = concatenateToken + tokenNumber[3].ToString();
                    token = int.Parse(concatenateToken);
                }
            }

             

            return token;
        }
    }
}
