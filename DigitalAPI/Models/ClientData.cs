using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalAPI.Models
{
    public class ClientData
    {
        public int CardId { get; set; }
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long Token { get; set; }

        public ClientData()
        {
            RegistrationDate = DateTime.UtcNow;
        }
    }

    public class ClientReturn
    {
        public DateTime RegistrationDate { get; set; }
        public long Token { get; set; }
        public int CardId { get; set; }

        public ClientReturn(ClientData clientData)
        {
            Token = CircularArray(clientData);
            RegistrationDate = clientData.RegistrationDate;
            CardId = clientData.CardId;
        }

        public int CircularArray(ClientData clientData)
        {
            int token = 0;
            var tokenNumber = new List<int>();
            var cardNumber = clientData.CardNumber.ToString();
            var lastFourNumbers = cardNumber.Substring(cardNumber.Length - 4);
            char[] chars = lastFourNumbers.ToCharArray();
            int[] numbers = chars.Select(_ => int.Parse(_.ToString())).ToArray();
            tokenNumber.Add(numbers[0]);
            tokenNumber.Add(numbers[1]);
            tokenNumber.Add(numbers[2]);
            tokenNumber.Add(numbers[3]);

            for (int i = 0; i < clientData.CVV; i++)
            {
                var aux = tokenNumber.Last();
                tokenNumber.RemoveAt(tokenNumber.Count - 1);
                tokenNumber.Insert(0, aux);

              /*  [1,2,3,4]
                [4,1,2,3]
                [3,4,1,2]
                [2,3,4,1]
                [1,2,3,4] */

                // cvv % numero de array  999 / 4 

                if (i == clientData.CVV - 1)
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
