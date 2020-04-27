using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zodii;

namespace Zodii.Services
{
    public class SignService : Sign.SignBase
    {
        public string replyAstroSign(string birthDate)
        {
            foreach (string i in Zodii.Program.infoFromFile)
            {
                string[] lineFromFile = i.Split(new Char[] { '/', '-', ' ' });
                string[] inputDate = birthDate.Split(new Char[] { '/' });
                if ((inputDate[0].Equals(lineFromFile[1]) && int.Parse(inputDate[1]) >= int.Parse(lineFromFile[2])) ||
                   (inputDate[0].Equals(lineFromFile[3]) && int.Parse(inputDate[1]) <= int.Parse(lineFromFile[4])))
                {
                    return lineFromFile[0];
                }
            }
            return " ";
        }
        public override Task<reply> CheckDate(request request, ServerCallContext context)
        {
            string reply = replyAstroSign(request.Date);
            return Task.FromResult(new reply
            {
                AstrologicalSign = reply
            });

        }
    }
}
