using System;
using System.Globalization;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Zodii;

namespace Client
{
    class Program
    {
        static bool Validate(string birthDate)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTimeObj;
            bool isSuccess = DateTime.TryParseExact(birthDate, "MM/dd/yyyy", provider, DateTimeStyles.None, out dateTimeObj);
            if (isSuccess)
                return true;
            return false;
        }
        static async Task Main(string[] args)
        {
            bool isValid = false;
            string birthDate;
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Sign.SignClient(channel);

            birthDate = Console.ReadLine();

            while (!isValid)
                if (Validate(birthDate))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please write a valid birth date");
                    birthDate = Console.ReadLine();
                }


            var clientBirthDate = new request { Date = birthDate };
            var reply = await client.CheckDateAsync(clientBirthDate);
            Console.WriteLine(reply.AstrologicalSign);
            Console.ReadLine();
        }
    }
}