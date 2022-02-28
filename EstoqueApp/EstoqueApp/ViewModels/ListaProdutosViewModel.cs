using EstoqueApp.Views;
using Interfaces;
using Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EstoqueApp.ViewModels
{
    public class ListaProdutosViewModel : ViewModelBase
    {

        private ObservableCollection<Produto> _produtos;
        private readonly IProduto produto;
        private readonly INavigationService _navigation;

        public ObservableCollection<Produto> Produtos
        {
            get { return _produtos; }
            set 
            { 
                _produtos = value;
                RaisePropertyChanged();
            }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            { 
                isRefreshing = value;
                RaisePropertyChanged();
            }
        }


        public ICommand AdicionarProdutoCommand { get; set; }
        public ICommand Editar { get; set; }
        public ICommand Deletar { get; set; }
        public ICommand RefreshCommand { get; set; }


        public ListaProdutosViewModel(INavigationService navigationService, IProduto produto)
            : base(navigationService)
        {
            Title = "Lista de Produtos";
            AssinarCommands();
            this.produto = produto;
            _navigation = navigationService;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await BuscarProdutos();
        }

        private async Task BuscarProdutos()
        {
            IsRefreshing = true;
            try
            {
                Produtos = new ObservableCollection<Produto>(await produto.GetProducts());
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", ex.Message, "Ok");
            }
            IsRefreshing = false;
        }
        private void AssinarCommands()
        {
            AdicionarProdutoCommand = new Command(async () => {
                try
                {
                    var param = new NavigationParameters
                    {
                        { ParansKeys.AdicionarEditarProdutoTitle, "Adicionar Produto" }
                    };
                    await _navigation.NavigateAsync(nameof(AdicionarEditarProdutoView), param);
                }
                catch (Exception ex)
                {
                    await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", ex.Message, "Ok");
                }
            });

            Editar = new Command<Produto>(async produto => {
                try
                {
                    var param = new NavigationParameters
                    {
                        { ParansKeys.AdicionarEditarProdutoTitle, "Editar Produto" },
                        { ParansKeys.ProdutoEditavel, produto}
                    };
                    await _navigation.NavigateAsync(nameof(AdicionarEditarProdutoView), param);
                }
                catch (Exception ex)
                {
                    await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", ex.Message, "Ok");
                }
            });

            Deletar = new Command<int>(async id => {
                try
                {
                    if(await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", "Tem certeza que deseja excluir o produto selecionado?", "Sim", "Não"))
                    {
                        await produto.DeleteProducts(id);
                        await BuscarProdutos();
                    }
                }
                catch (Exception ex)
                {
                    await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", ex.Message, "Ok");
                }
            });

            RefreshCommand = new Command(async() => {
                await BuscarProdutos();
            });
        }
    }
}
