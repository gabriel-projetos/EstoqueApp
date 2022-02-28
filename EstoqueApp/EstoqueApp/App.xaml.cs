using Communic;
using EstoqueApp.ViewModels;
using EstoqueApp.Views;
using Interfaces;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace EstoqueApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/ListaProdutosView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<ListaProdutosView, ListaProdutosViewModel>();
            containerRegistry.RegisterSingleton<IProduto, ProdutoCommunic>();
            containerRegistry.RegisterForNavigation<AdicionarEditarProdutoView, AdicionarEditarProdutoViewModel>();
            containerRegistry.RegisterForNavigation<AdicionarEditarProdutoView, AdicionarEditarProdutoViewModel>();
        }
    }
}
