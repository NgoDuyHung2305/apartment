using Apartment_Management.Helper;
using Apartment_Management.Model;
using Apartment_Management.Service;
using Apartment_Management.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Apartment_Management.ViewModel
{
    internal class Block_Management_View_Model : Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        private MainWindowViewModel _mainViewModel;
        public ICommand BlockViewCommand { get; set; }


        public BlockView BlockView;
        public ObservableCollection<Block> BlockList { get; set; }
        private Block _selectedBlock;

        public Block SelectedBlock
        {
            get => _selectedBlock;
            set
            {
                _selectedBlock = value;
                OnPropertyChanged();
            }
        }

       

        public Block_Management_View_Model(MainWindowViewModel mainViewModel)
        {
            _firebaseService = new FirebaseService();
            _mainViewModel = mainViewModel;
            BlockList = new ObservableCollection<Block>();
            GetBlocksAsync(BlockList);

            BlockViewCommand = new RelayCommand(async _ => await BlockViewClick());
        }
        public Block_Management_View_Model() {  }
       
        private async Task BlockViewClick()
        {
            
            BlockView = new BlockView
            {
                DataContext = new Block_View_Model(_mainViewModel, SelectedBlock)
            };

            // Chuyển sang BlockView
            _mainViewModel.CurrentView = BlockView;
        }
        
        private async void GetBlocksAsync(ObservableCollection<Block> blockList)
        {
            var blocks = await _firebaseService.GetDataAsync<Block>("Blocks", "BlockID");
           
            foreach (var block in blocks)
            {
                blockList.Add(block);
            }
            
        }
    }
}
