using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace routedapp.ViewModels
{
    class EditTaskViewModel : BaseViewModel
    {
        private string todoInputTitleValue = string.Empty;
        public string TodoInputTitleValue
        {
            get { return todoInputTitleValue; }
            set { SetProperty(ref todoInputTitleValue, value); }
        }

        private string todoInputDescriptionValue = string.Empty;
        public string TodoInputDescriptionValue
        {
            get { return todoInputDescriptionValue; }
            set { SetProperty(ref todoInputDescriptionValue, value); }
        }
        public EditTaskViewModel()
        {
            OnEdit = new Command(async () => await Edit());
            OnCancel = new Command(async () => await Cancel());
        }

        public ICommand OnEdit { get; }
        public ICommand OnCancel { get; }

        async Task Edit()
        {

        }

        async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
