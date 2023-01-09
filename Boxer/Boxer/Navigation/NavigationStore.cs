using Boxer.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Navigation
{
    public class NavigationStore : INavigationStore
    {
        public event Action CurrentPageChanged;

        private BaseViewModel _currentPage;
        public BaseViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage?.Dispose();
                _currentPage = value;
                OnCurrentPageChanged();
            }
        }

        private void OnCurrentPageChanged()
        {
            CurrentPageChanged?.Invoke();
        }
    }
}
