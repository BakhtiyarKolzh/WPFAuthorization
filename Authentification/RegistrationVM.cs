using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace WPFAuthorization
{

    internal class RegistrationVM : ObservableValidator
    {

        #region Properties

        private string firstName;

        [StringLength(15, MinimumLength = 3, ErrorMessage = "FirstName must contain at least 3 characters")]
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "FirstName entered incorrectly")]
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value, true);
        }


        private string lastName;

        [StringLength(15, MinimumLength = 3, ErrorMessage = "LastName must contain at least 3 characters")]
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "LastName entered incorrectly")]
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value, true);
        }

        private string email;

        [StringLength(30, MinimumLength = 5, ErrorMessage = "Email must contain at least 5 characters")]
        [RegularExpression(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$",
            ErrorMessage = "Email entered incorrectly")]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        #endregion


        private List<ValidationResult> errors = new List<ValidationResult>();


        public RegistrationVM()
        {
            ErrorsChanged += Suspect_ErrorsChanged;
        }


        private void Suspect_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }


        int counts = 0;

        //public string Errors => string.Join(Environment.NewLine, from ValidationResult e in errors select e.ErrorMessage);
        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);


        public void RegistrationCommandHandler()
        {
            ValidateAllProperties();
            Debug.Print($"ValidateErrors counts {Errors.Length}");
            if (Errors.Length == counts)
            {
                Debug.Print("IsValidResult!");
                var mail = new MailManager();
                string serialNumber = HardDriveInfo.GetMainHardSerialNumber();
                Authentification.RegistrationInDataBase(firstName, lastName, email, serialNumber);
                mail.GetDataFromMail(firstName, lastName, email);
            }
            else
            {
                counts = Errors.Length;
            }
        }

    }

}
