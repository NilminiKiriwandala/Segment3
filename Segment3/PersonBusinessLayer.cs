using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Segment3
{
   public class PersonBusinessLayer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName{ get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }



        public PersonBusinessLayer()
        {
            ID = -1;
            LastName = string.Empty;
            FirstName = string.Empty;
            Gender = string.Empty;
            DOB = DateTime.MinValue;
        }

        public static bool Add(string lstNm, string firtNm, string gndr, DateTime dob)
        {
            try
            {
                bool result = false;
                PersonBusinessLayer person = new PersonBusinessLayer();
                person.ID = -1;
                person.LastName = lstNm;
                person.FirstName = firtNm;
                person.Gender = gndr;
                person.DOB = dob;
                result = PersonDataLayer.AddPerson(person);
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static PersonBusinessLayer GetPerson(int id) 
        {
            PersonBusinessLayer findPerson = PersonDataLayer.FindPerson(id);
           return findPerson;
        
        }


        public static bool Update(int id,string lstNm, string firtNm, string gndr, DateTime dob)
        {
            try
            {
                bool result = false;
                PersonBusinessLayer person = new PersonBusinessLayer();
                person.ID = id;
                person.LastName = lstNm;
                person.FirstName = firtNm;
                person.Gender = gndr;
                person.DOB = dob;
                result = PersonDataLayer.UpdatePerson(person);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void  FillDataSet(ref DataSet ds, bool SearchMaleOnly) 
        {
            PersonDataLayer.FillDataset(ref ds,SearchMaleOnly);
        }

    }
}
