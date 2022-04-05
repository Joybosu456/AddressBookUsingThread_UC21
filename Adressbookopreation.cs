using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookThread
{
    public class Adressbookopreation
    {
        List<AdBook> listperson = new List<AdBook>();

        public void addressRoll(List<AdBook> ad)
        {
            ad.ForEach(addressData =>
            {
                Console.WriteLine("Employee being added:" + addressData.empName);
                this.addressRoll(addressData);
                Console.WriteLine("Employee added: " + addressData.empName);
            });
            Console.WriteLine(this.listperson.ToString());
        }
        private void addressRoll(AdBook address)
        {
            listperson.Add(address);

        }




        public void addressRollThread(List<AdBook> ad)
        {
            ad.ForEach(addressData =>
            {

                Task tread = new Task(() =>
                {

                    Console.WriteLine("Employee being added:" + addressData.empName);
                    this.addressRoll(addressData);
                    Console.WriteLine("Employee added: " + addressData.empName);
                });
                tread.Start();



            });
            Console.WriteLine(this.listperson.ToString());
        }

    }
}