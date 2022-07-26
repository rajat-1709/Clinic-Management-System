using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicAdoDl;



namespace ClinicClientTest
{
    [TestClass]
    public class ClinicUnitTest
    {
        [TestMethod]
        public void check_account_login()
        {
            //Arrange
            ClinicAdo cd_test = new ClinicAdo();
            string uname = "ninja20";
            string pswd = "admin";


            //Action
            var res=cd_test.account_user_login(uname,pswd);

            //Assert
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void add_patient_test()
        {
            //Arrange
            ClinicAdo cd_test = new ClinicAdo();
            string fname = "Reema";
            string lname = "sahu";
            string sex = "Female";
            string dob = "2/03/2000";
            int age = 28;
            //Action
            var i = cd_test.add_patients_details(fname, lname, sex, age, dob);

            //Assert
            Assert.AreEqual(1, i);
        }
    }
}
