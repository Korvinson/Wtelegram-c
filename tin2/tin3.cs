using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;
using TL.Methods;
using WTelegram;

namespace tin2
{
    internal class tin3
    {
        static StreamWriter WTelegramLogs = new StreamWriter("WTelegram.log", true, Encoding.UTF8) { AutoFlush = true };
        
        static async Task Main(string[] _)
        {
            Helpers.Log = (lvl, str) => WTelegramLogs.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{"TDIWE!"[lvl]}] {str}");


            Client client = new Client(29507894, "003cb70923156975558c4ddd14423c65");
            await DoLogin("+79621337887"); // user's phone_number

            async Task DoLogin(string loginInfo)
            {
                while (client.User == null)
                    switch (await client.Login(loginInfo))
                    {
                        case "verification_code": Console.Write("Code: "); loginInfo = Console.ReadLine(); break;
                        case "name": loginInfo = "John Doe"; break;   
                        case "password": loginInfo = "secret!"; break; 
                        default: loginInfo = null; break;
                    }
                Console.WriteLine($"Успешно {client.User} (id {client.User.id})");
            }
            Console.WriteLine("Введите номер");
            var PhoneNumber = Console.ReadLine();

            Console.WriteLine("Сообщение");
            var message = Console.ReadLine();
            var name = "";
            var lastName = "";
            //await client.Contacts_AddContact();

            var takeout = await client.Account_InitTakeoutSession(contacts: true);
            //var finishTakeout = new Account_FinishTakeoutSession();
            //try
            //{
            //    var savedContacts = await client.InvokeWithTakeout(takeout.id, new Contacts_GetSaved());
            //    //foreach (SavedPhoneContact contact in savedContacts)
            //    //    Console.WriteLine($"{contact.first_name} {contact.last_name} {contact.phone}, added on {contact.date}");
            //    //finishTakeout.flags = Account_FinishTakeoutSession.Flags.success;
            //}
            //finally
            //{
            //    await client.InvokeWithTakeout(takeout.id, finishTakeout);
            //}
            var cell = client.Contacts_ResolvePhone(PhoneNumber);
            var contacts = await client.Contacts_ImportContacts(new[] { new InputPhoneContact { phone = PhoneNumber} });
            var test = await client.InvokeWithTakeout(takeout.id, new Contacts_AddContact());
            
            //client.Contacts_AddContact(test.Users.Add());

            //await client.Contacts_AddContact(contacts.imported[0].user_id, lastName, name, PhoneNumber);
            
                if (contacts.imported.Length > 0)
                await client.SendMessageAsync(contacts.users[contacts.imported[0].user_id], message);
            Console.ReadLine();
        }


    }
}
