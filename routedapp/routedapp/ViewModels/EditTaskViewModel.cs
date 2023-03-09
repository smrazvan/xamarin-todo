using routedapp.Models;
using routedapp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace routedapp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    class EditTaskViewModel : BaseViewModel
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

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

        async Task SetDefaultInputs()
        {
            var todo = await TodosRepository.GetById(Id);
            TodoInputTitleValue = todo.Title;
            TodoInputDescriptionValue = todo.Description;
        }

        public EditTaskViewModel()
        {
            SetDefaultInputs();
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
