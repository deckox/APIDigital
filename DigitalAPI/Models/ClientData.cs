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

    //public class ClientReturn
    //{
    //    public DateTime RegistrationDate { get; set; }
    //    public long Token { get; set; }
    //    public int CardId { get; set; }

    //    public ClientReturn(ClientData clientData)
    //    {
    //        Token = CircularArray(clientData);
    //        RegistrationDate = clientData.RegistrationDate;
    //        CardId = clientData.CardId;
    //    }

    //    public int CircularArray(ClientData clientData)
    //    {
    //        var cardNumber = clientData.CardNumber.ToString();
    //        var lastFourNumbers = cardNumber.Substring(cardNumber.Length - 4);
    //        char[] chars = lastFourNumbers.ToCharArray();
    //        int[] numbers = chars.Select(_ => int.Parse(_.ToString())).ToArray();

    //        var token = int.Parse(string.Join("", ArrayRotate(clientData.CVV, numbers)));
    //        return token;
    //    }

    //    public int[] ArrayRotate(int rotation, int[] arrayEntry)
    //    {
    //        var arrayExit = new int[arrayEntry.Length];
    //        var realRotate = rotation % arrayEntry.Length;

    //        for (int i = 0; i < arrayEntry.Length; i++)
    //        {
    //            int indexDest;
    //            if (i + realRotate >= arrayEntry.Length)
    //            {
    //                indexDest = (i + realRotate) - arrayEntry.Length;
    //            }
    //            else
    //            {
    //                indexDest = i + realRotate;
    //            }

    //            arrayExit[indexDest] = arrayEntry[i];
    //        }

    //        return arrayExit;

    //    }

    //}
}
