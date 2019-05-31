using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using ModuleDataBase;
using ModuleDataBase.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;

namespace ModuleView.ViewModels
{
    public class MainWindowsViewModel : BindableBase, INavigationAware
    {
        IRegionNavigationJournal _journal;
        private readonly IBaseContext _context;

        #region команды и модели
        public DelegateCommand<string> PlayRecordCommand { get; private set; }
        public DelegateCommand LoadRecordsCommand { get; private set; }

        private int _countRecords;
        public int CountRecords
        {
            get { return _countRecords; }
            set
            {
                SetProperty(ref _countRecords, value);
            }
        }

        private string _pathFile;
        public string PathFile
        {
            get { return _pathFile; }
            set
            {
                SetProperty(ref _pathFile, value);
            }
        }

        private IEnumerable<CdrModel> _cdr;
        public IEnumerable<CdrModel> Cdr
        {
            get { return _cdr; }
            set
            {
                SetProperty(ref _cdr, value);
            }
        }
        #endregion

        public MainWindowsViewModel(IBaseContext context)
        {
            _context = context;
            LoadRecordsCommand = new DelegateCommand(LoadRecords);
            PlayRecordCommand = new DelegateCommand<string>(PlayRecord);
        }

        private void PlayRecord(string Record)
        {
            PathFile = string.Format(@"\\gateway\asterisk\{0}.wav", Record);
        }

        private void LoadRecords()
        {
            Cdr = null;
            Cdr = _context.CdrModels.OrderByDescending(x => x.Calldate).Take(10).ToList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
