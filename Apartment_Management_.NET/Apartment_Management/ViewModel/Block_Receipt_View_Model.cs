using Apartment_Management.Model;
using Apartment_Management.Service;
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
    internal class Block_Receipt_View_Model:Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        public ObservableCollection<Receipt> PaidReceiptList { get; set; }
        public ObservableCollection<Receipt> PaidReceipts { get; set; }

        public ObservableCollection<Receipt> UnpaidReceiptList { get; set; }
        public ObservableCollection<Receipt> UnpaidReceipts { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; set; }

        public Block Block { get; set; }
        public Block_Receipt_View_Model() { }
        public Block_Receipt_View_Model(MainWindowViewModel mainWindow, Block block)
        {
            _firebaseService = new FirebaseService();
            PaidReceiptList = new ObservableCollection<Receipt>();
            PaidReceipts = new ObservableCollection<Receipt>();
            UnpaidReceiptList = new ObservableCollection<Receipt>();
            UnpaidReceipts = new ObservableCollection<Receipt>();
            Block = new Block(block);
            GetAsync(PaidReceiptList, UnpaidReceiptList, Block);
            GetAsync(PaidReceipts, UnpaidReceipts, Block);
            SearchCommand = new RelayCommand<string>(OnSearch);
        }
        private async void GetAsync(ObservableCollection<Receipt> paidReceiptList, ObservableCollection<Receipt> unpaidReceiptList, Block block)
        {
            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            var Receipts = await _firebaseService.GetDataAsync<Receipt>("Bills", "ReceiptID");
            var PaidReceipts = Receipts.Where(receipts => BlockRooms.Any(room => room.RoomID == receipts.RoomID)&&receipts.Status=="Paid");
            foreach (var receipt in PaidReceipts)
            {
                paidReceiptList.Add(receipt);
            }
            var UnpaidReceipts = Receipts.Where(receipts => BlockRooms.Any(room => room.RoomID == receipts.RoomID) && receipts.Status == "Unpaid");
            foreach (var receipt in UnpaidReceipts)
            {
                unpaidReceiptList.Add(receipt);
            }
        }
        private void OnSearch(string obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                UnpaidReceipts.Clear();
                PaidReceipts.Clear();
                foreach (var contract in UnpaidReceiptList)
                {
                    UnpaidReceipts.Add(contract);
                }
                foreach (var contract in PaidReceiptList)
                {
                    PaidReceipts.Add(contract);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filterUnpaidReceipts = UnpaidReceiptList.Where(c =>
                (c.OrderID != null && RemoveVietnameseDiacritics(c.OrderID.ToLower()).Contains(lowerSearchText)) ||
                (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
                (c.Type != null && RemoveVietnameseDiacritics(c.Type.ToLower()).Contains(lowerSearchText))).ToList();

                var filterPaidReceipts = PaidReceiptList.Where(c =>
                (c.OrderID != null && RemoveVietnameseDiacritics(c.OrderID.ToLower()).Contains(lowerSearchText)) ||
                (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
                (c.Type != null && RemoveVietnameseDiacritics(c.Type.ToLower()).Contains(lowerSearchText))).ToList();
                UnpaidReceipts.Clear();
                foreach (var order in filterUnpaidReceipts)
                {
                    UnpaidReceipts.Add(order);
                }
                PaidReceipts.Clear();
                foreach (var order in filterPaidReceipts)
                {
                    PaidReceipts.Add(order);
                }
            }


        }
        public string RemoveVietnameseDiacritics(string text)
        {
            string[] vietnameseChars = new string[]
            {
            "áàảãạâấầẩẫậăắằẳẵặ",
            "éèẻẽẹêếềểễệ",
            "íìỉĩị",
            "óòỏõọôốồổỗộơớờởỡợ",
            "úùủũụưứừửữự",
            "ýỳỷỹỵ",
            "đ",
            "ÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶ",
            "ÉÈẺẼẸÊẾỀỂỄỆ",
            "ÍÌỈĨỊ",
            "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
            "ÚÙỦŨỤƯỨỪỬỮỰ",
            "ÝỲỶỸỴ",
            "Đ"
            };

            char[] replaceChars = new char[]
            {
            'a', 'e', 'i', 'o', 'u', 'y', 'd',
            'A', 'E', 'I', 'O', 'U', 'Y', 'D'
            };

            for (int i = 0; i < vietnameseChars.Length; i++)
            {
                foreach (char c in vietnameseChars[i])
                {
                    text = text.Replace(c, replaceChars[i]);
                }
            }

            return text;
        }
    }
}
