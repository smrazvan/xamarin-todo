using routedapp.Models;
using routedapp.Models.Tables;
using routedapp.Services;
using routedapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Forms;

namespace routedapp.ViewModels
{
    class TasksViewModel : BaseViewModel
    {
        public ObservableCollection<TodoModel> Tasks {get; set;} = new ObservableCollection<TodoModel>() { };

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

        public TasksViewModel()
        {
            Title = "Tasks";
            //load data asynchrounously
            IsBusy = true;
            Refresh();

            OnItemTapped = new Command<TodoModel>(async (x) => await ItemTapped(x));
            OnRefresh = new Command(async () => await Refresh());
            OnEdit = new Command<TodoModel>(async (x) => await Edit(x));
            OnDelete = new Command<TodoModel>(async (x) => await Delete(x));
            OnItemAdded = new Command(async () => await ItemAdded());
        }

        public ICommand OnItemTapped { get; }
        public ICommand OnRefresh { get; }
        public ICommand OnEdit { get; }
        public ICommand OnDelete { get; }
        public ICommand OnItemAdded { get; }

        async Task ItemTapped(TodoModel x)
        {
           
            x.IsCompleted = !x.IsCompleted;
            await TodosRepository.Edit(x);
            await Refresh();
            var status = x.IsCompleted ? "completed" : "not completed";
            await Application.Current.MainPage.DisplayAlert($"Item was marked as {status}", string.Empty, "OK");
        }
        async Task Edit(TodoModel x)
        {
            if (x == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(EditTaskPage)}?Id={x.Id}");

        }
        async Task Delete(TodoModel todo)
        {
            if (todo == null)
                return;
            await TodosRepository.Remove(todo);
            await Refresh();
            await Application.Current.MainPage.DisplayAlert($"Task {todo.Id} was deleted", string.Empty, "OK");

        }
        async Task Refresh()
        {
            IsBusy = true;

            Tasks.Clear();

            var tasks = await TodosRepository.GetAll();

            foreach(var task in tasks)
            {
                Tasks.Add(task);
            }
            IsBusy = false;
        }

        async Task ItemAdded()
        {
            IsBusy = true;
            if(todoInputTitleValue.Trim().Length > 0 && todoInputDescriptionValue.Trim().Length > 0)
            {
                await TodosRepository.Add(new TodoModel {
                    Title = todoInputTitleValue,
                    Description = todoInputDescriptionValue,
                    IsCompleted = false
                });

                //reset entries
                TodoInputTitleValue = string.Empty;
                TodoInputDescriptionValue = string.Empty;

                await Refresh();
                await Application.Current.MainPage.DisplayAlert("The task has been added", string.Empty, "OK");
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Please enter a valid value", string.Empty, "OK");
            }
            IsBusy = false;
        }
    }
}
