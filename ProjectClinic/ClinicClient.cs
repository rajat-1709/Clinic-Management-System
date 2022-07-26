using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ClinicAdoDl;
using ClinicBusinessLogicDl;


namespace ProjectClinic
{
    class ClinicClient
    {
        #region
        //HASHING
        //    public static string ComputeSha256Hash(string rawData)
        //    {
        //        // Create a SHA256   
        //        using (var sha2 = System.Security.Cryptography.SHA256.Create())
        //        {
        //            var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(rawData));
        //            {
        //                string hexString = string.Empty;

        //                for (int i = 0; i < hash.Length; i++)
        //                {
        //                    hexString += hash[i].ToString("X2");
        //                }
        //                return (hexString);
        //            }
        //        }
        //    }
        #endregion
        static void Main(string[] args)
        {
            //this var is used to run continuosly program in console
            int code_initiate = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------------------------------Welcome to Clinical Management System--------------------------------------------");
            Console.ResetColor();
            do
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nWelcome to ES Clinic Management Please Enter Your Credentials To Login\n");
                Console.ResetColor();
                Console.Write("Please Enter your username:");
                string uname = Console.ReadLine();
                //Masking of Password is Not working due to taking of one extra space due to which password is not matching in db , this can be done in frontend page easily 
                Console.Write("Please Enter your Password:");
                string passwrd = null;
                //Logic to hide Password while writing in console
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    passwrd += key.KeyChar;
                    Console.Write("*");
                }
                #region
                //Hashing
                //Byte[] inputBytes = Encoding.UTF8.GetBytes(passwrd);
                //SHA512 shaM = new SHA512Managed();
                //Byte[] hashedBytes = shaM.ComputeHash(inputBytes);
                //string hashedpassword = ComputeSha256Hash(passwrd);
                #endregion
                Console.WriteLine();
                ClinicBusinessLogic buslogic = new ClinicBusinessLogic();
                //Method to check Username and password in the database
                var check_login = buslogic.account_login(uname, passwrd);
                try
                {
                    if (check_login!=null  && check_login.Read() )
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogin Succesfull!  Welcome \n ");
                        Console.ResetColor();
                        int n = 1;
                        while (n == 1)
                        {
                            char agn;
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("\nPress 1: View Doctor\nPress 2: Add Patient\nPress 3: Schedule Appointment \nPress 4: Cancel Appointment \nPress 5: Logout");
                                int check_logout = Convert.ToInt32(Console.ReadLine());
                                Console.ResetColor();
                                switch (check_logout)

                                {
                                    case 1:
                                        Console.WriteLine("\nDoctors Availabel are as follows:\n");
                                        var all_doctors_availabel = buslogic.ShowAllDoctors();
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        while (all_doctors_availabel.Read())
                                        {
                                            for (int i = 0; i < all_doctors_availabel.FieldCount; i++)
                                            {
                                                Console.Write(all_doctors_availabel[i] + " ");
                                            }
                                            Console.WriteLine();
                                        }
                                        Console.ResetColor();

                                        break;
                                    case 2:
                                        Console.WriteLine("Please Enter the Patient details");
                                        Console.WriteLine("Enter Patient's First Name:");
                                        string fname = Console.ReadLine();
                                        Console.WriteLine("Enter Patients Last Name:");
                                        string lname = Console.ReadLine();
                                        Console.WriteLine("Choose Patients Sex :\nPress 1 :Male\nPress 2: Female\nPress 3: Others");
                                        string sex;
                                        int gender_option = Convert.ToInt32(Console.ReadLine());
                                        if (gender_option == 1)
                                        {
                                            sex = "Male";
                                        }
                                        else if (gender_option == 2)
                                        {
                                            sex = "Female";
                                        }
                                        else
                                        {
                                            sex = "Others";
                                        }
                                        Console.WriteLine("Please Enter your Age");
                                        int age = Convert.ToInt32(Console.ReadLine());
                                        if (age > 1 && age < 120)
                                        {

                                            Console.WriteLine("Enter Patients DOb in DD/MM/YYYY – Indian format");
                                            string dob = Console.ReadLine();

                                            int patient_details_tempchecker = buslogic.AddPatients(fname, lname, sex, age, dob);
                                            if (patient_details_tempchecker == 1)
                                            {
                                                Console.WriteLine("\nThank You ! Patient Details Succefully Recorded and saved\n\n");
                                                var latest_patient_details = buslogic.get_latest_patientid(fname, lname, sex, age, dob);
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                Console.WriteLine("Patient   id   FirstName    LastName   Sex   Age   Dob");
                                                Console.ResetColor();
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                while (latest_patient_details.Read())
                                                {
                                                    for (int i = 0; i < latest_patient_details.FieldCount; i++)
                                                    {
                                                        Console.Write(latest_patient_details[i] + "      ");
                                                    }
                                                    Console.WriteLine();
                                                }
                                                Console.ResetColor();

                                            }
                                            else
                                            {
                                                Console.WriteLine("Some Error Occurred");
                                            }
                                        }
                                        else
                                        {
                                            Console.BackgroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Please Enter the Age less than 120");
                                            Console.ResetColor();
                                        }
                                        break;

                                    case 3:
                                        Console.WriteLine("Appointment Scheduling.....");
                                        Console.WriteLine("Please Enter Your patient Id ");
                                        int pid = Convert.ToInt32(Console.ReadLine());
                                        var check_pd_id = buslogic.Check_patientid(pid);
                                        //Each appoitment is 1 hour long
                                        int apttime = 1;
                                        if (check_pd_id.Read())
                                        {
                                            Console.WriteLine("Please Enter Doctors Specialization  : General, Internal Medicine, Pediatrics, Orthopedics, Ophthalmology");
                                            string spclization = Console.ReadLine();
                                            var dc_availabels = buslogic.doctor_based_on_specialization(spclization);

                                            while (dc_availabels.Read())
                                            {
                                                for (int i = 0; i < dc_availabels.FieldCount; i++)
                                                {
                                                    Console.Write(dc_availabels[i] + "      ");

                                                }
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Please Enter the Doctor id for which  You want to register Appoitnment");
                                            int dcid = Convert.ToInt32(Console.ReadLine());
                                            var to_check_slot_availabel = buslogic.check_slots(dcid);

                                            if ((to_check_slot_availabel - apttime) == 0 || (to_check_slot_availabel - apttime) >= 1)
                                            {
                                                Console.WriteLine("Slots Are Available with the Doctor ,Please Enter Deatils to book the slot");
                                                string doctorfname = buslogic.extract_doc_fname(dcid);

                                                Console.WriteLine("Enter the Date of Visit in Dd/mm/yyyy for Appointment");
                                                string visitdate = Console.ReadLine();
                                                string doctorlname = buslogic.Doc_lname(dcid);
                                                buslogic.Addappointments(pid, doctorfname, doctorlname, dcid, visitdate, apttime);
                                                buslogic.update_doctors_slot(dcid, to_check_slot_availabel - apttime);
                                                Console.WriteLine("Thank you Your Appoitment is successfully booked");


                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry No slots Available");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please Register yourself as patients no records found");
                                        }

                                        break;
                                    case 4:
                                        Console.WriteLine("Cancel Appointment");
                                        Console.WriteLine("Please Enter the Patient id");
                                        int pid_cancel = Convert.ToInt32(Console.ReadLine());
                                        var check_pd_id_cancel = buslogic.Check_patientid(pid_cancel);
                                        if (check_pd_id_cancel.Read())
                                        {
                                            Console.WriteLine("Please Enter your Date of Appointment in mm/dd/yyyy formatt ");
                                            string cancel_visitdate = Console.ReadLine();
                                            Console.WriteLine(cancel_visitdate);
                                            var show_apntms = buslogic.show_appointments_of_specific_date(pid_cancel, cancel_visitdate);
                                            while (show_apntms.Read())
                                            {
                                                for (int i = 0; i < show_apntms.FieldCount; i++)
                                                {
                                                    Console.Write(show_apntms[i] + "      ");
                                                }
                                                Console.WriteLine();
                                            }
                                            Console.WriteLine("Ente the Doctor id To cancel the Appointment of Doctor");
                                            int d_cn_id = Convert.ToInt32(Console.ReadLine());
                                            var chc_delete_temp = buslogic.delete_apt(d_cn_id);
                                            var slots_add = buslogic.check_slots(d_cn_id);
                                            buslogic.update_doctors_slot(d_cn_id, slots_add + 1);
                                            if (chc_delete_temp > 0)
                                            {
                                                Console.WriteLine("Thank You ! for Visiting Your Slot has been Cancelled");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please Register yourself as patients no records found");
                                        }
                                        break;
                                    case 5:
                                        n = 2;
                                        break;
                                    default:
                                        Console.WriteLine("Please Choose Correct Option");
                                        break;

                                }
                                Console.WriteLine("\nDo You want to continue to Your Account Press y otherwise to Logout press n y/n");
                                agn = Convert.ToChar(Console.ReadLine());
                            } while (agn == 'y');
                        }
                    }
                
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLogin failed, Please Try Again");
                    Console.ResetColor();
                }
            }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
               

            } 
            while (code_initiate == 1);

        }
    }
}
