using EstoqueApp.Views;
using Interfaces;
using Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace EstoqueApp.ViewModels
{
    public class AdicionarEditarProdutoViewModel : ViewModelBase
    {
        private readonly IProduto produto;
        private readonly INavigationService _navigation;

        #region Commands
        public ICommand ConfirmarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        #endregion



        #region Propriedades
        private string nome;

        public string Nome
        {
            get { return nome; }
            set 
            { 
                nome = value;
                RaisePropertyChanged();
            }
        }

        private int quantidade;

        public int Quantidade
        {
            get { return quantidade; }
            set 
            { 
                quantidade = value;
                RaisePropertyChanged();
            }
        }

        private double preco;

        public double Preco
        {
            get { return preco; }
            set 
            { 
                preco = value;
                RaisePropertyChanged();
            }
        }



        private Produto produtoEditavel;

        public Produto ProdutoEditavel
        {
            get { return produtoEditavel; }
            set
            {
                produtoEditavel = value;
                RaisePropertyChanged();
            }
        }
        #endregion





        public AdicionarEditarProdutoViewModel(INavigationService navigationService, IProduto produto)
           : base(navigationService)
        {
            this.produto = produto;
            _navigation = navigationService;
            AssinarCommands();
        }

        private void AssinarCommands()
        {
            ConfirmarCommand = new Command(async () => 
            {
                try
                {
                    AtualizarProduto();
                    if (Title.Contains("Editar"))
                    {
                        await this.produto.UpdateProducts(ProdutoEditavel);
                        await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", "Produto atualizado com sucesso.", "Ok");

                    }
                    else
                    {
                        await this.produto.CreateProduct(ProdutoEditavel);
                        await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", "Produto adicionado com sucesso.", "Ok");
                    }
                    await _navigation.NavigateAsync(nameof(ListaProdutosView));
                }
                catch (Exception ex)
                {
                    await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Atenção", ex.Message, "Ok");
                }

                
            });

            CancelarCommand = new Command(async () => 
            {
                await _navigation.NavigateAsync(nameof(ListaProdutosView));
            });
        }

        private void AtualizarProduto()
        {
            ProdutoEditavel.Nome = Nome;
            ProdutoEditavel.Quantidade = Quantidade;
            ProdutoEditavel.Valor = Preco;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Title = parameters.GetValue<string>(ParansKeys.AdicionarEditarProdutoTitle);
            ProdutoEditavel = parameters.GetValue<Produto>(ParansKeys.ProdutoEditavel);

            if(ProdutoEditavel != null)
            {
                Nome = ProdutoEditavel.Nome;
                Quantidade = ProdutoEditavel.Quantidade;
                Preco = ProdutoEditavel.Valor;
            }
            else
            {
                ProdutoEditavel = new Produto();
            }
        }
    }
}
