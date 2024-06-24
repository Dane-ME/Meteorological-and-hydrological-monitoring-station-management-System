using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoAn.Services;
using DoAn.Views.AdminView;

namespace DoAn.ViewModels.AdminViewModel
{
    public class AddUserViewModel : ObservableObject
    {
        private bool _answer;
        public bool Answer
        {
            get => _answer;
            set
            {
                SetProperty(ref _answer, value);
                EventChanged.Instance.OnAnswerChanged();
            } 
        }
        public ICommand RegisCommand { get; set; }
        public AddUserViewModel() 
        {
            
            RegisCommand = new Command(() =>
            {
                EventChanged.Instance.OnPopupHandleChanged();
            });

            EventChanged.Instance.AnswerChanged += (s, e) =>
            {
                if(Answer) { }
                else { }
            };
        }   

    }
}
