using routedapp.Models;
using routedapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace routedapp.ViewModels
{
    class TasksViewModel : BaseViewModel
    {
        public ObservableCollection<TodoTask> Tasks {get; set;}

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
            Tasks = new ObservableCollection<TodoTask>();
            for(int i = 0; i < 5; i++)
            {
                Tasks.Add(new TodoTask()
                {
                    Id = i,
                    Title = $"This is task no {i.ToString()}",
                    Description = "Something goes here",
                    IsCompleted = false,
                });
            }
            OnItemTapped = new Command<TodoTask>(async (x) => await ItemTapped(x));
            OnRefresh = new Command(async () => await Refresh());
            OnEdit = new Command<TodoTask>(async (x) => await Edit(x));
            OnDelete = new Command<TodoTask>(async (x) => await Delete(x));
            OnItemAdded = new Command(async () => await ItemAdded());
        }

        public ICommand OnItemTapped { get; }
        public ICommand OnRefresh { get; }
        public ICommand OnEdit { get; }
        public ICommand OnDelete { get; }
        public ICommand OnItemAdded { get; }

        async Task ItemTapped(TodoTask x)
        {
            x.IsCompleted = !x.IsCompleted;
            var status = x.IsCompleted ? "completed" : "not completed";
            await Application.Current.MainPage.DisplayAlert($"Item was marked as {status}", string.Empty, "OK");
        }
        async Task Edit(TodoTask x)
        {
            //if (task == null)
            //    return;
            await Shell.Current.GoToAsync(nameof(EditTaskPage));
            //await Application.Current.MainPage.DisplayAlert($"Task {x.Id} was edited", string.Empty, "OK");

        }
        async Task Delete(TodoTask x)
        {
            //if (task == null)
            //    return;
            Tasks.Remove(x);
            await Application.Current.MainPage.DisplayAlert($"Task {x.Id} was deleted", string.Empty, "OK");

        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(1000);

            IsBusy = false;
        }

        async Task ItemAdded()
        {
            IsBusy = true;
            if(todoInputTitleValue.Trim().Length > 0 && todoInputDescriptionValue.Trim().Length > 0)
            {
                var rnd = new Random();
                Tasks.Add(new TodoTask {
                    Id = Tasks.Last().Id + 1,
                    Title = todoInputTitleValue,
                    Description = todoInputDescriptionValue,
                    IsCompleted = false

                });
                TodoInputTitleValue = string.Empty;
                TodoInputDescriptionValue = string.Empty;
                await Application.Current.MainPage.DisplayAlert("The task has been added", string.Empty, "OK");
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Please enter a valid value", string.Empty, "OK");
            }
            IsBusy = false;
        }
    }
}
