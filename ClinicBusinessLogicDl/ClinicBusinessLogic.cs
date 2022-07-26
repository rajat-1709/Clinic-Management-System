using System;
using ClinicAdoDl;
using System.Data.SqlClient;

namespace ClinicBusinessLogicDl
{
    public class ClinicBusinessLogic
    {
        readonly ClinicAdo cda = new();

        //method which connects with ado layer and get's account login verified
        public SqlDataReader account_login(string uname,string pwd)
        {

            var result = cda.account_user_login(uname, pwd);
            return result;

        }
        //To get all doctors list from  database
        public SqlDataReader ShowAllDoctors()
        {
            var all_doctors_list = cda.show_all_doctors_available();
            return all_doctors_list;
        }
        //to add patients in patients table in database
        public int AddPatients(string fname , string lname, string sex, int age , string dob)
        {
           var res= cda.add_patients_details(fname, lname, sex, age, dob);
           return res;
        }
        //to display latest patient details
        public SqlDataReader get_latest_patientid(string fname, string lname, string sex, int age, string dob)
        {
            var latest_pt_details = cda.latest_inserted_Patient_record(fname, lname, sex, age, dob);
            return latest_pt_details;
        }
        //to check patients on the basis of patient id whether it's available or not
        public SqlDataReader Check_patientid(int pid)
        {
            var temp_patient_id_check = cda.check_patient_id_for_appoitnment(pid);
            return temp_patient_id_check;
        }
        //to find doctor's based on their specialization
        public SqlDataReader doctor_based_on_specialization(string spclization)
        {
            var doctors_availabel = cda.doctor_available_specialization(spclization);
            return doctors_availabel;
        }
        //to add appointments with particular doctor in appointment table
        public int Addappointments(int patientid, string doctorfnames, string doctorlnames, int docid, string visitdate, int aptime)
        {
            var res = cda.register_appointments(patientid, doctorfnames, doctorlnames, docid, visitdate, aptime);
            return res;
        }
        //To check whether the slots are available with particulr doctor or not
        public int check_slots(int dcid)
        {
            var res = cda.chck_slot_available(dcid);
            return res;
        }


        //To update the doctor's slot after slot booking
        public void update_doctors_slot(int dcid,int slot)
        {
            cda.doctor_slot_update(dcid, slot);
        }

        //To get the doctor's firstname on basis of doctor's id
        public string extract_doc_fname(int dcid)
        {
            var res = cda.extract_doc_fname(dcid);
            return res;
        }
        public string  Doc_lname(int dcid)
        {
            var result = cda.extract_doc_lname(dcid);
            return result;
        }

        //to show the booked appointment on a specific date
        public SqlDataReader show_appointments_of_specific_date(int pid,string date_of_visit)
        {
            var result = cda.show_appointments_datewise(pid, date_of_visit);
            return result;
        }
        //To delete the appointment from appointments table
        public int delete_apt(int did)
        {
            var res = cda.delete_appointment(did);
            return res;
        }
    }
}
