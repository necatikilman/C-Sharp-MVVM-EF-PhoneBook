using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBookEF
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        PhoneBookRepository phoneBookRepository = new PhoneBookRepository();

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private bool saveButtonEnabled;
        private bool deleteButtonEnabled;

        private string lastname;
        private string firstname;
        private string email;
        private string phoneNumber;
        private string address;
        private List<Contact> contacts = new List<Contact>();
        private Contact selectedContact;

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged();
                CheckSaveButtonAvailablity();
            }
        }
        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged();
                CheckSaveButtonAvailablity();
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
                CheckSaveButtonAvailablity();
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
                CheckSaveButtonAvailablity();
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
                CheckSaveButtonAvailablity();
            }
        }
        public List<Contact> Contacts 
        { 
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                if (selectedContact != null)
                {
                    FillTextBoxes(selectedContact);
                }
                else
                {
                    ResetProperties();
                }
                OnPropertyChanged();
                CheckDeleteButtonAvailablity();
            }
        }
        public bool SaveButtonEnabled
        {
            get { return saveButtonEnabled; }
            private set
            {
                saveButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool DeleteButtonEnabled
        {
            get { return deleteButtonEnabled; }
            private set
            {
                deleteButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            SaveCommand = new ActionCommand(SaveCommandAction);
            DeleteCommand = new ActionCommand(DeleteCommandAction);
            Contacts = phoneBookRepository.GetAllContacts();
        }

        private void SaveCommandAction()
        {
            if (SelectedContact == null)
            {
                phoneBookRepository.SaveContact(new Contact(Lastname, Firstname, Email, PhoneNumber, Address));
                ResetProperties();
            }
            else
            {
                SelectedContact.Lastname = Lastname;
                SelectedContact.Firstname = Firstname;
                SelectedContact.Email = Email;
                SelectedContact.PhoneNumber = PhoneNumber;
                SelectedContact.Address = Address;
                phoneBookRepository.UpdateContact(SelectedContact);

                SelectedContact = null;
            }
            Contacts = phoneBookRepository.GetAllContacts();
        }
        private void DeleteCommandAction()
        {
            phoneBookRepository.DeleteContact(SelectedContact);
            Contacts = phoneBookRepository.GetAllContacts();
        }

        private void FillTextBoxes(Contact selectedContact)
        {
            Lastname = selectedContact.Lastname;
            Firstname = selectedContact.Firstname;
            Email = selectedContact.Email;
            PhoneNumber = selectedContact.PhoneNumber;
            Address = selectedContact.Address;
        }
        private void ResetProperties()
        {
            Lastname = null;
            Firstname = null;
            Email = null;
            PhoneNumber = null;
            Address = null;
        }

        private void CheckSaveButtonAvailablity()
        {
            SaveButtonEnabled = (Lastname != null && Lastname != "") 
                && (Firstname != null && Firstname != "") 
                && ((Email != null && Email != "") || (PhoneNumber != null && PhoneNumber != ""));
        }
        private void CheckDeleteButtonAvailablity()
        {
            DeleteButtonEnabled = SelectedContact != null;
        }

        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
