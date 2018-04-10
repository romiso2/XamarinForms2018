using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;


namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {


            //TODO - Validações.

            string cep = CEP.Text.Trim();

            if (isValdCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} - {3}, {0}/{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    } else
                    {
                        DisplayAlert("ERRO!", "Endereço não localizado para o CEP informado: " + cep, "OK");
                    }
                    

                }catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message,"OK");
                }
                
            }

            
        }

        private bool isValdCEP(string cep)
        {

            bool valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 digitos sem pontos ou traços.","OK");
                valido = false;
            }

            int NovoCEP = 0;

            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter somente números.", "OK");
                valido = false;
            }
            return valido;
        }
	}
}
