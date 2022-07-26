using System;
using System.Data.SqlClient;

namespace ClinicAdoDl
{

    public class ClinicAdo
    { 
         public static SqlConnection con;
         public static SqlCommand cmd;
        //Sql connection method
        private static SqlConnection getcon()
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source= (localdb)\\MSSQLLocaldb; Initial Catalog=CMS; Integrated Security=true ");
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return con;
        }

        //To Login in Database
        public SqlDataReader? account_user_login(string uname,string passwd)
        {
           
            try
            {
                con = getcon(); //To Open The onnection
                cmd = new SqlCommand("select * from UserAccounts where username=@uname and pwd=@passwd");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@passwd", passwd);
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                
            }
            return null;
        }

        //To fetch data from doctors table and show details
        public SqlDataReader show_all_doctors_available()
        {
            con = getcon(); //To Open The connection
            cmd = new SqlCommand("select * from Doctors");
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;

        }
        // To add Patient details in Patient Table 
        public int add_patients_details(string fname, string lname, string sex, int age, string dob)
        {
            try
            {
                con = getcon();
                cmd = new SqlCommand("insert_patients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstname ", fname);
                cmd.Parameters.AddWithValue("@lastname", lname);
                cmd.Parameters.AddWithValue("@sex", sex);
                cmd.Parameters.AddWithValue("age", age);
                cmd.Parameters.AddWithValue("@dob", dob);
                int temp_query_executed = cmd.ExecuteNonQuery();
                return temp_query_executed;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        //To Read the Data of the New  Inserted Patient
        public SqlDataReader latest_inserted_Patient_record(string fname, string lname, string sex, int age, string dob)
        {
            con = getcon(); //To Open The onnection
            cmd = new SqlCommand("select * from Patients where firstname=@fname and lastname=@lname and sex=@sex and age=@age");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@age", age);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //To check whether Patient id is availale to book the appointment
        public SqlDataReader check_patient_id_for_appoitnment(int pid)
        {
            con = getcon();
            cmd = new SqlCommand("Select * from patients where patientId=@pid",con);
            cmd.Parameters.AddWithValue("@pid", pid);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;


        }

        //To show Doctors based on Specialization
        public SqlDataReader doctor_available_specialization(string spclization)
        {
            con = getcon();
            cmd = new SqlCommand("select * from Doctors where specialization=@spclization",con);
            cmd.Parameters.AddWithValue("@spclization", spclization);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //To register the appointments
        public int register_appointments(int patientid, string dfname, string dlname, int docid, string visitdate,int aptime)
        {
            con = getcon();
            cmd = new SqlCommand("record_appoitment", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@patientid", patientid);
            cmd.Parameters.AddWithValue("@doctorfname", dfname);
            cmd.Parameters.AddWithValue("@doctorlname", dlname);
            cmd.Parameters.AddWithValue("@doctorid", docid);
            cmd.Parameters.AddWithValue("@visitdate", visitdate);
            cmd.Parameters.AddWithValue("@apttime", aptime);
            int temp_query_executed = cmd.ExecuteNonQuery();
            return temp_query_executed;


        }

        //To Check whether slots are available or not
        public int chck_slot_available(int dcid)
        {
            con = getcon(); //To Open The onnection
            cmd = new SqlCommand("select slotavailabel from Doctors where DoctorId=@dcid", con);
            cmd.Parameters.AddWithValue("@dcid", dcid);
            SqlDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                for (int i = 0; i < result.FieldCount; i++)
                {
                    return ((int)result[i]);
                }

            }
            return 0;
        }

        // to update the doctors availability slot after the appointment booking 
        public void doctor_slot_update(int dcid, int slots)
        {
            con = getcon(); //To Open The onnection
            cmd = new SqlCommand("Update Doctors Set slotavailabel=@slots where  DoctorId=@dcid", con);
            cmd.Parameters.AddWithValue("@slots", slots);
            cmd.Parameters.AddWithValue("@dcid", dcid);
            cmd.ExecuteNonQuery();
        }

        // To Extract Doctors firstname based on Doctors id
        public string extract_doc_fname(int dcid)
        {
            con = getcon(); //To Open The onnection
            cmd = new SqlCommand("ext_lname", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dcid", dcid);
            SqlDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                for (int i = 0; i < result.FieldCount; i++)
                {
                    return ((string)result[i]);
                }

            }
            return "";
        }
        public string extract_doc_lname(int dcid)
        {
            con = getcon(); //To Open The onnection
            cmd = new SqlCommand("select lastname from Doctors where DoctorId=@dcid", con);
            cmd.Parameters.AddWithValue("@dcid", dcid);
            SqlDataReader result1 = cmd.ExecuteReader();
            while (result1.Read())
            {
                for (int i = 0; i < result1.FieldCount; i++)
                {
                    return ((string)result1[i]);
                }

            }
            return "";
        }

        //To Show the Appointment details date wise
        public SqlDataReader show_appointments_datewise(int pid, string date_of_visit)
        {
            con = getcon();
            cmd = new SqlCommand("select * from Appoitment where PatientId=@pid and visitdate=@date_of_visit", con);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@date_of_visit", date_of_visit);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //To delete the appointment
        public int delete_appointment(int did)
        {
            con = getcon();
            cmd = new SqlCommand("delete from Appoitment where doctorid=@did", con);
            cmd.Parameters.AddWithValue("@did", did);
            int i = cmd.ExecuteNonQuery();
            return i;
        }
    }
}
